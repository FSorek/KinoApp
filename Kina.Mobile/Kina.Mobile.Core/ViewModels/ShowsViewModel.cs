using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using DataModel;
using Android;
using Kina.Mobile.Core.Model;
using System.Reflection;
using System.IO;


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

            var assembly = typeof(JsonReader).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream("Model.multikino.json");
            string text = "";
            using (var reader = new System.IO.StreamReader(stream))
            {
                text = reader.ReadToEnd();
            }



            movies = new List<ShowsMovieModel>();
            
            #region Temporary hardcoded for preview
            List<ShowsShowsModel> shows = new List<ShowsShowsModel>();
            shows.Add(new ShowsShowsModel(new DateTime(2018, 1, 1, 19, 16, 0)));
            shows.Add(new ShowsShowsModel(new DateTime(2018, 1, 5, 20, 16, 0)));
            shows.Add(new ShowsShowsModel(new DateTime(2018, 1, 5, 21, 16, 0)));
            shows.Add(new ShowsShowsModel(new DateTime(2018, 1, 5, 22, 16, 0)));
            shows.Add(new ShowsShowsModel(new DateTime(2018, 1, 5, 22, 16, 0)));
            shows.Add(new ShowsShowsModel(new DateTime(2018, 1, 5, 23, 16, 0)));
            shows.Add(new ShowsShowsModel(new DateTime(2018, 1, 6, 05, 16, 0)));

            movies.Add(new ShowsMovieModel(1, "xD", shows, _navigationService));

            shows = new List<ShowsShowsModel>();

            shows.Add(new ShowsShowsModel(new DateTime(2018, 1, 5, 13, 16, 0)));
            shows.Add(new ShowsShowsModel(new DateTime(2018, 1, 2, 11, 16, 0)));
            movies.Add(new ShowsMovieModel(2, "Title 2", shows, _navigationService));
            #endregion

            InitCommands();
        }

        private void InitCommands()
        {
        }
    }
}
