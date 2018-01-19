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

        private int movieID;
        private string title;

        private List<ShowsShowsModel> shows;

        public int MovieID
        {
            get
            {
                return movieID;
            }
            set
            {
                movieID = value;
            }
        }

        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
            }
        }

        public List<ShowsShowsModel> Shows
        {
            get
            {
                return shows;
            }
            set
            {
                shows = value;
            }
        }

        public ShowsMovieModel(int id, string title, List<ShowsShowsModel> shows, IMvxNavigationService navigationService)
        {
            movieID = id;
            this.title = title;
            this.shows = shows;

            _navigationService = navigationService;

            InitCommands();
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
    }
}
