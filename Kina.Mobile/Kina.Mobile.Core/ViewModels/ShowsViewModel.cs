using DataModel;
using Kina.Mobile.Core.Model;
using Kina.Mobile.DataProvider.Providers;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

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

        public List<Model.MovieList> ShowsList { get; set; }

        public List<Movie> AddMovies(DataRequestService dbReq, String cinemaType, int cinemaId)
        {
            var movieList = new List<Movie>();

            if(cinemaType == "Multikino")
                InitList(dbReq, CinemaType.multikino, cinemaId);
            else if(cinemaType == "CinemaCity")
                InitList(dbReq, CinemaType.cinemacity, cinemaId);
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
                    cinema.CinemaType = "Multikino";
                    break;
                case 14:
                    cinema.Latitude = 52.3025245;
                    cinema.Longtitude = 21.0153022;
                    cinema.City = "Warszawa Targowek";
                    cinema.CinemaType = "Multikino";
                    break;
                case 1073:
                    cinema.Latitude = 54.3533975;
                    cinema.Longtitude = 18.6439144;
                    cinema.City = "Gdansk";
                    cinema.CinemaType = "CinemaCity";
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
            ShowsList = new List<Model.MovieList>();

            List<Movie> movieList;

            if (MvxApp.FilterSettings.Cinemas != null)
            {
                foreach (var cinema in MvxApp.FilterSettings.Cinemas)
                {
                    movieList = new List<Movie>();
                    movieList.AddRange(AddMovies(dataRequestService, cinema.CinemaType, cinema.Id_Self));
                    CinemaType type = CinemaType.cinemacity;
                    switch (cinema.CinemaType)
                    {
                        case "Multikino": type = CinemaType.multikino; break;
                        case "CinemaCity": type = CinemaType.cinemacity; break;
                    }
                    ProcessMovies(movieList, String.Format("{0} - {1}", cinema.CinemaType, cinema.City), type);
                }
            }
            else
            {
                movieList = new List<Movie>();
                movieList.AddRange(AddMovies(dataRequestService, "Multikino", 12));
                ProcessMovies(movieList, "Multikino - Sopot", CinemaType.multikino);
                movieList = new List<Movie>();
                movieList.AddRange(AddMovies(dataRequestService, "Multikino", 14));
                ProcessMovies(movieList, "Multikino - Warszawa", CinemaType.multikino);
                movieList = new List<Movie>();
                movieList.AddRange(AddMovies(dataRequestService, "CinemaCity", 1073));
                ProcessMovies(movieList, "Krewetka", CinemaType.cinemacity);
            }
        }

        private void ProcessMovies(List<Movie> movieList, string cinemaName, CinemaType cinemaType)
        {
            var today = DateTime.Today;
            movies = new List<ShowsMovieModel>();

            foreach (Movie m in movieList)
            {
                bool check = true;
                bool content = m.Shows.Count != 0;
                int showAfterFiltering = 0;
                if (MvxApp.UsingFilter)
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
                            int parameterHourStart = int.Parse(_parameter.Start.Split(':')[0]);
                            int parameterHourEnd = int.Parse(_parameter.End.Split(':')[0]);
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
                    movies.Add(new ShowsMovieModel(m, score, _navigationService, _parameter, _settings, cinemaName));
                }
            }

            ShowsList.Add(new Model.MovieList(cinemaName, movies, CinemaColor(cinemaType)));
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
