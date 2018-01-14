using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kina.Mobile.Core.ViewModels
{
    class MovieViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;

        public MovieViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;

            //Temporary hard coded strings
            TitleText = "Simple Test Title";
            URLText = "www.Youtube.com";
            DescriptionText = "Jak zawsze, na moje podziękowania zasługuje wiele osób, bez których ta książka wyglądałaby zupełnie inaczej. Przede wszystkim, mój redaktor i mój agent – Moshe Feder i Joshua Bilmes – dzięki którym projekty osiągają swój pełen potencjał. Jak również moja cudowna żona, Emily, która była dla mnie wielkim wsparciem i pomocą w procesie pisarskim.";
        }

        public IMvxAsyncCommand GoToRatePage =>
            new MvxAsyncCommand(async () =>
            {
                await _navigationService.Navigate<MainViewModel>(); //Change to RateView
            });



        public string TitleText { get; set; }
        public string URLText { get; set; }
        public string DescriptionText { get; set; }
    }
}
