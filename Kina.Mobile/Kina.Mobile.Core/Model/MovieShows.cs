using Kina.Mobile.Core.ViewModels;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Kina.Mobile.Core.Model
{
    public class MovieShows
    {
        //private readonly IMvxNavigationService _navigationService;

        //private IMvxAsyncCommand
        //private BasicShowData _basicShowData;
        public IMvxAsyncCommand GoToMoviePageCommand { get; set; }

        public ImageSource ImageSource { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Shows { get; set; }
        public double Rating { get; set; }

        public MovieShows(ImageSource imageSource, string title, string genre, string shows, double rating,
            IMvxAsyncCommand command)
        {
            //_basicShowData = showData;
            ImageSource = imageSource;
            Title = title;
            Genre = genre;
            Shows = shows;
            Rating = rating;
            //_navigationService = navigationService;
            GoToMoviePageCommand = command;
        }

        public MovieShows(string title, string genre, string shows, double rating, IMvxAsyncCommand command)
            : this(null, title, genre, shows, rating, command)
        {

        }

        public MovieShows(string title, string genre, string shows, double rating)
            : this(title, genre, shows, rating, null)
        {

        }

        //private async Task GoToMoviePageAsync()
        //{
        //    MvxApp.UsingFilter = false;
        //    MvxApp.FilterSettings.ClearFilter();
        //    await _navigationService.Navigate<MovieViewModel, BasicShowData>(_basicShowData);
        //}
    }
}
