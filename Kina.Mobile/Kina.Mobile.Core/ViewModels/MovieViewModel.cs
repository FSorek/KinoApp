// ---------------------------------------------------------------
// <author>Paul Datsyuk</author>
// <url>https://www.linkedin.com/in/pauldatsyuk/</url>
// ---------------------------------------------------------------

using CoreMultikinoJson;
using DataModel;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using System;
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

        public MovieViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;

            //Temporary hard coded strings
            TitleText = "Simple Test Title";
            URLText = "www.Youtube.com";
            DescriptionText = "Jak zawsze, na moje podziękowania zasługuje wiele osób, bez których ta książka wyglądałaby zupełnie inaczej. Przede wszystkim, mój redaktor i mój agent – Moshe Feder i Joshua Bilmes – dzięki którym projekty osiągają swój pełen potencjał. Jak również moja cudowna żona, Emily, która była dla mnie wielkim wsparciem i pomocą w procesie pisarskim.";
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

        public override Task Initialize(Movie parameter)
        {
            _parameter = parameter;
            TitleText = _parameter.Name;
            DescriptionText = _parameter.Storyline;
            return Task.FromResult(true);
        }
    }
}
