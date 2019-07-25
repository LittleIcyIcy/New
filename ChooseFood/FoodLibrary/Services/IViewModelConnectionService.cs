using System;
using System.Collections.Generic;
using System.Text;

namespace FoodLibrary.Services
{
    public interface IViewModelConnectionService {
        INavigationService GetFrame(INavigationService navigationService);
    }
}
