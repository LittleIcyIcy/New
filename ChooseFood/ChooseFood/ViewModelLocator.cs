﻿using System;
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

        public MenuPage1ViewModel MenuPage1ViewModel =>
            SimpleIoc.Default.GetInstance<MenuPage1ViewModel>();

        public MenuPage2ViewModel MenuPage2ViewModel =>
            SimpleIoc.Default.GetInstance<MenuPage2ViewModel>();

        public MenuPage3ViewModel MenuPage3ViewModel =>
            SimpleIoc.Default.GetInstance<MenuPage3ViewModel>();

        public MenuPage4ViewModel MenuPage4ViewModel =>
            SimpleIoc.Default.GetInstance<MenuPage4ViewModel>();

        public LikePageViewModel LikePageViewModel =>
            SimpleIoc.Default.GetInstance<LikePageViewModel>();

        public DislikePageViewModel DislikePageViewModel =>
            SimpleIoc.Default.GetInstance<DislikePageViewModel>();

        public ViewModelLocator()
        {
            SimpleIoc.Default.Register<ILoadJsonService, LoadJsonService>();
            SimpleIoc.Default.Register<MainPageViewModel>();
            SimpleIoc.Default.Register<MenuPage1ViewModel>();
            SimpleIoc.Default.Register<MenuPage2ViewModel>();
            SimpleIoc.Default.Register<MenuPage3ViewModel>();
            SimpleIoc.Default.Register<MenuPage4ViewModel>();
            SimpleIoc.Default.Register<LikePageViewModel>();
            SimpleIoc.Default.Register<DislikePageViewModel>();
        }
    }
}
