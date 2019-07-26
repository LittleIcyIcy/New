using FoodLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FoodLibrary.Services
{
    public interface IOneDriveService
    {
        void SignInAsync();
        void SignOutAsync();
        Task<bool> SignSituationAsync();
        void SaveLogAsync(List<Log> LogList);
        Task<List<Log>> LoadLogAsync();
        void SaveFoodWeightAsync(List<FoodWeightChange> FoodWeight);
        Task<List<FoodWeightChange>> LoadFoodWeightAsync();

    }

}
