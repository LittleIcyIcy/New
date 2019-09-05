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
                    bool Flag = await _oneDriveService.SignSituationAsync(true);
                    if(Flag == false)
                    {
                        _oneDriveService.SignInAsync();
                    }
                    else
                    {
                        _navigationService.NavigateTo("LoginPromptPage","亲，您已经登陆过了呢！");
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
                    bool Flag = await _oneDriveService.SignSituationAsync(false);
                    if(Flag == true)
                    {
                        _oneDriveService.SignOutAsync();
                    }
                    else
                    {
                        _navigationService.NavigateTo("LoginPromptPage", "亲，您还没有登陆呢，这边建议您登一下呢，手动微笑！");
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
                    bool Flag = await _oneDriveService.SignSituationAsync(false);
                    if(Flag == true)
                    {
                        await _maintenanceService.MaintenanceAsync();
                        _oneDriveService.SaveFoodWeightAsync();
                        _oneDriveService.SaveLogAsync();
                    }
                    else
                    {
                        _navigationService.NavigateTo("LoginPromptPage", "亲，您还没有登陆呢，这边建议您登一下呢，手动微笑！");
                    }
                }));
    }
}
