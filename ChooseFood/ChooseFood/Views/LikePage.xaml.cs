﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using FoodLibrary.ViewModels;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace ChooseFood.Views
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class LikePage : Page
    {
        public LikePage()
        {
            this.InitializeComponent();
        }

        private void LikePage_OnLoaded(object sender, RoutedEventArgs e) {
            ((LikePageViewModel) DataContext).ReceiveFoodNameCommand.Execute(null);
        }

        private void One_Checked(object sender, RoutedEventArgs e)
        {
            ((LikePageViewModel)DataContext).FirstCheckCommand.Execute(null);
        }

        private void Two_Checked(object sender, RoutedEventArgs e)
        {
            ((LikePageViewModel)DataContext).SecondCheckCommand.Execute(null);
        }

        private void Three_Checked(object sender, RoutedEventArgs e)
        {
            ((LikePageViewModel)DataContext).ThirdCheckCommand.Execute(null);
        }

        private void Four_Checked(object sender, RoutedEventArgs e)
        {
            ((LikePageViewModel)DataContext).ForthCheckCommand.Execute(null);
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            ((LikePageViewModel)DataContext).FifthCheckCommand.Execute(null);
        }
    }
}
