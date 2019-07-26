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

        public LikePageViewModel(INavigationService navigationService) {
            _navigationService = navigationService;
        }

        private RelayCommand _firstCommand;
        private RelayCommand _secondCommand;
        private RelayCommand _thirdCommand;
        private RelayCommand _forthCommand;
        private RelayCommand _fifthCommand;

        private int [] _reasonList = {0,0,0,0,0};

        public RelayCommand FirstCommand =>
            _firstCommand ?? (_firstCommand = 
                new RelayCommand(() =>
                {
                    if (_reasonList[4] == 1) {
                        _reasonList[0] = 0;
                        //还要有改变button颜色的语句。
                    }
                    else {
                        if (_reasonList[0] == 0)
                            _reasonList[0] = 1;
                        //还要有改变button颜色的语句。
                        else
                        {
                            _reasonList[0] = 0;
                            //还要有改变button颜色的语句。
                        }
                    }
                }));

        public RelayCommand SecondCommand =>
            _secondCommand ?? (_secondCommand =
                new RelayCommand(() =>
                {
                    if (_reasonList[4] == 1)
                    {
                        _reasonList[1] = 0;
                        //还要有改变button颜色的语句。
                    }
                    else
                    {
                        if (_reasonList[1] == 0)
                            _reasonList[1] = 1;
                        //还要有改变button颜色的语句。
                        else
                        {
                            _reasonList[1] = 0;
                            //还要有改变button颜色的语句。
                        }
                    }
                }));

        public RelayCommand ThirdCommand =>
            _thirdCommand ?? (_thirdCommand =
                new RelayCommand(() =>
                {
                    if (_reasonList[4] == 1)
                    {
                        _reasonList[2] = 0;
                        //还要有改变button颜色的语句。
                    }
                    else
                    {
                        if (_reasonList[2] == 0)
                            _reasonList[2] = 1;
                        //还要有改变button颜色的语句。
                        else
                        {
                            _reasonList[2] = 0;
                            //还要有改变button颜色的语句。
                        }
                    }
                }));

        public RelayCommand ForthCommand =>
            _forthCommand ?? (_forthCommand =
                new RelayCommand(() =>
                {
                    if (_reasonList[4] == 1)
                    {
                        _reasonList[3] = 0;
                        //还要有改变button颜色的语句。
                    }
                    else
                    {
                        if (_reasonList[3] == 0)
                            _reasonList[3] = 1;
                        //还要有改变button颜色的语句。
                        else
                        {
                            _reasonList[3] = 0;
                            //还要有改变button颜色的语句。
                        }
                    }
                }));

        public RelayCommand FifthCommand =>
            _fifthCommand ?? (_fifthCommand =
                new RelayCommand(() =>
                {
                    if (_reasonList[4] == 0) {
                        _reasonList = new[] {0, 0, 0, 0, 1};
                        //颜色变化
                    }
                    else {
                        _reasonList[4] = 0;
                        //颜色
                    }
                }));

        private RelayCommand _confirmCommand;

        public RelayCommand ConfirmCommand =>
            _confirmCommand ?? (_confirmCommand =
                new RelayCommand(() => 
                {
                    _navigationService.NavigateTo("今日推荐");

                    //这个_reasonList要传给谁？
                }));
    }
}
