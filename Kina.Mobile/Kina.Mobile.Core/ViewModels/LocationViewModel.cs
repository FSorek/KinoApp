using Kina.Mobile.Core.Services;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using DataModel;
using Kina.Mobile.Core.Model;
using MvvmCross.Plugins.Messenger;

namespace Kina.Mobile.Core.ViewModels
{
    class LocationViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly MvxSubscriptionToken _token;

        private MvxAsyncCommand _confirmLocationCommandCommand;
        private MvxAsyncCommand _autoLocateCommandCommand;

        private List<string> location;

        public string DistanceLabel { get { return String.Format("Using the device's location, find cinemas at a distance of about {0} km.", distance); } }

        private int distance;

        public ICommand ConfirmLocationCommand => _confirmLocationCommandCommand;
        public ICommand AutoLocateCommand => _autoLocateCommandCommand;

        private double _lng; //Longtitude of device
        public double Lng
        {
            get { return _lng; }
            set { _lng = value; RaisePropertyChanged(() => Lng); }
        }

        private double _lat; //Latitude of device
        public double Lat
        {
            get { return _lat; }
            set { _lat = value; RaisePropertyChanged(() => Lat); }
        }

        public List<string> Location
        {
            get { return location; }
            set { SetProperty(ref location, value); }
        }

        public int Distance
        {
            get { return distance; }
            set { SetProperty(ref distance, value); }
        }



        public LocationViewModel(IMvxNavigationService navigationService, ILocationService service, IMvxMessenger messenger)
        {
            _navigationService = navigationService;
            _token = messenger.SubscribeOnMainThread<LocationMessage>(OnLocationMessage); //Live Update of device coords

            #region Temporary hardcoded content
            location = new List<string>();
            location.Add("Gdańsk");
            #endregion

            InitCommands();
        }

        private void OnLocationMessage(LocationMessage locationMessage)
        {
            Lat = locationMessage.Lat;
            Lng = locationMessage.Lng;
        }

        private async Task ConfirmLocationAction()
        {
            Debug.WriteLine(Lat);
            Debug.WriteLine(Lng);
            if (Lat == 0 || Lng == 0)
            {
                
            }
            MvxApp.FilterSettings.Cinemas = GetCinemasInRange(distance);
            await _navigationService.Navigate<ShowsViewModel, FilterSet>(MvxApp.FilterSettings);
        }

        private async Task AutoLocateAction()
        {
            MvxApp.FilterSettings.Cinemas = null;
            await _navigationService.Navigate<ShowsViewModel, FilterSet>(MvxApp.FilterSettings);
        }

        private void InitCommands()
        {
            _autoLocateCommandCommand = new MvxAsyncCommand(AutoLocateAction);
            _confirmLocationCommandCommand = new MvxAsyncCommand(ConfirmLocationAction);
        }

        public List<Cinema> GetCinemasInRange(int kilimeters)
        {
            var cinemas = new List<Cinema>();
            var cinemasInRange = new List<Cinema>();
            cinemas = Task.Run(() => MvxApp.Database.GetAllCinemaAsync()).Result;
            foreach (var cinema in cinemas)
            {
                if (CalculateDistance(Lat, Lng, cinema.Latitude, cinema.Longtitude) <= kilimeters)
                {
                    cinemasInRange.Add(cinema);
                }
            }

            return cinemasInRange;
        }

        public double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
        {
            double rlat1 = Math.PI * lat1 / 180;
            double rlat2 = Math.PI * lat2 / 180;
            double theta = lon1 - lon2;
            double rtheta = Math.PI * theta / 180;
            double dist =
                Math.Sin(rlat1) * Math.Sin(rlat2) + Math.Cos(rlat1) *
                Math.Cos(rlat2) * Math.Cos(rtheta);
            dist = Math.Acos(dist);
            dist = dist * 180 / Math.PI;
            dist = dist * 60 * 1.1515;

            return dist * 1.609344;
        }
    }
}
