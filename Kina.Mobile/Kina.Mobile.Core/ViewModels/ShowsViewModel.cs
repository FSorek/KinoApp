using Kina.Mobile.Core.Model;
using Kina.Mobile.DataProvider.Models;
using Kina.Mobile.DataProvider.Providers;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Kina.Mobile.Core.ViewModels
{
    class ShowsViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly Services.IAppSettings _settings;

        private MvxAsyncCommand _goToFilterViewCommandCommand;
        private MvxAsyncCommand _goToLocationViewCommandCommand;

        public IMvxAsyncCommand GoToFilterViewCommand => _goToFilterViewCommandCommand;
        public IMvxAsyncCommand GoToLocationViewCommand => _goToLocationViewCommandCommand;

        private List<MovieListItem> movies;
        private List<UserScore> userScore;

        public List<MovieListItem> Movies
        {
            get { return movies; }
            set { SetProperty(ref movies, value); }
        }

        public List<MovieList> ShowsList { get; set; }

        public ShowsViewModel(IMvxNavigationService navigationService, Services.IAppSettings settings)
        {
            _navigationService = navigationService;
            _settings = settings;

            FillWithData();

            InitCommands();
        }

        public void FillWithData()
        {
            DataRequest dataRequest = new DataRequest();

            ShowsList = new List<MovieList>();
            List<Cinema> cinemaList = MvxApp.FilterSettings.Cinemas;

            // If Cinemas is null, then City is definied
            if (MvxApp.FilterSettings.Cinemas == null)
            {
                GetData(dataRequest, MvxApp.FilterSettings.City);
                cinemaList = dataRequest.CinemaList;
            }

            foreach (Cinema cinema in cinemaList)
            {
                string cinemaName = String.Format("{0} - {1}", cinema.Name, cinema.City);
                ProcessMovies(cinema, cinemaName, (CinemaType) cinema.CinemaType);
            }
        }

        private void ProcessMovies(Cinema cinema, string cinemaName, CinemaType cinemaType)
        {
            var today = DateTime.Today;
            movies = new List<MovieListItem>();

            foreach (SimpleMovie movie in cinema.MoviesPlayed)
            {
                bool check = true;
                bool content = movie.Shows.Count != 0;
                int showAfterFiltering = 0;
                if (MvxApp.UsingFilter)
                {
                    if(MvxApp.FilterSettings.Title != null)
                    {
                        check = movie.Name.ToLower().Contains(MvxApp.FilterSettings.Title.ToLower());
                    }
                    if(MvxApp.FilterSettings.Category != null)
                    {
                        check = check && (movie.Genre.Contains(MvxApp.FilterSettings.Category) || movie.Genre.Contains(MvxApp.FilterSettings.Category));
                    }
                    if (content)
                    {
                        foreach (var s in movie.Shows)
                        {
                            
                            int showHour = int.Parse(s.Start.Split(':')[0]);
                            int parameterHourStart = int.Parse(MvxApp.FilterSettings.Start.Split(':')[0]);
                            int parameterHourEnd = int.Parse(MvxApp.FilterSettings.End.Split(':')[0]);
                            if (((showHour > (parameterHourStart)) && (showHour < (parameterHourEnd))) || (parameterHourStart == 0 && parameterHourEnd == 0))
                            {
                                showAfterFiltering++;
                            }
                        }
                        if (showAfterFiltering == 0)
                            continue;
                    }
                }
                if (content && check)
                {
                    DataRequest dataRequest = new DataRequest();
                    double score = 0.0;
                    GetScore(movie.Id, cinema.IdCinema, dataRequest);
                    userScore = dataRequest.ShowScore;
                    if (userScore.Count != 0)
                    {
                        int i = 0;
                        if (userScore != null)
                        {
                            foreach (UserScore s in userScore)
                            {
                                if (s.IdMovie.Equals(movie.Id) && s.IdCinema == cinema.IdCinema)
                                {
                                    score += (s.Screen + s.Seat + s.Sound + s.Popcorn) / 4.0;
                                    i++;
                                }
                            }
                            score /= i;
                        }
                    }

                    BasicShowData basicShowData = new BasicShowData(cinema.IdCinema, movie.Id, cinemaName);
                    movies.Add(new MovieListItem(basicShowData ,movie, score, _navigationService));
                }
            }

            ShowsList.Add(new MovieList(cinemaName, movies, CinemaColor(cinemaType)));
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

        private void GetData(DataRequest dataRequest, string city)
        {
            Task.Run(() => dataRequest.ProvideShowsFromCity(city)).Wait();
        }

        public void InitCommands()
        {
            _goToFilterViewCommandCommand = new MvxAsyncCommand(GoToFilterViewAction);
            _goToLocationViewCommandCommand = new MvxAsyncCommand(GoToLocationViewAction);
        }

        private void GetScore(long movieId, long cinemaId, DataRequest dataRequest)
        {
            Task.Run(() => dataRequest.ProvideScoreData(movieId, cinemaId)).Wait();
        }

        private async Task GoToFilterViewAction()
        {
            await _navigationService.Navigate<FilterViewModel>();
        }

        private async Task GoToLocationViewAction()
        {
            await _navigationService.Navigate<LocationInitViewModel>();
        }
    }
}
