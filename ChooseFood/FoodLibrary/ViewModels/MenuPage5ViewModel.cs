using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
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

        private INavigationService _navigationService;
        public MenuPage5ViewModel(IOneDriveService oneDriveService, 
            IMaintenanceService maintenanceService,INavigationService navigationService)
        {
            _oneDriveService = oneDriveService;
            _maintenanceService = maintenanceService;
            _navigationService = navigationService;
        }

        /// <summary>
        /// 登录按钮绑定。
        /// </summary>
        private RelayCommand _logCommand;
        public RelayCommand LogCommand =>
            _logCommand ?? (_logCommand = 
                new RelayCommand(async () => 
                {
                    bool Flag = await _oneDriveService.SignSituationAsync();
                    if(Flag == false)
                    {
                        _oneDriveService.SignInAsync();
                    }
                    else
                    {
                        //显示已经登陆
                    }
                }));

        /// <summary>
        /// 注销按钮绑定。
        /// </summary>
        private RelayCommand _logoutCommand;
        public RelayCommand LogoutCommand =>
            _logoutCommand ?? (_logoutCommand = 
                new RelayCommand(async () =>
                {
                    bool Flag = await _oneDriveService.SignSituationAsync();
                    if(Flag == true)
                    {
                        _oneDriveService.SignOutAsync();
                    }
                    else
                    {
                        //显示还未登录
                    }
                }));
        /// <summary>
        /// 同步按钮绑定。
        /// </summary>
        private RelayCommand _synCommand;

        public RelayCommand SynCommand =>
            _synCommand ?? (_synCommand = 
                new RelayCommand(async () => 
                {
                    bool Flag = await _oneDriveService.SignSituationAsync();
                    if(Flag == true)
                    {
                        await _maintenanceService.MaintenanceAsync();
                        _oneDriveService.SaveFoodWeightAsync();
                        _oneDriveService.SaveLogAsync();
                    }
                    else
                    {
                        //显示还未登陆，请先登录
                    }
                }));
    }
}
