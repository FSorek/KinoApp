using Kina.Mobile.Core.Model;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Kina.Mobile.Core.ViewModels
{
    public class NavigationViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;

        private MvxAsyncCommand _openDetailCommandCommand;
        private List<NavigationItem> navigationItems;

        public IMvxAsyncCommand OpenDetailCommand => _openDetailCommandCommand;
        public List<NavigationItem> NavigationItems => navigationItems;

        public NavigationViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
            navigationItems = new List<NavigationItem>
            {
                new NavigationItem("Filter Settings", GetImage("Funnel.png"), new MvxAsyncCommand(GoToFilterSettings)),
                new NavigationItem("Location Settings", GetImage("Pin.png"), new MvxAsyncCommand(GoToLocationSettings))
            };
        }

        private ImageSource GetImage(string source)
        {
            return ImageSource.FromResource("Kina.Mobile.Core.Resources.Images." + source);
        }

        public void InitializeCommands()
        {
            _openDetailCommandCommand = new MvxAsyncCommand(OpenDetailViewModel);
        }

        private async Task OpenDetailViewModel()
        {
            await _navigationService.Navigate<ShowsViewModel>();
        }

        private async Task GoToFilterSettings()
        {
            await _navigationService.Navigate<FilterViewModel>();
        }

        private async Task GoToLocationSettings()
        {
            await _navigationService.Navigate<LocationViewModel>();
        }
    }
}
