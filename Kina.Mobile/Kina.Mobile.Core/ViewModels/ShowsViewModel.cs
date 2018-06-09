using Kina.Mobile.Core.Model;
using Kina.Mobile.Core.Services;
using Kina.Mobile.DataProvider.Models;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Kina.Mobile.Core.ViewModels
{
    public class ShowsViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly IAppSettings _settings;
        private readonly IFilterService _filterService;
        private readonly IDataService _dataService;

        private MvxAsyncCommand _goToFilterViewCommandCommand;
        private MvxAsyncCommand _goToLocationViewCommandCommand;

        public IMvxAsyncCommand GoToFilterViewCommand => _goToFilterViewCommandCommand;
        public IMvxAsyncCommand GoToLocationViewCommand => _goToLocationViewCommandCommand;

        public MvxObservableCollection<Group<MovieShows>> Repertoires { get; set; }

        public ShowsViewModel(IMvxNavigationService navigationService, IDataService dataService,
            IFilterService filterService, IAppSettings settings)
        {
            _navigationService = navigationService;
            _settings = settings;
            _filterService = filterService;
            _dataService = dataService;
            Repertoires = new MvxObservableCollection<Group<MovieShows>>();

            InitCommands();
        }

        public void FillWithData()
        {
            if (Repertoires.Count != 0)
            {
                Repertoires.Clear();
            }

            List<Cinema> cinemas = _filterService.Cinemas;
            if (_filterService.Cinemas == null)
            {
                cinemas = Task.Run(() => _dataService.GetCinemasInCity(_filterService.City)).Result;
            }

            foreach (Cinema cinema in cinemas)
            {
                string cinemaName = String.Format("{0} - {1}", cinema.Name, cinema.City);
                ProcessMovies(cinema, cinemaName, (CinemaType) cinema.CinemaType);
            }
        }

        private void ProcessMovies(Cinema cinema, string cinemaName, CinemaType cinemaType)
        {
            var textColor = CinemaColor(cinemaType);
            var group = new Group<MovieShows>(textColor, cinemaName, cinema.Name.Substring(0, 1) + cinema.City.Substring(0, 2));
            foreach (SimpleMovie movie in cinema.MoviesPlayed)
            {
                if (_filterService.IsActive)
                {
                    if (!_filterService.Check(movie))
                    {
                        continue;
                    }
                }

                string shows = "";
                foreach (var show in movie.Shows)
                {
                    if (show.ShowDate.Date.Equals(DateTime.Today))
                    {
                        shows = shows + show.Start + ", ";
                    }
                }

                string genre = null;
                if (movie.Genre != null)
                {
                    if (movie.Genre.Count > 0)
                    {
                        genre = movie.Genre[0];
                    }
                }

                var showData = new BasicShowData(cinema.IdCinema, movie.Id, cinemaName, movie.AverageRating);
                MvxAsyncCommand command = new MvxAsyncCommand(async () =>
                {
                    await _navigationService.Navigate<MovieViewModel, BasicShowData>(showData);
                });

                var movieShows = new MovieShows(movie.Name, genre, shows, movie.AverageRating, command);
                group.Add(movieShows);
            }

            if (group.Count != 0)
            {
                Repertoires.Add(group);
            }
        }

        private Color CinemaColor(CinemaType type)
        {
            Color cinemaColor = Color.Transparent;
            switch (type)
            {
                default: break;
                case CinemaType.cinemacity: cinemaColor = Color.OrangeRed; break;
                case CinemaType.multikino: cinemaColor =  Color.MediumVioletRed; break;
            }

            return cinemaColor;
        }

        public void InitCommands()
        {
            _goToFilterViewCommandCommand = new MvxAsyncCommand(GoToFilterViewAction);
            _goToLocationViewCommandCommand = new MvxAsyncCommand(GoToLocationViewAction);
        }

        private async Task GoToFilterViewAction()
        {
            await _navigationService.Navigate<FilterViewModel>();
        }

        private async Task GoToLocationViewAction()
        {
            await _navigationService.Navigate<LocationViewModel>();
        }
    }
}
