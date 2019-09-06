using System;
using System.Collections.Generic;
using System.Text;
using FoodLibrary.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace FoodLibrary.ViewModels
{
    public class DislikePageViewModel:ViewModelBase
    {
        private string _color1 = "White";
        private string _color2 = "White";
        private string _color3 = "White";
        private string _color4 = "White";
        private string _color5 = "White";

        private INavigationService _navigationService;

        private IRecommendationService _recommendationService;

        public DislikePageViewModel(INavigationService navigationService,
            IRecommendationService recommendationService)
        {
            _navigationService = navigationService;
            _recommendationService = recommendationService;
        }

        public string Color1
        {
            get => _color1;
            set => Set(nameof(Color1), ref _color1, value);
        }

        public string Color2
        {
            get => _color2;
            set => Set(nameof(Color2), ref _color2, value);
        }

        public string Color3
        {
            get => _color3;
            set => Set(nameof(Color3), ref _color3, value);
        }

        public string Color4
        {
            get => _color4;
            set => Set(nameof(Color4), ref _color4, value);
        }

        public string Color5
        {
            get => _color5;
            set => Set(nameof(Color5), ref _color5, value);
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

        private int[] _reasonList = { 0, 0, 0, 0, 0, 0 };

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
                    if (_reasonList[0] == 0)
                    {
                        _reasonList[0] = -1;
                        if (flag == -1)
                        {
                            flag = 0;
                            _color5 = "White";
                        }

                        if (_reasonList[2] == -1)
                        {
                            _reasonList[2] = 0;
                            _color2 = "White";
                        }
                        _color1 = "Black";
                    }
                    else
                    {
                        _reasonList[0] = 0;
                        _color1 = "White";
                    }
                }));

        public RelayCommand SecondCommand =>
            _secondCommand ?? (_secondCommand =
                new RelayCommand(() =>
                {
                    if (_reasonList[2] == 0)
                    {
                        _reasonList[2] = -1;
                        if (flag == -1)
                        {
                            flag = 0;
                            _color5 = "White";
                        }

                        if (_reasonList[0] == -1)
                        {
                            _reasonList[0] = 0;
                            _color1 = "White";
                        }
                        _color2 = "CadetBlue";
                    }
                    else
                    {
                        _reasonList[2] = 0;
                        _color2 = "White";
                    }
                }));

        public RelayCommand ThirdCommand =>
            _thirdCommand ?? (_thirdCommand =
                new RelayCommand(() =>
                {
                    if (_reasonList[5] == 0)
                    {
                        _reasonList[5] = -1;
                        if (flag == -1)
                        {
                            flag = 0;
                            _color5 = "White";
                        }

                        if (_reasonList[3] == -1)
                        {
                            _reasonList[3] = 0;
                            _color4 = "White";
                        }
                        _color3 = "CadetBlue";
                    }
                    else
                    {
                        _reasonList[5] = 0;
                        _color3 = "White";
                    }
                }));

        public RelayCommand ForthCommand =>
            _forthCommand ?? (_forthCommand =
                new RelayCommand(() =>
                {
                    if (_reasonList[3] == 0)
                    {
                        _reasonList[3] = -1;
                        if (flag == 1)
                        {
                            flag = 0;
                            _color5 = "White";
                        }

                        if (_reasonList[5] == -1)
                        {
                            _reasonList[5] = 0;
                            _color3 = "White";
                        }
                        _color4 = "CadetBlue";
                    }
                    else
                    {
                        _reasonList[3] = 0;
                        _color3 = "White";
                    }
                }));

        public RelayCommand FifthCommand =>
            _fifthCommand ?? (_fifthCommand =
                new RelayCommand(() =>
                {
                    if (flag == 0)
                    {
                        for (int i = 0; i < 6; i++)
                        {
                            _reasonList[i] = -1;
                        }
                        flag = 1;
                        _color5 = "CadetBlue";
                    }
                    else
                    {
                        for (int i = 0; i < 6; i++)
                        {
                            _reasonList[i] = 0;
                        }
                        flag = 0;
                        if (_color1 == "CadetBlue") { _color1 = "White"; }
                        if (_color2 == "CadetBlue") { _color2 = "White"; }
                        if (_color3 == "CadetBlue") { _color3 = "White"; }
                        if (_color4 == "CadetBlue") { _color4 = "White"; }
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
                    _navigationService.NavigateTo("今日推荐", null);
                    foreach (int i in _reasonList) _reasonList[i] = 0;
                }));
    }
}
