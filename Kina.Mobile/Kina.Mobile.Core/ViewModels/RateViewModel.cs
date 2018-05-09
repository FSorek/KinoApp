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

namespace Kina.Mobile.Core.ViewModels
{
    public class RateViewModel : MvxViewModel<BasicShowData>
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly IDataService _dataService;
        private readonly IAppSettings _settings;

        private MvxAsyncCommand _submitCommandCommand;
        private MvxAsyncCommand _goBackCommandCommand;

        private long cinemaID;
        private long movieID;

        public IMvxAsyncCommand SubmitCommand => _submitCommandCommand;
        public IMvxAsyncCommand GoBackCommand => _goBackCommandCommand;

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
            _goBackCommandCommand = new MvxAsyncCommand(GoBackAction);
        }

        private async Task SubmitAction()
        {
            List<UserScore> userScore = await _dataService.GetRating(movieID, cinemaID);
            string userID = Hardware.DeviceId;
            try
            {
                UserScore score = new UserScore
                {
                    IdUser = 0,
                    IdStringUser = userID,
                    IdCinema = cinemaID,
                    IdMovie = movieID,
                    Screen = (long)ScreenRating,
                    Seat = (long)SeatsRating,
                    Sound = (long)SoundRating,
                    Popcorn = (long)SnacksRating,
                    Cleanliness = (long)CleanlinessRating
                };
                if (await _dataService.PostScore(score))
                {
                    Mvx.Resolve<IUserDialogs>().Alert("You have already scored this show!");
                }
            } catch(System.Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message + e.StackTrace);
            }

            await _navigationService.Close(this);

        }

        private async Task GoBackAction()
        {
            await _navigationService.Close(this);
        }

        public override void Prepare(BasicShowData parameter)
        {
            cinemaID = parameter.IdCinema;
            movieID = parameter.IdMovie;
        }
    }
}
