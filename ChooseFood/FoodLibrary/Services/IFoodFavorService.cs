using System;
using System.Collections.Generic;
using System.Text;
using FoodLibrary.Models;

namespace FoodLibrary.Services
{
    public interface IFoodFavorService
    {
        void InitAsync(List<FoodInformation> foodInformationList);
        void ChangeWeight(int pos, List<int> changeWeight);
        void SaveChangeWeightAsync();

        List<FoodWeightChange> GetFoodWeightChanges();
        void SetWeight(int i, List<int> tmpWeightChangeList);
    }
}
