using Kina.Mobile.Core.Pages;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kina.Mobile.Core.ViewModels
{
    public class MasterViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;

        private MvxAsyncCommand _openDetailCommandCommand;

        public IMvxAsyncCommand OpenDetailCommand => _openDetailCommandCommand;

        public MasterViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
            InitializeCommands();
        }

        public void InitializeCommands()
        {
            _openDetailCommandCommand = new MvxAsyncCommand(OpenDetailViewModel);
        }

        private async Task OpenDetailViewModel()
        {
            await _navigationService.Navigate<DetailViewModel>();
        }
    }
}
