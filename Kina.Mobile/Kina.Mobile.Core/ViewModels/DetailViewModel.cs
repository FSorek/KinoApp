using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kina.Mobile.Core.ViewModels
{
    public class DetailViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;

        public DetailViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }
    }
}
