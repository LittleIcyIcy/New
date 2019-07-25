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
        /// 位置服务。
        /// </summary>
        private ILocationService _locationService;

        /// <summary>
        /// 天气服务。
        /// </summary>
        private IWeatherService _weatherService;


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
            INavigationService navigationService)
        {
            _locationService = locationService;
            _weatherService = weatherService;
            _navigationService = navigationService;
        }

        public Weather WeatherRoot;

        /// <summary>
        /// 天气刷新按钮。
        /// </summary>
        private RelayCommand _refreshCommand;
        public RelayCommand RefreshCommand =>
            _refreshCommand ?? (_refreshCommand
            = new RelayCommand(async () =>
            {
                _weatherRootData = await _weatherService.GetWeatherAsync();
                Temperature = _weatherRootData.main.temp + "℃";
                Humidity = _weatherRootData.main.humidity;
                Site = _weatherRootData.sys.country;
            }));


        private RelayCommand<string> _navigationCommand;
        private RelayCommand<INavigationService> _navigationRelayCommand;

        /// <summary>
        /// 接收点击的页面。
        /// </summary>
        public RelayCommand<string> NavigationCommand =>
            _navigationCommand ??
            (_navigationCommand =
                new RelayCommand<string>((s) =>
                {
                    _navigationService.NavigateTo(s);
                }));

    }
}
