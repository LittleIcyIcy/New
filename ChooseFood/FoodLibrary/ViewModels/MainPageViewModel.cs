using System;
using System.Collections.Generic;
using System.Text;
using FoodLibrary.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace FoodLibrary.ViewModels
{
    public class MainPageViewModel:ViewModelBase
    {
        /// <summary>
        /// 登录注销服务。
        /// </summary>
        private IOneDriveService _oneDriveService;

        /// <summary>
        /// 数据同步服务。
        /// </summary>
        private IMaintenanceService _maintenanceService;

        /// <summary>
        /// 位置服务。
        /// </summary>
        private ILocationService _locationService;

        /// <summary>
        /// 天气服务。
        /// </summary>
        private IWeatherService _weatherService;

        /// <summary>
        /// 推荐服务。
        /// </summary>
        private IRecommendationService _recommendationService;

        /// <summary>
        /// 所有的天气数据。
        /// </summary>
        private WeatherRoot _weatherRootData;

        /// <summary>
        /// 导航服务。
        /// </summary>
        private INavigationService _navigationService;

        /// <summary>
        /// 温度。
        /// </summary>
        public string Temperature
        {
            get => _temperature;
            set => Set(nameof(Temperature), ref _temperature, value);
        }
        private string _temperature;

        /// <summary>
        /// 湿度。
        /// </summary>
        public string Humidity
        {
            get => _humidity;
            set => Set(nameof(Humidity), ref _humidity, value);
        }
        private string _humidity;

        /// <summary>
        /// 天气状态。
        /// </summary>
        public string Description;

        /// <summary>
        /// 位置。
        /// </summary>
        public string Site
        {
            get => _site;
            set => Set(nameof(Site), ref _site, value);
        }
        private string _site;

        public MainPageViewModel(ILocationService locationService,
            IWeatherService weatherService,
            IRecommendationService recommendationService,
            INavigationService navigationService,IOneDriveService oneDriveService,
            IMaintenanceService maintenanceService)
        {
            _locationService = locationService;
            _weatherService = weatherService;
            _navigationService = navigationService;
            _recommendationService = recommendationService;
            _oneDriveService = oneDriveService;
            _maintenanceService = maintenanceService;
        }

        public Weather WeatherRoot;

        /// <summary>
        /// 显示天气。
        /// </summary>
        /// <returns></returns>
        public async System.Threading.Tasks.Task RefreshWeatherAsync() {
            _weatherRootData = await _weatherService.GetWeatherAsync();
            Temperature = _weatherRootData.main.temp + "℃";
            Humidity = _weatherRootData.main.humidity;
            Site = _weatherRootData.sys.country;
        }

        private RelayCommand<string> _navigationCommand;

        /// <summary>
        /// 接收点击的页面。
        /// </summary>
        public RelayCommand<string> NavigationCommand =>
            _navigationCommand ??
            (_navigationCommand =
                new RelayCommand<string>((s) =>
                {
                    _navigationService.NavigateTo(s,null);
                }));


        private RelayCommand _toFirstCommand;
        public RelayCommand ToFirstCommand =>
            _toFirstCommand ?? (_toFirstCommand = 
                new RelayCommand(() => {
                    _recommendationService.InitRecommendationAsync();
                    RefreshWeatherAsync();
                    _navigationService.NavigateTo("ImagePage",null);
                }));

        /// <summary>
        /// 登录按钮绑定。
        /// </summary>
        private RelayCommand _logCommand;
        public RelayCommand LogCommand =>
            _logCommand ?? (_logCommand =
                new RelayCommand(async () =>
                {
                    bool Flag = await _oneDriveService.SignSituationAsync(true);
                    if (Flag == false)
                    {
                        _oneDriveService.SignInAsync();
                    }
                    else
                    {
                        _navigationService.NavigateTo("LoginPromptPage", "亲，您已经登陆过了呢！");
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
                    if (Flag == true)
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
                    if (Flag == true)
                    {
                        await _maintenanceService.MaintenanceAsync();
                        _oneDriveService.SaveFoodWeightAsync();
                        _oneDriveService.SaveLogAsync();
                        //加一个同步成功的窗口
                    }
                    else
                    {
                        _navigationService.NavigateTo("LoginPromptPage", "亲，您还没有登陆呢，这边建议您登一下呢，手动微笑！");
                    }
                }));
    }
}
