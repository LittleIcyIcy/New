using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChooseFood.Services.Impl;
using FoodLibrary.Services;
using FoodLibrary.ViewModels;
using GalaSoft.MvvmLight.Ioc;

namespace ChooseFood
{
    public class ViewModelLocator
    {
        public MainPageViewModel MainPageViewModel =>
            SimpleIoc.Default.GetInstance<MainPageViewModel>();

        public ViewModelLocator()
        {
            SimpleIoc.Default.Register<ILoadJsonService, LoadJsonService>();
            SimpleIoc.Default.Register<MainPageViewModel>();
        }
    }
}
