using Acr.UserDialogs;
using Kina.Mobile.Core.Services;
using Kina.Mobile.DataProvider.Providers;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Plugins.Messenger;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kina.Mobile.Core.ViewModels
{
    public class LocationViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;

        private readonly MvxSubscriptionToken _token;

        private MvxAsyncCommand _confirmLocationCommandCommand;
        private MvxAsyncCommand _autoLocateCommandCommand;

        private double _longtitude;
        private double _latitude;
        private string selectedLocation;
        private int distance;
        private List<string> location;

        public IMvxAsyncCommand ConfirmLocationCommand => _confirmLocationCommandCommand;
        public IMvxAsyncCommand AutoLocateCommand => _autoLocateCommandCommand;

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
            get { return location; }
        }

        public LocationViewModel(IMvxNavigationService navigationService, ILocationService locationService, IMvxMessenger messenger)
        {
            _navigationService = navigationService;
            _token = messenger.SubscribeOnMainThread<LocationMessage>(OnLocationMessage);

            DataRequest dataRequest = new DataRequest();
            location = dataRequest.CityList;
            GetLocations(dataRequest);
            location = dataRequest.CityList;

            InitCommands();
        }

        private void OnLocationMessage(LocationMessage locationMessage)
        {
            _latitude = locationMessage.Lat;
            _longtitude = locationMessage.Lng;
        }

        private async Task ConfirmLocation()
        {
            if (selectedLocation == null)
            {
                Mvx.Resolve<IUserDialogs>().Alert("Location was not idicated. Please provide your location for this session before proceeding.");
                //return;
            }
            MvxApp.FilterSettings.City = selectedLocation;
            MvxApp.FilterSettings.Cinemas = null;
            await ShowMasterDetailView();
        }

        private async Task AutoDetectLocation()
        {
            if (_latitude == 0 || _longtitude == 0)
            {
                Mvx.Resolve<IUserDialogs>().Alert("Your device was not able to detect proper location. Please provide your location for this session manually.");
                return;
            }

            DataRequest dataRequest = new DataRequest();
            GetCinemasInRange(dataRequest);
            MvxApp.FilterSettings.Cinemas = dataRequest.CinemaList;
            MvxApp.FilterSettings.City = null;
            await ShowMasterDetailView();
        }

        private async Task ShowMasterDetailView()
        {
            await _navigationService.Navigate<MasterDetailViewModel>();
        }

        private void GetLocations(DataRequest dataRequest)
        {
            Task.Run(() => dataRequest.ProvideCities()).Wait();
        }

        private void GetCinemasInRange(DataRequest dataRequest)
        {
            Task.Run(() => dataRequest.ProvideCinemasInRange(_latitude, _longtitude, distance)).Wait();
        }

        private void InitCommands()
        {
            _autoLocateCommandCommand = new MvxAsyncCommand(AutoDetectLocation);
            _confirmLocationCommandCommand = new MvxAsyncCommand(ConfirmLocation);
        }
    }
}
