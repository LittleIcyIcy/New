using System;
using System.Collections.Generic;
using System.Text;
using FoodLibrary.Models;
using FoodLibrary.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MvvmHelpers;

namespace FoodLibrary.ViewModels
{
    public class MenuPage2ViewModel:ViewModelBase
    {
        /// <summary>
        /// 日志服务。
        /// </summary>
        private ILogService _logService;

        public MenuPage2ViewModel(ILogService logService)
        {
            _logService = logService;
        }

        /// <summary>
        /// 历史记录。
        /// </summary>
        public ObservableRangeCollection<Log> HistoricFoodInformationsCollection
            {get;} = new ObservableRangeCollection<Log>();


        public RelayCommand ShowCommand =>
            _showCommand ?? (_showCommand = 
                new RelayCommand(() => {
                    HistoricFoodInformationsCollection.Clear();
                    List<Log> temp = new List<Log>(_logService.GetLogs());
                    if(temp.Count != 0)
                    {
                        for(int i = 0; i < temp.Count; i++)
                        {
                            List<string> vs = new List<string>();
                            switch (temp[i].WeatherList[0])
                            {
                                case 0:
                                    vs.Add("天气寒冷");
                                    break;
                                case 1:
                                    vs.Add("温度适宜");
                                    break;
                                case 2:
                                    vs.Add("热");
                                    break;
                                default:break;
                            }
                            switch (temp[i].WeatherList[1])
                            {
                                case 0:
                                    vs.Add("天干物燥，小心火烛");
                                    break;
                                case 1:
                                    vs.Add("湿度刚刚好");
                                    break;
                                case 2:
                                    vs.Add("潮湿");
                                    break;
                                default: break;
                            }
                            switch (temp[i].GetFavor())
                            {
                                case 0:
                                    vs.Add("sucai/cai.jpg");
                                    break;
                                case 1:
                                    vs.Add("sucai/zan.jpg");
                                    break;
                                default:break;
                            }
                            temp[i].WeatherInformationList = vs;
                        }
                        HistoricFoodInformationsCollection.AddRange(temp);
                    }
                }));
        private RelayCommand _showCommand;

    }
}
