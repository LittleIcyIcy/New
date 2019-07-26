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
        /// 用于标志，同一道菜不能既赞又踩。
        /// </summary>
        private int[] flag = {0,0,0,0,0};

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
        /// 第一个点赞按钮。
        /// </summary>
        public RelayCommand ZanCommand1 =>
            _zanCommand1 ?? (_zanCommand1 = new RelayCommand(() =>
            {
                if (flag[0] == 0)
                {
                    _navigationService.NavigateTo("LikePage");
                    flag[0] = 1;
                }
            }));
        private RelayCommand _zanCommand1;

        /// <summary>
        /// 第一个踩的按钮。
        /// </summary>
        public RelayCommand CaiCommand1 =>
            _caiCommand1 ?? (_caiCommand1 = new RelayCommand(() =>
            {
                if (flag[0] == 0)
                {
                    _navigationService.NavigateTo("DislikePage");
                    flag[0] = 2;
                }

            }));
        private RelayCommand _caiCommand1;








        public class FilterViewModel :ViewModelBase
        {
            /// <summary>
            /// 父ViewModel
            /// </summary>
            private MenuPage1ViewModel _menuPage1ViewModel;

            private INavigationService _navigationService;

            private FoodInformation _foodInformation;
            //还应该有个颜色的成员变量

            /// <summary>
            /// 构造函数
            /// </summary>
            /// <param name="menuPage1ViewModel"></param>
            public FilterViewModel(MenuPage1ViewModel menuPage1ViewModel,
                INavigationService navigationService)
            {
                _menuPage1ViewModel = menuPage1ViewModel;
                _navigationService = navigationService;
            }

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
                        if (flag_zan == 1)//连续点击两下会取消
                        {
                            flag_zan = 0;
                            //把原来的原因给取消了
                        }
                        
                    }));
        }

    }
}
