using CoreMultikinoJson;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
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

        private bool[] isStarred;
        private int movieID;
        private string title;

        private List<ShowsShowsModel> shows;

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

        public int MovieID
        {
            get { return movieID; }
            set { movieID = value; }
        }

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        public List<ShowsShowsModel> Shows
        {
            get { return shows; }
            set { shows = value; }
        }

        public ShowsMovieModel(int id, string title, List<ShowsShowsModel> shows, double rating, IMvxNavigationService navigationService)
        {
            movieID = id;
            this.title = title;
            this.shows = shows;

            isStarred = new bool[5];

            _navigationService = navigationService;

            InitCommands();

            // Value temporary hardcoded for preview
            InitRating(3.5);
        }

        private async Task GoToMovieViewAction()
        {
            Showing showing = new Showing();

            //await _navigationService.Navigate<FilterViewModel, Showing>(showing);
            await _navigationService.Navigate<MovieViewModel>();
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
