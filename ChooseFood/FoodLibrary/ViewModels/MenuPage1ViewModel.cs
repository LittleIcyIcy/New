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
                    FoodInformationCollection.Clear();
                    FoodInformationCollection.AddRange(await _recommendationService.ReFlashAsync());
                }));
        private RelayCommand _recommendationCommand;

        /// <summary>
        /// 所推荐的菜品的名称列表。
        /// </summary>
        public ObservableRangeCollection<FoodInformation> FoodInformationCollection { get; } =
            new ObservableRangeCollection<FoodInformation>();

        /// <summary>
        /// 点赞按钮。
        /// </summary>
        public RelayCommand ZanCommand =>
            _zanCommand ?? (_zanCommand = new RelayCommand(() =>
            {
                _navigationService.NavigateTo("LikePage");
            }));
        private RelayCommand _zanCommand;

        /// <summary>
        /// 踩的按钮。
        /// </summary>
        public RelayCommand CaiCommand =>
            _caiCommand ?? (_caiCommand = new RelayCommand(() => 
            {
                _navigationService.NavigateTo("DislikePage");
            }));
        private RelayCommand _caiCommand;
    }
}
