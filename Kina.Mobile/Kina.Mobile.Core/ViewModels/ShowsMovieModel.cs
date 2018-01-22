using CoreMultikinoJson;
using DataModel;
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

        private MvxAsyncCommand _goToMovieViewCommandCommand;
        public ICommand GoToMovieViewCommand => _goToMovieViewCommandCommand;

        private Movie movie;

        private bool[] isStarred;
        private string movieID;
        private string title;

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

        public ShowsMovieModel(Movie movie, double rating, IMvxNavigationService navigationService)
        {
            var date = DateTime.Today.Date;
            movieID = movie.Id_Movie;
            title = movie.Name;
            shows = new List<Show>();
            foreach(Show s in movie.Shows)
            {
                if (s.ShowDate.Date.Equals(DateTime.Today.Date))
                {
                    shows.Add(s);
                }
            }
            this.movie = movie;

            isStarred = new bool[5];

            _navigationService = navigationService;

            InitCommands();

            // Value temporary hardcoded for preview
            InitRating(rating);
        }

        private async Task GoToMovieViewAction()
        {
            Movie param = movie;

            //await _navigationService.Navigate<FilterViewModel, Showing>(showing);
            await _navigationService.Navigate<MovieViewModel, Movie>(param);
        }

        private void InitCommands()
        {
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
