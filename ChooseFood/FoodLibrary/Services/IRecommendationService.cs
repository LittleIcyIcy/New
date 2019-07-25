using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FoodLibrary.Models;

namespace FoodLibrary.Services
{
    public interface IRecommendationService
    {
        Task<List<FoodInformation>> ReFlashAsync();
        List<FoodChoice> GetFoodChoices();
        List<int> GetCos(List<Vector> foodVector);

        List<Vector> getFoodVector(int temperature, int humidity);

        void InitWeight(List<FoodInformation> food_infs, List<UserFavorInformation> userfavor);

        int GetOneFoodNum(List<int> cos);
        List<FoodInformation> GetFoodInfs();
        void ChangeWeight(String food_name, List<int> reason, bool IsWriteToJson);

        void AddUserChoice(String userchoice);
        void SaveUserFavor();
        void SaveUserChoice();
    }
}
