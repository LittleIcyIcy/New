using System;
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
using ChooseFood.Services.Impl;
using FoodLibrary.Services;
using FoodLibrary.ViewModels;
using GalaSoft.MvvmLight.Ioc;

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace ChooseFood
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            SimpleIoc.Default.GetInstance<INavigationService>().SetFrame(ContentFrame);
        }

        private void NavigationView_OnItemInvoked(NavigationView sender, 
            NavigationViewItemInvokedEventArgs args)
        {
            ((MainPageViewModel)DataContext).NavigationCommand.Execute((string)args.InvokedItem);
        }

        private void MainPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            ((MainPageViewModel) DataContext).RefreshCommand.Execute(null);
        }
    }
}
