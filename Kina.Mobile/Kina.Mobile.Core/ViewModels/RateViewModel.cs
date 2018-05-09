using Acr.UserDialogs;
using Kina.Mobile.Core.Helpers;
using Kina.Mobile.Core.Model;
using Kina.Mobile.Core.Services;
using Kina.Mobile.DataProvider.Models;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Kina.Mobile.Core.ViewModels
{
    public class RateViewModel : MvxViewModel<BasicShowData>
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly IDataService _dataService;
        private readonly IAppSettings _settings;

        private MvxAsyncCommand _submitCommandCommand;

        private long cinemaID;
        private long movieID;

        public IMvxAsyncCommand SubmitCommand => _submitCommandCommand;

        public double CleanlinessRating { get; set; }
        public double ScreenRating { get; set; }
        public double SeatsRating { get; set; }
        public double SoundRating { get; set; }
        public double SnacksRating { get; set; }

        public RateViewModel(IMvxNavigationService navigationService, IDataService dataService, IAppSettings settings)
        {
            _navigationService = navigationService;
            _dataService = dataService;
            _settings = settings;

            InitCommands();
        }

        private void InitCommands()
        {
            _submitCommandCommand = new MvxAsyncCommand(SubmitAction);
        }

        private async Task SubmitAction()
        {
            List<UserScore> userScore = await _dataService.GetRating(movieID, cinemaID);
            string userId = null;
            if (Device.RuntimePlatform != Device.UWP)
            {
                userId = Hardware.DeviceId;
            }

            UserScore score = new UserScore
            {
                IdUser = 0,
                IdStringUser = userId,
                IdCinema = cinemaID,
                IdMovie = movieID,
                Screen = (long)ScreenRating,
                Seat = (long)SeatsRating,
                Sound = (long)SoundRating,
                Popcorn = (long)SnacksRating,
                Cleanliness = (long)CleanlinessRating
            };
            if (!await _dataService.PostScore(score))
            {
                Mvx.Resolve<IUserDialogs>().Alert("You have already scored this show!");
            }
            //else
            //{
            //    Mvx.Resolve<IUserDialogs>().Alert("Thank you for scoring this show!");
            //}

            await _navigationService.Close(this);
        }

        public override void Prepare(BasicShowData parameter)
        {
            cinemaID = parameter.IdCinema;
            movieID = parameter.IdMovie;
        }
    }
}
