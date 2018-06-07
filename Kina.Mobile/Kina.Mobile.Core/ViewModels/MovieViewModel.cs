using Kina.Mobile.Core.Model;
using Kina.Mobile.Core.Services;
using Kina.Mobile.DataProvider.Models;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Kina.Mobile.Core.ViewModels
{
    class MovieViewModel : MvxViewModel<BasicShowData>
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly IDataService _dataService;
        //private readonly Services.IAppSettings _settings;

        private MvxAsyncCommand _goToLocationViewCommandCommand;
        private MvxAsyncCommand _goToRateViewCommandCommand;

        public IMvxAsyncCommand GoToLocationViewCommand => _goToLocationViewCommandCommand;
        public IMvxAsyncCommand GoToRateViewCommand => _goToRateViewCommandCommand;

        private BasicShowData _parameter;
        private double _averageRating;
        private string _title;
        private string _urlText;
        private string _description;
        private string _year;
        private string _cast;
        private string _director;
        private string _genre;
        private double _snacksRating;
        private double _soundRating;
        private double _seatsRating;
        private double _screenRating;
        private double _cleanlinessRating;
        private bool _isYoutubeLink;

        public double AverageRating
        {
            get { return _averageRating; }
            set { SetProperty(ref _averageRating, value); }
        }

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public string URLText
        {
            get { return _urlText; }
            set { SetProperty(ref _urlText, value); }
        }

        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }

        public string Year
        {
            get { return _year; }
            set { SetProperty(ref _year, value); }
        }

        public string Cast
        {
            get { return _cast; }
            set { SetProperty(ref _cast, value); }
        }

        public string Director
        {
            get { return _director; }
            set { SetProperty(ref _director, value); }
        }

        public string Genre
        {
            get { return _genre; }
            set { SetProperty(ref _genre, value); }
        }

        public bool IsYouTubeLink
        {
            get { return _isYoutubeLink; }
            set { SetProperty(ref _isYoutubeLink, value); }
        }

        public double CleanlinessRating
        {
            get { return _cleanlinessRating; }
            set { SetProperty(ref _cleanlinessRating, value); }
        }

        public double ScreenRating
        {
            get { return _screenRating; }
            set { SetProperty(ref _screenRating, value); }
        }

        public double SeatsRating
        {
            get { return _seatsRating; }
            set { SetProperty(ref _seatsRating, value); }
        }

        public double SoundRating
        {
            get { return _soundRating; }
            set { SetProperty(ref _soundRating, value); }
        }

        public double SnacksRating
        {
            get { return _snacksRating; }
            set { SetProperty(ref _snacksRating, value); }
        }

        public MovieViewModel(IMvxNavigationService navigationService, IDataService dataService)
        {
            _navigationService = navigationService;
            _dataService = dataService;

            InitCommands();
        }

        public override void Prepare(BasicShowData parameter)
        {
            _parameter = parameter;
        }

        public IMvxCommand OpenYoutubeUrlCommand =>
            new MvxCommand(() =>
            {
                Device.OpenUri(new Uri(URLText));
            });

        private async Task GoToRateViewAction()
        {
            await _navigationService.Navigate<RateViewModel, BasicShowData>(_parameter);
        }

        private async Task GoToLocationViewAction()
        {
            await _navigationService.Navigate<LocationViewModel>();
        }

        private void InitCommands()
        {
            _goToLocationViewCommandCommand = new MvxAsyncCommand(GoToLocationViewAction);
            _goToRateViewCommandCommand = new MvxAsyncCommand(GoToRateViewAction);
        }

        public void FillWithData()
        {
            Movie requested = Task.Run(() => _dataService.GetMovie(_parameter.IdMovie)).Result;
            Title = requested.OriginalName;
            if (requested.Genre != null)
            {
                if (requested.Genre.Count > 0)
                {
                    Genre = requested.Genre[0];
                }
            }

            Description = requested.Storyline;
            Director = requested.Director;
            URLText = requested.Trailer;
            Cast = requested.Stars;
            Year = null;
            CleanlinessRating = 0;
            ScreenRating = 0;
            SeatsRating = 0;
            SnacksRating = 0;
            SoundRating = 0;

            if (URLText.Contains("youtube.com/"))
            {
                IsYouTubeLink = true;
            }

            List<UserScore> ratings = Task.Run(() => _dataService.GetRating(_parameter.IdMovie, _parameter.IdCinema)).Result;
            foreach(var rating in ratings)
            {
                CleanlinessRating = CleanlinessRating + rating.Cleanliness;
                ScreenRating += rating.Screen;
                SeatsRating += rating.Seat;
                SnacksRating += rating.Popcorn;
                SoundRating += rating.Sound;
            }

            if (ratings.Count != 0)
            {
                CleanlinessRating = CleanlinessRating / ratings.Count;
                ScreenRating /= ratings.Count;
                SeatsRating /= ratings.Count;
                SnacksRating /= ratings.Count;
                SoundRating /= ratings.Count;
            }

            AverageRating = (CleanlinessRating + ScreenRating + SeatsRating + SnacksRating + SoundRating) / 5;
        }
    }
}
