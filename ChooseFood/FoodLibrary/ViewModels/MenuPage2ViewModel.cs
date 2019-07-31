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
        public ObservableRangeCollection<Log> HistoricFoodInformationsCollection {
            get;
        } = 
            new ObservableRangeCollection<Log>();

        public RelayCommand ShowCommand =>
            _showCommand ?? (_showCommand = 
                new RelayCommand(() => {
                    HistoricFoodInformationsCollection.Clear();
                    HistoricFoodInformationsCollection.AddRange(_logService.GetLogs());
                }));
        private RelayCommand _showCommand;

    }
}
