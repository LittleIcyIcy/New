using System;
using System.Collections.Generic;
using System.Text;

namespace FoodLibrary.Services.Impl
{
    class ViewModelConnectionService:IViewModelConnectionService
    {
        public INavigationService GetFrame(INavigationService navigationService)
        {
            return navigationService;
        }
    }
}
