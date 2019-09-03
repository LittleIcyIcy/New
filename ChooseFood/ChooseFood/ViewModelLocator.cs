using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChooseFood.Services.Impl;
using FoodLibrary.Services;
using FoodLibrary.Services.Impl;
using FoodLibrary.ViewModels;
using GalaSoft.MvvmLight.Ioc;

namespace ChooseFood
{
    public class ViewModelLocator
    {
        public MainPageViewModel MainPageViewModel =>
            SimpleIoc.Default.GetInstance<MainPageViewModel>();

        public MenuPage1ViewModel MenuPage1ViewModel =>
            SimpleIoc.Default.GetInstance<MenuPage1ViewModel>();

        public MenuPage2ViewModel MenuPage2ViewModel =>
            SimpleIoc.Default.GetInstance<MenuPage2ViewModel>();

        public MenuPage3ViewModel MenuPage3ViewModel =>
            SimpleIoc.Default.GetInstance<MenuPage3ViewModel>();

        public MenuPage4ViewModel MenuPage4ViewModel =>
            SimpleIoc.Default.GetInstance<MenuPage4ViewModel>();

        public MenuPage5ViewModel MenuPage5ViewModel =>
            SimpleIoc.Default.GetInstance<MenuPage5ViewModel>();

        public LikePageViewModel LikePageViewModel =>
            SimpleIoc.Default.GetInstance<LikePageViewModel>();

        public DislikePageViewModel DislikePageViewModel =>
            SimpleIoc.Default.GetInstance<DislikePageViewModel>();

        public ImagePageViewModel ImagePageViewModel =>
            SimpleIoc.Default.GetInstance<ImagePageViewModel>();
        public ViewModelLocator()
        {
            SimpleIoc.Default.Register<IUserChoiceService, UserChoiceService>();
            SimpleIoc.Default.Register<IUserFavorService, UserFavorService>();
            SimpleIoc.Default.Register<ILocationService, LocationService>();
            SimpleIoc.Default.Register<IWeatherService, WeatherService>();
            SimpleIoc.Default.Register<IRecommendationService, RecommendationService>();
            SimpleIoc.Default.Register<ILoadJsonService, LoadJsonService>();
            SimpleIoc.Default.Register<INavigationService, NavigationService>();
            SimpleIoc.Default.Register<ILogService,LogService>();
            SimpleIoc.Default.Register<IFoodFavorService, FoodFavorService>();
            SimpleIoc.Default.Register<ILastTimeCommitService,LastTimeCommitService>();
            SimpleIoc.Default.Register<IOneDriveService, OneDriveService>();
            SimpleIoc.Default.Register<IMaintenanceService,MaintenanceService>();

            SimpleIoc.Default.Register<MainPageViewModel>();
            SimpleIoc.Default.Register<MenuPage1ViewModel>();
            SimpleIoc.Default.Register<MenuPage2ViewModel>();
            SimpleIoc.Default.Register<MenuPage3ViewModel>();
            SimpleIoc.Default.Register<MenuPage4ViewModel>();
            SimpleIoc.Default.Register<MenuPage5ViewModel>();
            SimpleIoc.Default.Register<LikePageViewModel>();
            SimpleIoc.Default.Register<DislikePageViewModel>();
            SimpleIoc.Default.Register<ImagePageViewModel>();
        }
    }
}
