using System;
using System.Collections.Generic;
using System.Text;

namespace FoodLibrary.Services
{
    public interface INavigationService
    {
        void NavigateTo(string pageName);
    }
}
