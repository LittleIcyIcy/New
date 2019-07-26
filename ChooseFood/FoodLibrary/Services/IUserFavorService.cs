using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FoodLibrary.Models;

namespace FoodLibrary.Services
{
    public interface IUserFavorService
    {
        Task<List<FoodWeightChange>> ReadJsonAsync();

        void SaveJsonAsync(List<FoodWeightChange> userChoiceInformation);
    }
}
