using DataModel;
using Kina.Mobile.DataProvider.Providers;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Kina.Mobile.Core.ViewModels
{
    class ShowsViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly Services.IAppSettings _settings;

        private List<ShowsMovieModel> movies;
        private List<UserScore> userScore;

        public List<ShowsMovieModel> Movies
        {
            get { return movies; }
            set { SetProperty(ref movies, value); }
        }

        public ShowsViewModel(IMvxNavigationService navigationService, Services.IAppSettings settings)
        {
            _navigationService = navigationService;
            _settings = settings;

            DataRequestService dataRequestService = new DataRequestService();
            InitList(dataRequestService);
            List<Movie> movieList = dataRequestService.MovieList;

            var today = DateTime.Today;
            movies = new List<ShowsMovieModel>();

            foreach(Movie m in movieList)
            {
                if (m.Shows.Count != 0)
                {
                    double score = 0.0;
                    GetScore(m.Id_Movie, m.Shows[0].Id_Cinema);
                    if(userScore.Count != 0)
                    {
                        int i = 0;
                        if(userScore != null)
                        {
                            foreach (UserScore s in userScore)
                            {
                                if(s.Id_Movie.Equals(m.Id_Movie) && s.Id_Cinema == m.Shows[0].Id_Cinema)
                                {
                                    score += (s.Screen + s.Seat + s.Sound + s.Popcorn) / 4.0;
                                    i++;
                                }
                            }
                            score /= i;
                        }
                    }
                    movies.Add(new ShowsMovieModel(m, score, _navigationService));
                }
            }
        }

        private void InitList(DataRequestService dataRequestService)
        {
            // Older version using JsonReader for static testing
            //JsonReader jsonReader = new JsonReader();
            //Multikino multikino = jsonReader.DeserializeMultikino();
            //List<Film> films = multikino.Films;
            //return films;

            // Krewetka = 1073, multikino = 14
            Task.Run(() => dataRequestService.ProvideData(CinemaType.multikino, 14)).Wait();
            Debug.WriteLine("I'm here");
        }
        private void GetScore(string movieId, int cinemaId)
        {
            Task.Run(() => GetScoreAsync(movieId, cinemaId)).Wait();
        }

        private async Task GetScoreAsync(string movieId, int cinemaId)
        {
            userScore = await MvxApp.Database.GetUserScoreAsync(cinemaId, movieId);
        }

        // Will be needed if we implement filtering so let it be commentd
        //public override Task Initialize(Showing parameter)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
