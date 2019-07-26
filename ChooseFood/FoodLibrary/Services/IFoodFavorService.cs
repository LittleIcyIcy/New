using System;
using System.Collections.Generic;
using System.Text;

namespace FoodLibrary.Services
{
    public interface IFoodFavorService
    {
        void InitAsync();
        void ChangeWeight(int pos, List<int> changeWeight);
        void SaveChangeWeightAsync();

    }
}
