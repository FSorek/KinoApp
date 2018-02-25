using Kina.Mobile.Core.ViewModels;
using Kina.Mobile.DataProvider.Models;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Kina.Mobile.Core.Model
{
    class MovieListItem
    {
        private readonly IMvxNavigationService _navigationService;

        private MvxAsyncCommand _goToScoreViewCommandCommand;
        private MvxAsyncCommand _goToMovieViewCommandCommand;
        public ICommand GoToScoreViewCommand => _goToScoreViewCommandCommand;
        public ICommand GoToMovieViewCommand => _goToMovieViewCommandCommand;

        private SimpleMovie movie;
        private BasicShowData basicShowData;

        private bool[] isStarred;
        private string title;

        private List<SimpleShow> shows;

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

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        public List<SimpleShow> Shows
        {
            get { return shows; }
            set { shows = value; }
        }

        public MovieListItem(BasicShowData basicShowData, SimpleMovie movie, double overallRating, IMvxNavigationService navigationService)
        {
            var date = DateTime.Today.Date;
            this.basicShowData = basicShowData;
            title = movie.Name;
            shows = new List<SimpleShow>();
            foreach(SimpleShow s in movie.Shows)
            {
                string start = MvxApp.FilterSettings.Start;
                string end = MvxApp.FilterSettings.End;
                bool check = true;
                if(start != null && end != null)
                {
                    int showHour = int.Parse(s.Start.Split(':')[0]);
                    int parameterHourStart = int.Parse(start.Split(':')[0]);
                    int parameterHourEnd = int.Parse(end.Split(':')[0]);

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
            InitRating(overallRating);
        }

        private async Task GoToMovieViewAction()
        {
            MvxApp.UsingFilter = false;
            MvxApp.FilterSettings.ClearFilter();
            await _navigationService.Navigate<MovieViewModel, BasicShowData>(basicShowData);
        }

        private async Task GoToScoreViewAction()
        {
            MvxApp.UsingFilter = false;
            MvxApp.FilterSettings.ClearFilter();
            await _navigationService.Navigate<ScoreViewModel, BasicShowData>(basicShowData);
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
