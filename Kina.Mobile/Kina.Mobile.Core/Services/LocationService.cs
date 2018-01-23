using System;
using MvvmCross.Plugins.Location;
using MvvmCross.Plugins.Messenger;

namespace Kina.Mobile.Core.Services
{
    public class LocationMessage : MvxMessage
    {
        public LocationMessage(object sender, double lat, double lng) 
            : base(sender)
        {
            Lng = lng;
            Lat = lat;
        }

        public double Lat { get; private set;}
        public double Lng { get; private set; }
    }
    public class LocationService : ILocationService
    {
        private readonly IMvxLocationWatcher _watcher;
        private readonly IMvxMessenger _messenger;

        //private readonly MvxMessenger _messenger;

        public LocationService(IMvxLocationWatcher watcher, IMvxMessenger messenger)
        {
            _watcher = watcher;
            _messenger = messenger;
            _watcher.Start(new MvxLocationOptions(), OnLocation, OnError);
        }

        private void OnLocation(MvxGeoLocation location)
        {
            var message = new LocationMessage(this, location.Coordinates.Latitude, location.Coordinates.Latitude);
            _messenger.Publish(message);
        }                                           

        private void OnError(MvxLocationError error)
        {

        }
    }
}