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

        private List<string> _flagList = new List<string> { "False", "true", "true", "true", "true" };

        public List<string> FlagList
        {
            get => _flagList;
            set => Set(nameof(FlagList),ref _flagList, value);
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

        private RelayCommand _firstCheckCommand;
        public RelayCommand FirstCheckCommand =>
            _firstCheckCommand ?? (_firstCheckCommand = 
                new RelayCommand(() =>
                {
                    _reasonList[0] = 1;
                    _reasonList[2] = 0;
                }));

        private RelayCommand _secondCheckCommand;
        public RelayCommand SecondCheckCommand =>
            _secondCheckCommand ?? (_secondCheckCommand =
                new RelayCommand(() =>
                {
                    _reasonList[0] = 0;
                    _reasonList[2] = 1;
                }));

        private RelayCommand _thirdCheckCommand;
        public RelayCommand ThirdCheckCommand =>
            _thirdCheckCommand ?? (_thirdCheckCommand =
                new RelayCommand(() =>
                {
                    _reasonList[5] = 1;
                    _reasonList[3] = 0;
                }));

        private RelayCommand _forthCheckCommand;
        public RelayCommand ForthCheckCommand =>
            _forthCheckCommand ?? (_forthCheckCommand =
                new RelayCommand(() =>
                {
                    _reasonList[5] = 0;
                    _reasonList[3] = 1;
                }));

        private RelayCommand _fifthCheckCommand;
        public RelayCommand FifthCheckCommand =>
            _fifthCheckCommand ?? (_fifthCheckCommand =
                new RelayCommand(() =>
                {
                    for (int i = 0; i < 4; i++)
                        _flagList[i] = "false";
                    for (int i = 0; i < 6; i++)
                        _reasonList[i] = 1;
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
                    for (int i = 0; i < 6; i++)
                        _reasonList[i] = 0;
                }));

        /// <summary>
        /// 返回按钮的绑定。
        /// </summary>
        private RelayCommand _backCommand;
        public RelayCommand BackCommand =>
            _backCommand ?? (_backCommand =
                new RelayCommand(() =>
                {
                    foreach (int i in _reasonList) _reasonList[i] = 0;
                    _navigationService.NavigateTo("今日推荐", null);
                }));

    }
}
