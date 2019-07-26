using FoodLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FoodLibrary.Services.Impl
{
    public class MaintenanceService : IMaintenanceService
    {
        /// <summary>
        /// 统计每一种食物 在不同天气的权重改变量
        /// </summary>
        class ChangeInformation
        {
            public string FoodName;
            public List<List<int>> Weight;
        }

        private IRecommendationService _recommendationService;

        private IUserChoiceService _userChoiceService;
        private IUserFavorService _userFavorService;
        private IOneDriveService _oneDriveService;
        private ILastTimeCommitService _lastTimeCommitService;

        public MaintenanceService(IRecommendationService recommendationService, IUserChoiceService userChoiceService,
            IUserFavorService userFavorService, IOneDriveService oneDriveService, ILastTimeCommitService lastTimeCommitService)
        {
            _recommendationService = recommendationService;
            _userChoiceService = userChoiceService;
            _userFavorService = userFavorService;
            _oneDriveService = oneDriveService;
            _lastTimeCommitService = lastTimeCommitService;
        }

        public async System.Threading.Tasks.Task MaintenanceAsync()
        {
            List<Log> localLog = await _userChoiceService.ReadJsonAsync();
            List<Log> cloudLog = await _oneDriveService.LoadLogAsync();

            DateTime lastCommitTime = await _lastTimeCommitService.ReadJsonAsync();

            List<FoodWeightChange> totalWeight = await _oneDriveService.LoadFoodWeightAsync();
            var foodInformationList = _recommendationService.GetFoodInfs();

            for (int i = 0; i < foodInformationList.Count; i++)
            {
                var FoodName = foodInformationList[i].Name;

                ChangeInformation cloudChangeInfList = GetChangeInf(cloudLog, lastCommitTime, FoodName);

                ChangeInformation localChangeInfList = GetChangeInf(localLog, lastCommitTime, FoodName);

                List<int> tmpWeightChangeList = new List<int>(6);
                for (int j = 0; j < 9; j++)
                {
                    for (int k = 0; k < 6; k++)
                    {
                        if (cloudChangeInfList.Weight[j][k] >= 0 && localChangeInfList.Weight[j][k] >= 0)
                        {
                            if (cloudChangeInfList.Weight[j][k] >= localChangeInfList.Weight[j][k])
                            {
                                continue;
                            }
                            else if (cloudChangeInfList.Weight[j][k] < localChangeInfList.Weight[j][k])
                            {
                                cloudLog = DeleteRecord(cloudLog, FoodName, lastCommitTime);
                                cloudLog = InsertRecord(cloudLog, localLog, FoodName, lastCommitTime);
                                tmpWeightChangeList[k] = tmpWeightChangeList[k] + (localChangeInfList.Weight[j][k] - cloudChangeInfList.Weight[j][k]);
                            }
                        }

                        else if (cloudChangeInfList.Weight[j][k] <= 0 && localChangeInfList.Weight[j][k] <= 0)
                        {
                            if (cloudChangeInfList.Weight[j][k] <= localChangeInfList.Weight[j][k])
                            {
                                continue;
                            }
                            else if (cloudChangeInfList.Weight[j][k] > localChangeInfList.Weight[j][k])
                            {
                                cloudLog = DeleteRecord(cloudLog, FoodName, lastCommitTime);
                                cloudLog = InsertRecord(cloudLog, localLog, FoodName, lastCommitTime);
                                tmpWeightChangeList[k] = tmpWeightChangeList[k] + (localChangeInfList.Weight[j][k] - cloudChangeInfList.Weight[j][k]);
                            }
                        }
                        else
                        {
                            cloudLog = InsertRecord(cloudLog, localLog, FoodName, lastCommitTime);
                            tmpWeightChangeList[k] = tmpWeightChangeList[k] + localChangeInfList.Weight[j][k];
                        }
                    }
                }

                localLog = cloudLog;
                _userChoiceService.SaveJsonAsync(localLog);
                lastCommitTime = DateTime.Now;
                _lastTimeCommitService.SaveJsonAsync(lastCommitTime);

            }
        }


        private List<Log> InsertRecord(List<Log> cloudLog, List<Log> localLog, string foodName, DateTime lastCommitTime)
        {
            int i = 0;
            while (i < localLog.Count)
            {
                if (localLog[i].FoodName == foodName && lastCommitTime > localLog[i].Date)
                {
                    cloudLog.Add(localLog[i]);
                }
            }

            cloudLog.OrderBy(log => log.Date);
            return cloudLog;
        }

        private List<Log> DeleteRecord(List<Log> cloudLog, string foodName, DateTime lastCommitTime)
        {
            int i = 0;
            while (i < cloudLog.Count)
            {
                if (cloudLog[i].Date > lastCommitTime && cloudLog[i].FoodName == foodName)
                {
                    cloudLog.RemoveAt(i);
                }
                else
                {
                    i++;
                }
            }

            return cloudLog;
        }

        private ChangeInformation GetChangeInf(List<Log> cloudLog, DateTime lastCommitTime, string foodName)
        {
            ChangeInformation changeInf = new ChangeInformation();
            changeInf.FoodName = foodName;
            changeInf.Weight = new List<List<int>>(9);

            for (int i = 0; i < 9; i++)
            {
                List<int> weight = new List<int>(6);
                changeInf.Weight[i] = weight;
            }

            for (int i = 0; i < cloudLog.Count; i++)
            {
                if (cloudLog[i].Date < lastCommitTime)
                {
                    continue;
                }

                else if (foodName == cloudLog[i].FoodName)
                {
                    int changePos = 3 * cloudLog[i].WeatherList[0] + cloudLog[i].WeatherList[0];
                    for (int j = 0; j < 6; j++)
                    {
                        changeInf.Weight[changePos][j] =
                            changeInf.Weight[changePos][j] + cloudLog[i].WeightChangeList[j];
                    }

                }
            }

            return changeInf;

        }

    }
}
