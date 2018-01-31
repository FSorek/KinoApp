using Acr.UserDialogs;
using DataModel;
using Kina.Mobile.Core.Model;
using Kina.Mobile.Core.Services;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Plugins.Messenger;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Kina.Mobile.Core.ViewModels
{
    class LocationInitViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;

        private readonly MvxSubscriptionToken _token;

        private MvxAsyncCommand _confirmLocationCommandCommand;
        private MvxAsyncCommand _autoLocateCommandCommand;

        private double _longtitude;
        private double _latitude;
        private string selectedLocation;
        private int distance;
        private Dictionary<string, Cinema> location;

        public ICommand ConfirmLocationCommand => _confirmLocationCommandCommand;
        public ICommand AutoLocateCommand => _autoLocateCommandCommand;

        public string SelectedLocation
        {
            get { return selectedLocation; }
            set { SetProperty(ref selectedLocation, value); }
        }

        public string RangeText
        {
            get { return String.Format("Using the device's location, find cinemas at a distance of about {0} km.", distance); }
        }

        public int Distance
        {
            get { return distance; }
            set { SetProperty(ref distance, value); }
        }

        public List<string> Location
        {
            get { return location.Keys.ToList(); }
        }

        public LocationInitViewModel(IMvxNavigationService navigationService, ILocationService locationService, IMvxMessenger messenger)
        {
            _navigationService = navigationService;
            _token = messenger.SubscribeOnMainThread<LocationMessage>(OnLocationMessage);

            location = new Dictionary<string, Cinema>();
            #region Temporary hardcoded content
            location.Add("Rumia", new Cinema());
            #endregion

            InitCommands();
        }

        private void OnLocationMessage(LocationMessage locationMessage)
        {
            _latitude = locationMessage.Lat;
            _longtitude = locationMessage.Lng;
        }

        private async Task ConfirmLocationAction()
        {
            await _navigationService.Navigate<ShowsViewModel, FilterSet>(MvxApp.FilterSettings);
        }

        private async Task AutoLocateAction()
        {
            MvxApp.FilterSettings.Cinemas = null;
            Debug.WriteLine(_latitude);
            Debug.WriteLine(_longtitude);
            if (_latitude == 0 || _longtitude == 0)
            {
                Mvx.Resolve<IUserDialogs>().Alert("Your device was not able to detect proper location. Please provide your location for this session manually.");
                return;
            }
            await _navigationService.Navigate<ShowsViewModel>();
        }

        private void InitCommands()
        {
            _autoLocateCommandCommand = new MvxAsyncCommand(AutoLocateAction);
            _confirmLocationCommandCommand = new MvxAsyncCommand(ConfirmLocationAction);
        }
    }
}
