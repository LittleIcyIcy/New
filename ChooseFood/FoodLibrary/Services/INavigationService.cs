using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Controls;

namespace FoodLibrary.Services
{
    public interface INavigationService
    {
        void SetFrame(Frame frame);
        void NavigateTo(string pageName);
    }
}
