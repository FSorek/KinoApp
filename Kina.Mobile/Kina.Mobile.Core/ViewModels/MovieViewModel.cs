// ---------------------------------------------------------------
// <author>Paul Datsyuk</author>
// <url>https://www.linkedin.com/in/pauldatsyuk/</url>
// ---------------------------------------------------------------

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

        public double AverageRating { get; set; }
        public string Title { get; set; }
        public string URLText { get; set; }
        public string Description { get; set; }
        public string Year { get; set; }
        public string Cast { get; set; }
        public string Director { get; set; }
        public string Genre { get; set; }

        public double CleanlinessRating { get; set; }
        public double ScreenRating { get; set; }
        public double SeatsRating { get; set; }
        public double SoundRating { get; set; }
        public double SnacksRating { get; set; }

        public MovieViewModel(IMvxNavigationService navigationService, IDataService dataService)
        {
            _navigationService = navigationService;
            _dataService = dataService;

            InitCommands();
        }

        public IMvxCommand OpenYoutubeUrlCommand =>
            new MvxCommand(() =>
            {
                Device.OpenUri(new Uri("https://www.youtube.com/"));
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

        public override void Prepare(BasicShowData parameter)
        {
            _parameter = parameter;
            Movie requested = Task.Run(() => _dataService.GetMovie(parameter.IdMovie)).Result;
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
            AverageRating = parameter.AverageRating;

            List<UserScore> ratings = Task.Run(() => _dataService.GetRating(parameter.IdMovie, parameter.IdCinema)).Result;
            foreach(var rating in ratings)
            {
                CleanlinessRating += rating.Cleanliness;
                ScreenRating += rating.Screen;
                SeatsRating += rating.Seat;
                SnacksRating += rating.Popcorn;
                SoundRating += rating.Sound;
            }

            CleanlinessRating /= ratings.Count;
            ScreenRating /= ratings.Count;
            SeatsRating /= ratings.Count;
            SnacksRating /= ratings.Count;
            SoundRating /= ratings.Count;
        }
    }
}
