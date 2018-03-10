using Kina.Mobile.Core.Model;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kina.Mobile.Core.ViewModels
{
    public class MasterViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;

        private MvxAsyncCommand _openDetailCommandCommand;
        private List<NavigationItem> navigationItems;

        public IMvxAsyncCommand OpenDetailCommand => _openDetailCommandCommand;
        public List<NavigationItem> NavigationItems => navigationItems;

        public MasterViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
            navigationItems = new List<NavigationItem>
            {
                new NavigationItem("Filter Settings", new MvxAsyncCommand(GoToFilterSettings)),
                new NavigationItem("Location Settings", new MvxAsyncCommand(GoToLocationSettings))
            };
        }

        public void InitializeCommands()
        {
            _openDetailCommandCommand = new MvxAsyncCommand(OpenDetailViewModel);
        }

        private async Task OpenDetailViewModel()
        {
            await _navigationService.Navigate<DetailViewModel>();
        }

        private async Task GoToFilterSettings()
        {
            await _navigationService.Navigate<FilterViewModel>();
        }

        private async Task GoToLocationSettings()
        {
            await _navigationService.Navigate<LocationInitViewModel>();
        }
    }
}
