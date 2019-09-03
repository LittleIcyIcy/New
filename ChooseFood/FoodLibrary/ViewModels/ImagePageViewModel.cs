using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodLibrary.ViewModels
{
    public class ImagePageViewModel:ViewModelBase
    {
        public string Image
        {
            get => _image;
            set => Set(nameof(Image), ref _image, value);
        }
        private string _image;

        private RelayCommand _coverCommand;

        public RelayCommand CoverCommand => _coverCommand ??
            (_coverCommand = new RelayCommand(() => {
                Random run = new Random();
                int RandomKey = run.Next(1, 22);
                Image = "cover/timg (" + Convert.ToString(RandomKey) + ").jpg";
            }));
    }
}
