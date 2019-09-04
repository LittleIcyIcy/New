using FoodLibrary.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodLibrary.ViewModels
{
    public class LoginPromptPageViewModel:ViewModelBase
    {
        private INavigationService _navigationService;

        /// <summary>
        /// 提示内容。
        /// </summary>
        private string _promptContent;

        public LoginPromptPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public string PromptContent
        {
            get => _promptContent;
            set => Set(nameof(PromptContent), ref _promptContent, value);
        }

        /// <summary>
        /// 返回同步界面。
        /// </summary>
        private RelayCommand _returnCommand;
        public RelayCommand ReturnCommand => _returnCommand ??
            (_returnCommand = new RelayCommand(() =>
            {
                _navigationService.NavigateTo("数据同步", null);
            }));

        /// <summary>
        /// Loaded绑定的用于显示提示内容。
        /// </summary>
        private RelayCommand _promptContentCommand;
        public RelayCommand PromptContentCommand => _promptContentCommand ??
            (_promptContentCommand = new RelayCommand( () => {
                _promptContent = _navigationService.FoodName();
            }));

    }
}
