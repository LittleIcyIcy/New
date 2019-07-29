using System;
using System.Collections.Generic;
using System.Text;
using FoodLibrary.Models;
using FoodLibrary.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace FoodLibrary.ViewModels
{
    public class FilterViewModel:ViewModelBase
    {
        /// <summary>
        /// 父ViewModel
        /// </summary>
        private MenuPage1ViewModel _menuPage1ViewModel;

        private INavigationService _navigationService;

        public FilterViewModel(MenuPage1ViewModel menuPage1ViewModel, INavigationService navigationService)
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
        private FoodInformation _foodInformation;

        private int _flagZan = 0;
        private int _flagCai = 0;
        /// <summary>
        /// 点赞按钮的绑定。
        /// </summary>
        public RelayCommand<int> ZanCommand =>
            _zanCommand ?? (_zanCommand = new RelayCommand<int>(
                (i) => {
                    ButtonChange(i);
                }));
        private RelayCommand<int> _zanCommand;

        /// <summary>
        /// 踩按钮绑定。
        /// </summary>
        public RelayCommand<int> CaiCommand =>
            _caiCommand ?? (_caiCommand = new RelayCommand<int>(
                (i) => {
                    ButtonChange(i);
                }));
        private RelayCommand<int> _caiCommand;

        /// <summary>
        /// 按钮被click后
        /// </summary>
        /// <param name="flag">代表是哪个按钮被点击了</param>
        private void ButtonChange(int flag)
        {
            if (flag == 1)//赞
            {
                if (_flagZan == 0)
                {
                    _navigationService.NavigateTo("LikePage");
                    _flagZan = 1;
                    //改变zan按钮的颜色
                    if (_flagCai == 1)
                    {
                        _flagCai = 0;
                    }
                }
                else
                {
                    return;
                }
            }
            else
            {
                if (_flagCai == 0)
                {
                    _navigationService.NavigateTo("DislikePage");
                    _flagCai = 1;
                    //改变cai按钮颜色
                    if (_flagZan == 1)
                    {
                        _flagZan = 0;
                    }
                }
                else
                {
                    return;
                }
            }
        }

    }
}
