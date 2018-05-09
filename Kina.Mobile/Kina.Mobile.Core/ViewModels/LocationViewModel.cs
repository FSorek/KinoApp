using Acr.UserDialogs;
using Kina.Mobile.Core.Services;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Plugins.Messenger;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kina.Mobile.Core.ViewModels
{
    public class LocationViewModel : MvxViewModel<List<string>>
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly IDataService _dataService;
        private readonly IFilterService _filterService;

        private readonly MvxSubscriptionToken _token;

        private MvxAsyncCommand _confirmLocationCommandCommand;
        private MvxAsyncCommand _autoLocateCommandCommand;

        private double _longtitude;
        private double _latitude;
        private int _selectedLocationIndex;
        private int _distance;
        private string _rangeText;
        private List<string> _locations;

        public IMvxAsyncCommand ConfirmLocationCommand => _confirmLocationCommandCommand;
        public IMvxAsyncCommand AutoLocateCommand => _autoLocateCommandCommand;

        public int SelectedLocationIndex
        {
            get { return _selectedLocationIndex; }
            set { SetProperty(ref _selectedLocationIndex, value); }
        }

        public string RangeText
        {
            get { return String.Format("Using the device's location, find cinemas at a distance of about {0} km.", _distance); }
            set { SetProperty(ref _rangeText, String.Format("Using the device's location, find cinemas at a distance of about {0} km.", value)); }
        }

        public int Distance
        {
            get { return _distance; }
            set
            {
                RangeText = value.ToString();
                SetProperty(ref _distance, value);
            }
        }

        public List<string> Locations
        {
            get { return _locations; }
            set { SetProperty(ref _locations, value); }
        }

        public LocationViewModel(IMvxNavigationService navigationService, IDataService dataService,
            ILocationService locationService, IFilterService filterService, IMvxMessenger messenger)
        {
            _navigationService = navigationService;
            _dataService = dataService;
            _filterService = filterService;
            _token = messenger.SubscribeOnMainThread<LocationMessage>(OnLocationMessage);

            Locations = Task.Run(() => _dataService.GetCities()).Result;

            InitCommands();
        }

        private void OnLocationMessage(LocationMessage locationMessage)
        {
            _latitude = locationMessage.Lat;
            _longtitude = locationMessage.Lng;
        }

        private async Task ConfirmSelectedLocation()
        {
            if (_selectedLocationIndex == -1)
            {
                Mvx.Resolve<IUserDialogs>().Alert("Location was not idicated. Please provide your location for this session before proceeding.");
            }

            _filterService.City = _locations[_selectedLocationIndex];
            _filterService.Cinemas = null;
            await ShowMasterDetailView();
        }

        private async Task AutoDetectLocation()
        {
            if (_latitude == 0 || _longtitude == 0)
            {
                Mvx.Resolve<IUserDialogs>().Alert("Your device was not able to detect proper location. Please provide your location for this session manually.");
                return;
            }

            _filterService.Cinemas = await _dataService.GetCinemasInRange(_latitude, _longtitude, _distance);
            _filterService.City = null;
            await ShowMasterDetailView();
        }

        private async Task ShowMasterDetailView()
        {
            await _navigationService.Navigate<MasterDetailViewModel>();
        }

        private void InitCommands()
        {
            _autoLocateCommandCommand = new MvxAsyncCommand(AutoDetectLocation);
            _confirmLocationCommandCommand = new MvxAsyncCommand(ConfirmSelectedLocation);
        }

        public override void Prepare(List<string> parameter)
        {
            Locations = parameter;
        }
    }
}
