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
    public class MenuPage3ViewModel:ViewModelBase
    {
        private IRecommendationService _recommendationService;

        public MenuPage3ViewModel(IRecommendationService recommendationService)
        {
            _recommendationService = recommendationService;
        }

        private RelayCommand _getCommand;
        public RelayCommand GetCommand =>
            _getCommand ?? (_getCommand
                = new RelayCommand(async () =>
                {
                    AllFoodInformationsCollection.Clear();
                    AllFoodInformationsCollection.AddRange(_recommendationService.GetFoodInfs());
                }));

        public ObservableRangeCollection<FoodInformation> AllFoodInformationsCollection { get; } =
            new ObservableRangeCollection<FoodInformation>();
    }
}
