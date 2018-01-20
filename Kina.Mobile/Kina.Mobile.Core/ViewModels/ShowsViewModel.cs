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

            DateTime d = new DateTime(2018, 1, 8);
            movies = new List<ShowsMovieModel>();

            List<Film> films = InitList();
            foreach(Film f in films)
            {
                List<ShowsShowsModel> shows = new List<ShowsShowsModel>();
                foreach (Showing s in f.Showings)
                {
                    if (s.DateTime.Equals(d))
                    {
                        foreach (Time t in s.Times)
                        {
                            shows.Add(new ShowsShowsModel(t.PurpleTime));
                        }
                        int id = 0;
                        int.TryParse(f.Id, out id);
                        ShowsMovieModel movieModel = new ShowsMovieModel(id, f.Title, shows, 3.5, _navigationService);
                        movies.Add(movieModel);
                        break;
                    }
                }
            }

            InitCommands();
        }

        private void InitCommands()
        {
        }

        private List<Film> InitList()
        {
            JsonReader jsonReader = new JsonReader();
            Multikino multikino = jsonReader.DeserializeMultikino();
            List<Film> films = multikino.Films;
            return films;
        }
    }
}
