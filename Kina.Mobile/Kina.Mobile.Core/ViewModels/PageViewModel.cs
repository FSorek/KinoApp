using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kina.Mobile.Core.ViewModels
{
    class PageViewModel : MvxViewModel
    {
        IMvxNavigationService _navigationService;
        public PageViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }
    }
}
