using System;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using System.Security.Authentication;
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

        /// <summary>
        /// 接收随机生成的推荐菜单
        /// </summary>
        private List<FoodInformation> _foodInformations = new List<FoodInformation>();

        private int flag = 0;

        public MenuPage1ViewModel(IRecommendationService recommendationService
            , INavigationService navigationService)
        {
            _recommendationService = recommendationService;
            _navigationService = navigationService;
        }

        /// <summary>
        /// 跟页面的Loaded函数绑定。
        /// </summary>
        /// <returns></returns>
        public async System.Threading.Tasks.Task ShowAsync()
        {
            if (flag == 0) {
                _foodInformations = await _recommendationService.ReFlashAsync();
                exchange(_foodInformations);
                flag = 1;
            }
            else {
                return;
            }
        }

        /// <summary>
        /// 刷新按钮的绑定。
        /// </summary>
        public RelayCommand RecommendationCommand =>
            _recommendationCommand ?? (_recommendationCommand
                = new RelayCommand(async () =>
                {
                    _foodInformations = await _recommendationService.ReFlashAsync();
                    exchange(_foodInformations);
                }));
        private RelayCommand _recommendationCommand;

        /// <summary>
        /// 所推荐的菜品的名称列表。
        /// </summary>
        public ObservableRangeCollection<FilterViewModel> FoodInformationCollection { get; } =
            new ObservableRangeCollection<FilterViewModel>();

        /// <summary>
        /// 将FoodInformation包装成FilterViewModel
        /// </summary>
        /// <param name="foodInformations"></param>
        private void exchange(List<FoodInformation> foodInformations) {
            FoodInformationCollection.Clear();
            foreach (var foodInformation in foodInformations) {
                FilterViewModel filterViewModel = new FilterViewModel(_navigationService);
                filterViewModel.FoodInformation = foodInformation;
                FoodInformationCollection.Add(filterViewModel);
            }
        }






        public class FilterViewModel :ViewModelBase
        {
            private INavigationService _navigationService;

            private FoodInformation _foodInformation;
            //还应该有个颜色的成员变量

            /// <summary>
            /// 构造函数
            /// </summary>
            /// <param name="menuPage1ViewModel"></param>
            public FilterViewModel(INavigationService navigationService)
            {
                _navigationService = navigationService;
            }

            public FilterViewModel() { }

            public FoodInformation FoodInformation
            {
                get => _foodInformation;
                set =>
                    Set(nameof(FoodInformation), ref _foodInformation, value);
            }

            private int flag_zan = 0;
            private int flag_cai = 0;

            /// <summary>
            /// 点赞的命令绑定。
            /// </summary>
            private RelayCommand _zanCommand;

            public RelayCommand ZanCommand =>
                _zanCommand ?? (_zanCommand = 
                    new RelayCommand(() => 
                    {
                        if (flag_zan == 0) {
                            _navigationService.NavigateTo("LikePage",_foodInformation.Name);
                            flag_zan = 1;
                            flag_cai = 0;
                            //改变button的颜色
                        }
                    }));

            /// <summary>
            /// 点踩按钮的绑定。
            /// </summary>
            private RelayCommand _caiCommand;

            public RelayCommand CaiCommand =>
                _caiCommand ?? (_caiCommand = 
                    new RelayCommand(() => {
                        if (flag_cai == 0) {
                            _navigationService.NavigateTo("DislikePage",_foodInformation.Name);
                            flag_cai = 1;
                            flag_zan = 0;
                        }
                    }));

        }

    }
}
