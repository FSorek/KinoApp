using DataModel;
using Kina.Mobile.Core.Model;
using Kina.Mobile.DataProvider.Providers;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Kina.Mobile.Core.ViewModels
{
    class ShowsViewModel : MvxViewModel<FilterSet>
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly Services.IAppSettings _settings;

        private FilterSet _parameter;

        private MvxAsyncCommand _goToFilterViewCommandCommand;
        private MvxAsyncCommand _goToLocationViewCommandCommand;

        public IMvxAsyncCommand GoToFilterViewCommand => _goToFilterViewCommandCommand;
        public IMvxAsyncCommand GoToLocationViewCommand => _goToLocationViewCommandCommand;

        private List<ShowsMovieModel> movies;
        private List<UserScore> userScore;

        public List<ShowsMovieModel> Movies
        {
            get { return movies; }
            set { SetProperty(ref movies, value); }
        }

        public List<Movie> AddMovies(DataRequestService dbReq, CinemaType cinemaType, int cinemaId)
        {
            var movieList = new List<Movie>();

            InitList(dbReq, CinemaType.multikino, cinemaId);
            movieList.AddRange(dbReq.MovieList);

            Cinema cinema = new Cinema();
            cinema.Id_Self = cinemaId;
            switch (cinemaId)
            {
                default: cinema.Latitude = cinema.Longtitude = 0; break;
                case 12:
                    cinema.Latitude = 54.44514;
                    cinema.Longtitude = 18.5654693;
                    cinema.City = "Sopot";
                    break;
                case 14:
                    cinema.Latitude = 52.3025245;
                    cinema.Longtitude = 21.0153022;
                    cinema.City = "Warszawa Targowek";
                    break;
                case 1073:
                    cinema.Latitude = 54.3533975;
                    cinema.Longtitude = 18.6439144;
                    cinema.City = "Gdansk";
                    break;
            }

            Task.Run(() => MvxApp.Database.SaveCinemaAsync(cinema)).Wait();

            return movieList;


        }
        public ShowsViewModel(IMvxNavigationService navigationService, Services.IAppSettings settings)
        {
            _navigationService = navigationService;
            _settings = settings;

            if (!MvxApp.UsingFilter)
            {
                FillWithData();
            }

            InitCommands();
        }

        public void FillWithData()
        {
            DataRequestService dataRequestService = new DataRequestService();

            List<Movie> movieList = new List<Movie>();
            
            movieList.AddRange(AddMovies(dataRequestService, CinemaType.multikino, 12));
            movieList.AddRange(AddMovies(dataRequestService, CinemaType.multikino, 14));
            movieList.AddRange(AddMovies(dataRequestService, CinemaType.cinemacity, 1073));
            
            var today = DateTime.Today;
            movies = new List<ShowsMovieModel>();

            foreach (Movie m in movieList)
            {
                bool check = true;
                bool content = m.Shows.Count != 0;
                int showAfterFiltering = 0;
                if (_parameter != null)
                {
                    if(_parameter.Title != null)
                    {
                        check = m.Name.ToLower().Contains(_parameter.Title.ToLower());
                    }
                    if(_parameter.Genre != null)
                    {
                        check = check && (m.Genre.Contains(_parameter.Genre.Name) || m.Genre.Contains(_parameter.Genre.EngName));
                    }
                    if (content)
                    {
                        foreach (var s in m.Shows)
                        {
                            
                            int showHour = int.Parse(s.Start.Split(':')[0]);
                            int parameterHour = int.Parse(_parameter.Start.Split(':')[0]);
                            if(((showHour > (parameterHour - 1)) && (showHour < (parameterHour + 1))) || (parameterHour == 0))
                            {
                                showAfterFiltering++;
                            }
                        }
                    }
                }
                if (content && check)
                {
                    if (_parameter != null && showAfterFiltering == 0)
                        continue;
                    double score = 0.0;
                    GetScore(m.Id_Movie, m.Shows[0].Id_Cinema);
                    if (userScore.Count != 0)
                    {
                        int i = 0;
                        if (userScore != null)
                        {
                            foreach (UserScore s in userScore)
                            {
                                if (s.Id_Movie.Equals(m.Id_Movie) && s.Id_Cinema == m.Shows[0].Id_Cinema)
                                {
                                    score += (s.Screen + s.Seat + s.Sound + s.Popcorn) / 4.0;
                                    i++;
                                }
                            }
                            score /= i;
                        }
                    }
                    movies.Add(new ShowsMovieModel(m, score, _navigationService, _parameter, _settings));
                }
            }
        }

        private void InitList(DataRequestService dataRequestService, CinemaType cinema, int cinema_id)
        {
            Task.Run(() => dataRequestService.ProvideData(cinema, cinema_id)).Wait();
            Debug.WriteLine("I'm here");
        }

        public void InitCommands()
        {
            _goToFilterViewCommandCommand = new MvxAsyncCommand(GoToFilterViewAction);
            _goToLocationViewCommandCommand = new MvxAsyncCommand(GoToLocationViewAction);
        }

        private void GetScore(string movieId, int cinemaId)
        {
            Task.Run(() => GetScoreAsync(movieId, cinemaId)).Wait();
        }

        private async Task GoToFilterViewAction()
        {
            await _navigationService.Navigate<FilterViewModel>();
        }

        private async Task GoToLocationViewAction()
        {
            await _navigationService.Navigate<LocationViewModel>();
        }

        private async Task GetScoreAsync(string movieId, int cinemaId)
        {
            userScore = await MvxApp.Database.GetUserScoreAsync(cinemaId, movieId);
        }

        public override Task Initialize(FilterSet parameter)
        {
            _parameter = parameter;
            if (MvxApp.UsingFilter)
            {
                FillWithData();
            }
            return Task.FromResult(true);
        }
    }
}
