using System;
using System.Collections.Generic;
using System.Text;

namespace FoodLibrary.Services
{
    public interface INavigationService {
        string FoodName();
        void NavigateTo(string pageName,string foodName);
    }
}
