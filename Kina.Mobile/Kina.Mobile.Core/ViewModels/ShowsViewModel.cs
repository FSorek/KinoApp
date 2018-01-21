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
                    movies.Add(new ShowsMovieModel(m, 3.5, _navigationService));
                }
            }

            InitCommands();
        }

        private void InitCommands()
        {
        }

        private void InitList(DataRequestService dataRequestService)
        {
            //JsonReader jsonReader = new JsonReader();
            //Multikino multikino = jsonReader.DeserializeMultikino();
            //List<Film> films = multikino.Films;
            //return films;

            Task.Run(() => dataRequestService.ProvideData(CinemaType.cinemacity, 1073)).Wait();
            Debug.WriteLine("I'm here");
        }
    }
}
