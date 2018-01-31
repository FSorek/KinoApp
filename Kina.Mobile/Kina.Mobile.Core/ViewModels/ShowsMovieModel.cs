using CoreMultikinoJson;
using DataModel;
using Kina.Mobile.Core.Model;
using Kina.Mobile.Core.Services;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Kina.Mobile.Core.ViewModels
{
    class ShowsMovieModel
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly IAppSettings _settings;

        private MvxAsyncCommand _goToScoreViewCommandCommand;
        private MvxAsyncCommand _goToMovieViewCommandCommand;
        public ICommand GoToScoreViewCommand => _goToScoreViewCommandCommand;
        public ICommand GoToMovieViewCommand => _goToMovieViewCommandCommand;

        private Movie movie;

        private bool[] isStarred;
        private string movieID;
        private string title;
        private string cinemaName;

        private List<Show> shows;

        public bool IsStarredOne
        {
            get { return isStarred[0]; }
        }
        public bool IsStarredTwo
        {
            get { return isStarred[1]; }
        }
        public bool IsStarredThree
        {
            get { return isStarred[2]; }
        }
        public bool IsStarredFour
        {
            get { return isStarred[3]; }
        }
        public bool IsStarredFive
        {
            get { return isStarred[4]; }
        }

        public string MovieID
        {
            get { return movieID; }
            set { movieID = value; }
        }

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        public List<Show> Shows
        {
            get { return shows; }
            set { shows = value; }
        }

        public ShowsMovieModel(Movie movie, double rating, IMvxNavigationService navigationService, FilterSet parameter, IAppSettings settings, string cinemaName)
        {
            _settings = settings;
            var date = DateTime.Today.Date;
            movieID = movie.Id_Movie;
            title = movie.Name;
            shows = new List<Show>();
            this.cinemaName = cinemaName;
            foreach(Show s in movie.Shows)
            {
                bool check = true;
                if(parameter != null)
                {
                    int showHour = int.Parse(s.Start.Split(':')[0]);
                    int parameterHourStart = int.Parse(parameter.Start.Split(':')[0]);
                    int parameterHourEnd = int.Parse(parameter.End.Split(':')[0]);

                    check = ((showHour > (parameterHourStart)) && (showHour < (parameterHourEnd))) || (parameterHourStart == 0 && parameterHourEnd == 0);
                }
                if (s.ShowDate.Date.Equals(DateTime.Today.Date) && check)
                {
                    shows.Add(s);
                }
            }
            this.movie = movie;

            isStarred = new bool[5];

            _navigationService = navigationService;

            InitCommands();
            InitRating(rating);
        }

        private async Task GoToMovieViewAction()
        {
            Movie parameter = movie;
            MvxApp.UsingFilter = false;
            MvxApp.FilterSettings.ClearFilter();
            await _navigationService.Navigate<MovieViewModel, Movie>(parameter);
        }

        private async Task GoToScoreViewAction()
        {
            MovieDataSet parameter = new MovieDataSet(movie, cinemaName);
            MvxApp.UsingFilter = false;
            MvxApp.FilterSettings.ClearFilter();
            await _navigationService.Navigate<ScoreViewModel, MovieDataSet>(parameter);
        }

        private void InitCommands()
        {
            _goToScoreViewCommandCommand = new MvxAsyncCommand(GoToScoreViewAction);
            _goToMovieViewCommandCommand = new MvxAsyncCommand(GoToMovieViewAction);
        }

        private void InitRating(double rating)
        {
            int starRate = (int)rating;
            for(int i = 0; i < starRate; i++)
            {
                isStarred[i] = true;
            }
        }
    }
}
