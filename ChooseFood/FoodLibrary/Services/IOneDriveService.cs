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
        Task<bool> SignSituationAsync(bool flagSignIn);
        void SaveLogAsync();
        Task<List<Log>> LoadLogAsync();
        void SaveFoodWeightAsync();
        Task<List<FoodWeightChange>> LoadFoodWeightAsync();

    }

}
