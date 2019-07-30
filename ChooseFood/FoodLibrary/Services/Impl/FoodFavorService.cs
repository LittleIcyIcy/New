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

        public async void InitAsync()
        {
            FoodWeightChangesList = await _userFavorService.ReadJsonAsync();
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
