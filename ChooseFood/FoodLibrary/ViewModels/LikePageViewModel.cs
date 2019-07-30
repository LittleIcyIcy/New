using System;
using System.Collections.Generic;
using System.Text;
using FoodLibrary.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace FoodLibrary.ViewModels
{
    public class LikePageViewModel:ViewModelBase
    {
        private INavigationService _navigationService;

        private IRecommendationService _recommendationService;

        public LikePageViewModel(INavigationService navigationService,
            IRecommendationService recommendationService) {
            _navigationService = navigationService;
            _recommendationService = recommendationService;
        }

        private string _foodName;

        /// <summary>
        /// 导航至当前页面时，接受相对应的菜名
        /// </summary>
        private RelayCommand _receiveFoodNameCommand;
        public RelayCommand ReceiveFoodNameCommand =>
            _receiveFoodNameCommand ?? (_receiveFoodNameCommand =
                new RelayCommand(() => {
                    _foodName = _navigationService.FoodName();
                }));

        private int[] _reasonList = {0, 0, 0, 0, 0, 0};

        private RelayCommand _firstCommand;
        private RelayCommand _secondCommand;
        private RelayCommand _thirdCommand;
        private RelayCommand _forthCommand;
        private RelayCommand _fifthCommand;

        /// <summary>
        /// 标志是否为无理由的喜欢或者不喜欢。
        /// </summary>
        private int flag = 0;

        public RelayCommand FirstCommand =>
            _firstCommand ?? (_firstCommand = 
                new RelayCommand(() =>
                {
                    if (_reasonList[0] == 0) {
                        _reasonList[0] = 1;
                        if (flag == 1) {
                            flag = 0;
                            //改变没有原因的那个button的颜色
                        }

                        if (_reasonList[2] == 1) {
                            _reasonList[2] = 0;
                            //改变这个按钮颜色
                        }
                        //改变按钮颜色
                    }
                    else {
                        _reasonList[0] = 0;
                        //改变按钮颜色
                    }
                }));

        public RelayCommand SecondCommand =>
            _secondCommand ?? (_secondCommand =
                new RelayCommand(() =>
                {
                    if (_reasonList[2] == 0)
                    {
                        _reasonList[2] = 1;
                        if (flag == 1)
                        {
                            flag = 0;
                            //改变没有原因的那个button的颜色
                        }

                        if (_reasonList[0] == 1)
                        {
                            _reasonList[0] = 0;
                            //改变这个按钮颜色
                        }
                        //改变按钮颜色
                    }
                    else
                    {
                        _reasonList[2] = 0;
                        //改变按钮颜色
                    }
                }));

        public RelayCommand ThirdCommand =>
            _thirdCommand ?? (_thirdCommand =
                new RelayCommand(() =>
                {
                    if (_reasonList[5] == 0)
                    {
                        _reasonList[5] = 1;
                        if (flag == 1)
                        {
                            flag = 0;
                            //改变没有原因的那个button的颜色
                        }

                        if (_reasonList[3] == 1)
                        {
                            _reasonList[3] = 0;
                            //改变这个按钮颜色
                        }
                        //改变按钮颜色
                    }
                    else
                    {
                        _reasonList[5] = 0;
                        //改变按钮颜色
                    }
                }));

        public RelayCommand ForthCommand =>
            _forthCommand ?? (_forthCommand =
                new RelayCommand(() =>
                {
                    if (_reasonList[3] == 0)
                    {
                        _reasonList[3] = 1;
                        if (flag == 1)
                        {
                            flag = 0;
                            //改变没有原因的那个button的颜色
                        }

                        if (_reasonList[5] == 1)
                        {
                            _reasonList[5] = 0;
                            //改变这个按钮颜色
                        }
                        //改变按钮颜色
                    }
                    else
                    {
                        _reasonList[3] = 0;
                        //改变按钮颜色
                    }
                }));

        public RelayCommand FifthCommand =>
            _fifthCommand ?? (_fifthCommand =
                new RelayCommand(() =>
                {
                    if (flag == 0) {
                        for (int i = 0; i < 6; i++) {
                            _reasonList[i] = 1;
                        }
                        flag = 1;
                        //改变按钮颜色
                    }
                    else {
                        for (int i = 0; i < 6; i++)
                        {
                            _reasonList[i] = 0;
                        }
                        flag = 0;
                        //改变按钮颜色
                    }
                }));

        /// <summary>
        /// 确定按钮的绑定。
        /// </summary>
        private RelayCommand _confirmCommand;

        public RelayCommand ConfirmCommand =>
            _confirmCommand ?? (_confirmCommand =
                new RelayCommand(() => 
                {
                    List<int> reasonList = new List<int>(_reasonList);
                    _recommendationService.ChangeWeight(_foodName, reasonList, true);
                    _navigationService.NavigateTo("今日推荐",null);
                }));
    }
}
