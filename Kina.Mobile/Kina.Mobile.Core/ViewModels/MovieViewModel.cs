// ---------------------------------------------------------------
// <author>Paul Datsyuk</author>
// <url>https://www.linkedin.com/in/pauldatsyuk/</url>
// ---------------------------------------------------------------

using CoreMultikinoJson;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Kina.Mobile.Core.ViewModels
{
    class MovieViewModel : MvxViewModel<Showing>
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly Services.IAppSettings _settings;

        private MvxAsyncCommand _goToRateViewCommandCommand;

        public IMvxAsyncCommand GoToRateViewCommand => _goToRateViewCommandCommand;

        private Showing _parameter;

        public MovieViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;

            //Temporary hard coded strings
            TitleText = "Simple Test Title";
            URLText = "www.Youtube.com";
            DescriptionText = "Jak zawsze, na moje podziękowania zasługuje wiele osób, bez których ta książka wyglądałaby zupełnie inaczej. Przede wszystkim, mój redaktor i mój agent – Moshe Feder i Joshua Bilmes – dzięki którym projekty osiągają swój pełen potencjał. Jak również moja cudowna żona, Emily, która była dla mnie wielkim wsparciem i pomocą w procesie pisarskim.";
        }

        public IMvxCommand OpenYoutubeUrlCommand =>
            new MvxCommand(() =>
            {
                Device.OpenUri(new Uri("https:/youtube.com/"));
            });

        private async Task GoToRateViewAction()
        {
            await _navigationService.Navigate<RateViewModel>();
        }


        private void InitCommands()
        {
            _goToRateViewCommandCommand = new MvxAsyncCommand(GoToRateViewAction);
        }

        public IMvxAsyncCommand GoToRatePage =>
            new MvxAsyncCommand(async () =>
            {
                await _navigationService.Navigate<RateViewModel>();
            });

        public IMvxAsyncCommand GoToLocationPage =>
            new MvxAsyncCommand(async () =>
            {
                await _navigationService.Navigate<LocationViewModel>();
            });


        public string TitleText { get; set; }
        public string URLText { get; set; }
        public string DescriptionText { get; set; }

        public override Task Initialize(Showing parameter)
        {
            throw new NotImplementedException();
        }
    }
}
