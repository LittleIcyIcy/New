using System;
using System.Collections.Generic;
using System.Text;
using FoodLibrary.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace FoodLibrary.ViewModels
{
    public class MenuPage5ViewModel:ViewModelBase
    {
        /// <summary>
        /// 登录注销服务。
        /// </summary>
        private IOneDriveService _oneDriveService;

        /// <summary>
        /// 数据同步服务。
        /// </summary>
        private IMaintenanceService _maintenanceService;

        public MenuPage5ViewModel(IOneDriveService oneDriveService, 
            IMaintenanceService maintenanceService)
        {
            _oneDriveService = oneDriveService;
            _maintenanceService = maintenanceService;
        }

        /// <summary>
        /// 登录按钮绑定。
        /// </summary>
        private RelayCommand _logCommand;
        public RelayCommand LogCommand =>
            _logCommand ?? (_logCommand = 
                new RelayCommand(() => 
                {
                    _oneDriveService.SignInAsync();
                }));

        /// <summary>
        /// 注销按钮绑定。
        /// </summary>
        private RelayCommand _logoutCommand;
        public RelayCommand LogoutCommand =>
            _logoutCommand ?? (_logoutCommand = 
                new RelayCommand(() =>
                {
                    _oneDriveService.SignOutAsync();
                }));

        private RelayCommand _synCommand;

        public RelayCommand SynCommand =>
            _synCommand ?? (_synCommand = 
                new RelayCommand(() => 
                {
                    _maintenanceService.MaintenanceAsync();
                    _oneDriveService.SaveFoodWeightAsync(null);
                    _oneDriveService.SaveLogAsync(null);
                }));
    }
}
