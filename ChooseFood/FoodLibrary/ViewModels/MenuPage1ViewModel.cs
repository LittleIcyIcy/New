using System;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using System.Text;
using FoodLibrary.Models;
using FoodLibrary.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MvvmHelpers;

namespace FoodLibrary.ViewModels
{
    public class MenuPage1ViewModel:ViewModelBase
    {
        /// <summary>
        /// 推荐服务。
        /// </summary>
        private IRecommendationService _recommendationService;

        /// <summary>
        /// 页面导航服务。
        /// </summary>
        private INavigationService _navigationService;

        public MenuPage1ViewModel(IRecommendationService recommendationService
            , INavigationService navigationService)
        {
            _recommendationService = recommendationService;
            _navigationService = navigationService;
        }


        /// <summary>
        /// 刷新按钮的绑定。
        /// </summary>
        public RelayCommand RecommendationCommand =>
            _recommendationCommand ?? (_recommendationCommand
                = new RelayCommand(async () =>
                {
                    _foodInformationCollection.Clear();
                    _foodInformationCollection.AddRange(await _recommendationService.ReFlashAsync());
                }));
        private RelayCommand _recommendationCommand;

        /// <summary>
        /// 所推荐的菜品的小ViewModel列表。
        /// </summary>
        public ObservableRangeCollection<FilterViewModel> FoodInformationCollection
        {
            get;
            set;
        } =
            new ObservableRangeCollection<FilterViewModel>();

        private ObservableRangeCollection<FoodInformation> _foodInformationCollection =
            new ObservableRangeCollection<FoodInformation>();


    }
}
