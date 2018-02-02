// ---------------------------------------------------------------
// <author>Paul Datsyuk</author>
// <url>https://www.linkedin.com/in/pauldatsyuk/</url>
// ---------------------------------------------------------------

using Kina.Mobile.Core.Helpers;
using Kina.Mobile.DataProvider.Models;
using Kina.Mobile.DataProvider.Providers;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Kina.Mobile.Core.ViewModels
{
    class MovieViewModel : MvxViewModel<Movie>
    {
        private readonly IMvxNavigationService _navigationService;
        //private readonly Services.IAppSettings _settings;

        private MvxAsyncCommand _goToLocationViewCommandCommand;
        private MvxAsyncCommand _goToRateViewCommandCommand;

        public IMvxAsyncCommand GoToLocationViewCommand => _goToLocationViewCommandCommand;
        public IMvxAsyncCommand GoToRateViewCommand => _goToRateViewCommandCommand;

        private Movie _parameter;

        public string TitleText { get; set; }
        public string URLText { get; set; }
        public string DescriptionText { get; set; }
        public string Year { get; set; }
        public string Cast { get; set; }
        public string Director { get; set; }

        public MovieViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;

            InitCommands();
        }

        public IMvxCommand OpenYoutubeUrlCommand =>
            new MvxCommand(() =>
            {
                Device.OpenUri(new Uri("https://www.youtube.com/"));
            });

        private async Task GoToRateViewAction()
        {
            await _navigationService.Navigate<RateViewModel, Movie>(_parameter);
        }

        private async Task GoToLocationViewAction()
        {
            await _navigationService.Navigate<LocationViewModel>();
        }

        private void InitCommands()
        {
            _goToLocationViewCommandCommand = new MvxAsyncCommand(GoToLocationViewAction);
            _goToRateViewCommandCommand = new MvxAsyncCommand(GoToRateViewAction);
        }

        private void GetMovieData(DataRequest dataRequest, long id)
        {
            Task.Run(() => dataRequest.ProvideMovieData(id)).Wait();
        }

        public override Task Initialize(Movie parameter)
        {
            _parameter = parameter;
            DataRequest dataRequest = new DataRequest();
            GetMovieData(dataRequest, _parameter.Id);
            Movie requested = dataRequest.SelectedMovie;
            TitleText = requested.OriginalName;
            DescriptionText = requested.Storyline;
            Director = requested.Director;
            URLText = requested.Trailer;
            Cast = requested.Stars;
            Year = null;

            return Task.FromResult(true);
        }
    }
}
