using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kina.Mobile.Core.ViewModels
{
    public class MasterDetailaViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;

        private MvxAsyncCommand _showInitialMenuCommandCommand;
        private MvxAsyncCommand _showDetailCommandCommand;

        public IMvxAsyncCommand ShowDetailCommand => _showDetailCommandCommand;
        public IMvxAsyncCommand ShowInitialMenuCommand => _showInitialMenuCommandCommand;

        public MasterDetailaViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            _showInitialMenuCommandCommand = new MvxAsyncCommand(ShowInitialViewModel);
            _showDetailCommandCommand = new MvxAsyncCommand(ShowDetailViewModel);
        }

        public async Task ShowInitialViewModel()
        {
            await _navigationService.Navigate<MasterViewModel>();
        }

        private async Task ShowDetailViewModel()
        {
            await _navigationService.Navigate<DetailViewModel>();
        }

        public override void ViewAppeared()
        {
            MvxNotifyTask.Create(async () =>
            {
                await ShowInitialViewModel();
                await ShowDetailViewModel();
            });
        }
    }
}
