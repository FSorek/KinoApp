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

        public List<MovieList> ShowsList { get; set; }

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
            DataRequest dataRequest = new DataRequest();

            ShowsList = new List<MovieList>();
            List<Cinema> cinemaList;
            FilterSet filterSet = MvxApp.FilterSettings;

            // If Cinemas is null, then City is definied
            if (MvxApp.FilterSettings.Cinemas != null)
            {
                //foreach (var cinema in MvxApp.FilterSettings.Cinemas)
                //{
                //    movieList = new List<Movie>();
                //    movieList.AddRange(AddMovies(dataRequestService, cinema.CinemaType, cinema.Id_Self));
                //    CinemaType type = CinemaType.cinemacity;
                //    switch (cinema.CinemaType)
                //    {
                //        case "Multikino": type = CinemaType.multikino; break;
                //        case "CinemaCity": type = CinemaType.cinemacity; break;
                //    }
                //    ProcessMovies(movieList, String.Format("{0} - {1}", cinema.CinemaType, cinema.City), type);
                //}
            }
            else
            {
                GetData(dataRequest, filterSet.City);
                cinemaList = dataRequest.CinemaList;
                foreach (Cinema cinema in cinemaList)
                {
                    if (cinema.City.Equals(filterSet.City))
                    {
                        string cinemaName = String.Format("{0} - {1}", cinema.Name, cinema.City);
                        CinemaType cinemaType = CinemaType.multikino;
                        switch (cinema.Name.Contains("Multikino"))
                        {
                            case true: break;
                            case false: cinemaType = CinemaType.cinemacity; break;
                        }
                        ProcessMovies(cinema.MoviesPlayed, cinemaName, cinemaType);
                    }
                }
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
                        check = m.OriginalName.ToLower().Contains(_parameter.Title.ToLower());
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
                    GetScore(m.Id, m.Shows[0].IdCinema);
                    if (userScore.Count != 0)
                    {
                        int i = 0;
                        if (userScore != null)
                        {
                            foreach (UserScore s in userScore)
                            {
                                if (s.Id_Movie.Equals(m.Id) && s.Id_Cinema == m.Shows[0].IdCinema)
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

        private void GetData(DataRequest dataRequest, string city)
        {
            Task.Run(() => dataRequest.ProvideShowsFromCity(city)).Wait();
        }

        public void InitCommands()
        {
            _goToFilterViewCommandCommand = new MvxAsyncCommand(GoToFilterViewAction);
            _goToLocationViewCommandCommand = new MvxAsyncCommand(GoToLocationViewAction);
        }

        private void GetScore(long movieId, long cinemaId)
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

        private async Task GetScoreAsync(long movieId, long cinemaId)
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
