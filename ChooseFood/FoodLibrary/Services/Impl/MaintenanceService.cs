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
        public ILogService _logService;
        public IFoodFavorService _foodFavorService;

        public MaintenanceService(IRecommendationService recommendationService, IUserChoiceService userChoiceService,
            IUserFavorService userFavorService, IOneDriveService oneDriveService, ILastTimeCommitService lastTimeCommitService,
            ILogService logService, IFoodFavorService foodFavorService)
        {
            _recommendationService = recommendationService;
            _userChoiceService = userChoiceService;
            _userFavorService = userFavorService;
            _oneDriveService = oneDriveService;
            _lastTimeCommitService = lastTimeCommitService;
            _logService = logService;
            _foodFavorService = foodFavorService;
        }

        public async System.Threading.Tasks.Task MaintenanceAsync()
        {
            List<Log> localLog = _logService.GetLogs();
            if (localLog == null)
            {
                localLog = new List<Log>();
            }
            List<Log> cloudLog = await _oneDriveService.LoadLogAsync();

            DateTime lastCommitTime = await _lastTimeCommitService.ReadJsonAsync();
            if (lastCommitTime == null)
            {
                lastCommitTime = DateTime.Now;
            }
            List<FoodWeightChange> totalWeight = await _oneDriveService.LoadFoodWeightAsync();
            if (totalWeight.Count == 0)
            {
                List<FoodInformation> foodInformations = _recommendationService.GetFoodInfs();
                for (int i = 0; i < foodInformations.Count; i++)
                {
                    FoodWeightChange foodWeightChange = new FoodWeightChange();
                    foodWeightChange.FoodName = foodInformations[i].Name;
                    int[] arr = { 0, 0, 0, 0, 0, 0 };
                    List<int> reasonList = new List<int>(arr);
                    foodWeightChange.weightChangeList = reasonList;
                    totalWeight.Add(foodWeightChange);
                }
            }
            // bug
            var foodInformationList = _recommendationService.GetFoodInfs();

            for (int i = 0; i < foodInformationList.Count; i++)
            {
                var FoodName = foodInformationList[i].Name;

                ChangeInformation cloudChangeInfList = GetChangeInf(cloudLog, lastCommitTime, FoodName);

                ChangeInformation localChangeInfList = GetChangeInf(localLog, lastCommitTime, FoodName);

                int[] arr = { 0, 0, 0, 0, 0, 0 };
                List<int> tmpWeightChangeList = totalWeight[i].weightChangeList;
                for (int j = 0; j < 9; j++)
                {
                    int flag = 0;
                    for (int k = 0; k < 6; k++)
                    {
                        if (cloudChangeInfList.Weight[j][k] >= 0 && localChangeInfList.Weight[j][k] >= 0)
                        {
                            if (cloudChangeInfList.Weight[j][k] >= localChangeInfList.Weight[j][k])
                            {
                                //tmpWeightChangeList[k] = tmpWeightChangeList[k] + cloudChangeInfList.Weight[j][k];
                                tmpWeightChangeList[k] = tmpWeightChangeList[k];
                                continue;
                            }
                            else if (cloudChangeInfList.Weight[j][k] < localChangeInfList.Weight[j][k])
                            {
                                if (flag == 0)
                                {
                                    cloudLog = DeleteRecord(cloudLog, FoodName, lastCommitTime,j);
                                    cloudLog = InsertRecord(cloudLog, localLog, FoodName, lastCommitTime,j);
                                    flag = 1;
                                    for(int m =0;m < 6; m++)
                                    {
                                        tmpWeightChangeList[m] = tmpWeightChangeList[m] - cloudChangeInfList.Weight[j][m] + localChangeInfList.Weight[j][m];
                                    }
                                }
                                
                            }
                        }

                        else if (cloudChangeInfList.Weight[j][k] <= 0 && localChangeInfList.Weight[j][k] <= 0)
                        {
                            if (cloudChangeInfList.Weight[j][k] <= localChangeInfList.Weight[j][k])
                            {
                                tmpWeightChangeList[k] = tmpWeightChangeList[k];
                                continue;
                            }
                            else if (cloudChangeInfList.Weight[j][k] > localChangeInfList.Weight[j][k])
                            {
                                if (flag == 0)
                                {
                                    cloudLog = DeleteRecord(cloudLog, FoodName, lastCommitTime,j);
                                    cloudLog = InsertRecord(cloudLog, localLog, FoodName, lastCommitTime,j);
                                    flag = 1;
                                    for (int m = 0; m < 6; m++)
                                    {
                                        tmpWeightChangeList[m] = tmpWeightChangeList[m] - cloudChangeInfList.Weight[j][m] + localChangeInfList.Weight[j][m];
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (flag == 0)
                            {
                                cloudLog = InsertRecord(cloudLog, localLog, FoodName, lastCommitTime,j);
                                flag = 1;
                                for (int m = 0; m < 6; m++)
                                {
                                    tmpWeightChangeList[m] = tmpWeightChangeList[m]+ localChangeInfList.Weight[j][m];
                                }
                            }
                        }
                    }
                }
                _foodFavorService.SetWeight(i, tmpWeightChangeList);
            }
            localLog = cloudLog;
            _logService.SetLogs(cloudLog);
            _logService.SaveLogAsync();
            _foodFavorService.SaveChangeWeightAsync();
            lastCommitTime = DateTime.Now;
            _lastTimeCommitService.SaveJsonAsync(lastCommitTime);
        }


        public List<Log> InsertRecord(List<Log> cloudLog, List<Log> localLog, string foodName, DateTime lastCommitTime,int j)
        {
            int i = 0;
            while (i < localLog.Count)
            {
                if (localLog[i].FoodName == foodName && lastCommitTime < localLog[i].Date && j == localLog[i].WeatherList[0] * 3 + localLog[i].WeatherList[1])
                {
                    cloudLog.Add(localLog[i]);
                }

                i++;
            }

            cloudLog = cloudLog.OrderBy(log => log.Date).ToList();
            return cloudLog;
        }

        public List<Log> DeleteRecord(List<Log> cloudLog, string foodName, DateTime lastCommitTime,int j)
        {
            int i = 0;
            while (i < cloudLog.Count)
            {
                if (cloudLog[i].Date > lastCommitTime && cloudLog[i].FoodName == foodName && j == cloudLog[i].WeatherList[0]*3+ cloudLog[i].WeatherList[1])
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
            int[] arr = { 0, 0, 0, 0, 0, 0 };
            changeInf.Weight = new List<List<int>>(9);

            for (int i = 0; i < 9; i++)
            {
                List<int> weight = new List<int>(arr);
                changeInf.Weight.Add(weight);
            }

            for (int i = 0; i < cloudLog.Count; i++)
            {
                if (cloudLog[i].Date < lastCommitTime)
                {
                    continue;
                }

                else if (foodName == cloudLog[i].FoodName)
                {
                    int changePos = 3 * cloudLog[i].WeatherList[0] + cloudLog[i].WeatherList[1];
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
