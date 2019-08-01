using System;
using System.Collections.Generic;
using System.Text;
using FoodLibrary.Models;

namespace FoodLibrary.Services.Impl
{
    public class FoodFavorService: IFoodFavorService
    {
        private IUserFavorService _userFavorService;
        public List<FoodWeightChange> FoodWeightChangesList;
        public FoodFavorService(IUserFavorService foodFavorService)
        {
            _userFavorService = foodFavorService;
        }

        public async void InitAsync(List<FoodInformation> foodInformations)
        {
            FoodWeightChangesList = await _userFavorService.ReadJsonAsync();
            if (FoodWeightChangesList.Count == 0)
            {
                FoodWeightChangesList = new List<FoodWeightChange>();
                for (int i = 0; i < foodInformations.Count; i++)
                {
                    FoodWeightChange foodchange = new FoodWeightChange();
                    foodchange.FoodName = foodInformations[i].Name;
                    foodchange.weightChangeList = new List<int>() {0,0,0,0,0,0};
                    FoodWeightChangesList.Add(foodchange);
                }
            }
        }

        public void ChangeWeight(int pos, List<int> changeWeight)
        {
            for (int i = 0; i < changeWeight.Count; i++)
            {
                FoodWeightChangesList[pos].weightChangeList[i] =
                    FoodWeightChangesList[pos].weightChangeList[i] + changeWeight[i];
            }

            return;
        }

        public void SetWeight(int pos, List<int> changeWeight)
        {
            FoodWeightChangesList[pos].weightChangeList = changeWeight;
            return;
        }
        public async void SaveChangeWeightAsync()
        {
            _userFavorService.SaveJsonAsync(FoodWeightChangesList);
        }

        public List<FoodWeightChange> GetFoodWeightChanges()
        {
            return FoodWeightChangesList;
        }

        public void SetFoodWeightChanges(List<FoodWeightChange> foodWeightChanges)
        {
            FoodWeightChangesList = foodWeightChanges;
        }

    }
}
