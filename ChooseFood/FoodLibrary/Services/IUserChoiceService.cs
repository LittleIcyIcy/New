using FoodLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FoodLibrary.Services
{
    public interface IUserChoiceService
    {
        Task<List<FoodChoice>> ReadJsonAsync();

        void SaveJsonAsync(List<FoodChoice> userChoice);
    }
}
