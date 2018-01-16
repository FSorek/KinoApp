using CoreMultikinoJson;
using Kina.Mobile.Core.Model;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;


namespace Kina.Mobile.Core.ViewModels
{
    class ShowsViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;

        private List<ShowsMovieModel> movies;

        public List<ShowsMovieModel> Movies
        {
            get { return movies; }
            set { SetProperty(ref movies, value); }
        }

        public ShowsViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;

            JsonReader jsonReader = new JsonReader();
            Multikino multikino = jsonReader.DeserializeMultikino();

            movies = new List<ShowsMovieModel>();

            List<Film> films = multikino.Films;
            foreach(Film f in multikino.Films)
            {
                List<ShowsShowsModel> shows = new List<ShowsShowsModel>();
                foreach (Showing s in f.Showings)
                {
                    if (s.DateTime.Equals(new DateTime(2018, 1, 8)))
                    {
                        foreach (Time t in s.Times)
                        {
                            shows.Add(new ShowsShowsModel(t.PurpleTime));
                        }
                        int id = 0;
                        int.TryParse(f.Id, out id);
                        ShowsMovieModel movieModel = new ShowsMovieModel(id, f.Title, shows, _navigationService);
                        movies.Add(movieModel);
                        break;
                    }
                }
            }

            #region Temporary hardcoded for preview
            //shows.Add(new ShowsShowsModel(new DateTime(2018, 1, 1, 19, 16, 0)));
            //shows.Add(new ShowsShowsModel(new DateTime(2018, 1, 5, 20, 16, 0)));
            //shows.Add(new ShowsShowsModel(new DateTime(2018, 1, 5, 21, 16, 0)));
            //shows.Add(new ShowsShowsModel(new DateTime(2018, 1, 5, 22, 16, 0)));
            //shows.Add(new ShowsShowsModel(new DateTime(2018, 1, 5, 22, 16, 0)));
            //shows.Add(new ShowsShowsModel(new DateTime(2018, 1, 5, 23, 16, 0)));
            //shows.Add(new ShowsShowsModel(new DateTime(2018, 1, 6, 05, 16, 0)));

            //movies.Add(new ShowsMovieModel(1, "xD", shows, _navigationService));

            //shows = new List<ShowsShowsModel>();

            //shows.Add(new ShowsShowsModel(new DateTime(2018, 1, 5, 13, 16, 0)));
            //shows.Add(new ShowsShowsModel(new DateTime(2018, 1, 2, 11, 16, 0)));
            //movies.Add(new ShowsMovieModel(2, "Title 2", shows, _navigationService));
            #endregion

            InitCommands();
        }

        private void InitCommands()
        {
        }
    }
}
