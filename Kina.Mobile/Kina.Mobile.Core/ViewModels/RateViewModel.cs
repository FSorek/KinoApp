using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kina.Mobile.Core.Services;
using MvvmCross.Platform;
using Acr.UserDialogs;
using Kina.Mobile.DataProvider.Models;
using Kina.Mobile.Core.Helpers;
using Kina.Mobile.DataProvider.Providers;
using Kina.Mobile.Core.Model;

namespace Kina.Mobile.Core.ViewModels
{
    public class RateViewModel : MvxViewModel<BasicShowData>
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly IAppSettings _settings;

        private MvxAsyncCommand _submitCommandCommand;
        private MvxAsyncCommand _goBackCommandCommand;

        public IMvxAsyncCommand SubmitCommand => _submitCommandCommand;
        public IMvxAsyncCommand GoBackCommand => _goBackCommandCommand;

        private long cinemaID;
        private long movieID;

        public int ScreenRate { get; set; }
        public int SeatsRate { get; set; }
        public int SoundRate { get; set; }
        public int PopcornRate { get; set; }
        public int CleanlinessRate { get; set; }

        public string SelectedCinema { get; set; }

        public RateViewModel(IMvxNavigationService navigationService, IAppSettings settings)
        {
            _navigationService = navigationService;
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
            bool isInBase = false;
            DataRequest dataRequest = new DataRequest();
            GetScore(movieID, cinemaID, dataRequest);
            List<UserScore> userScore = dataRequest.ShowScore;
            string userID = Hardware.DeviceId;
            foreach(UserScore score in userScore)
            {
                if (score.IdStringUser.Equals(userID))
                {
                    Mvx.Resolve<IUserDialogs>().Alert("You have already scored this show!");
                    isInBase = true;
                    return;
                }
            }

            if (isInBase)
            {
                await _navigationService.Close(this);
            }
            else
            {
                UserScore score = new UserScore
                {
                    IdStringUser = userID,
                    IdCinema = cinemaID,
                    IdMovie = movieID,
                    Screen = ScreenRate,
                    Seat = SeatsRate,
                    Sound = SoundRate,
                    Popcorn = PopcornRate,
                    Cleanliness = CleanlinessRate
                };
                await dataRequest.PostScoreAsync(score);
                await _navigationService.Close(this);
            }
        }

        private async Task GoBackAction()
        {
            await _navigationService.Close(this);
        }

        private void GetScore(long movieId, long cinemaId, DataRequest dataRequest)
        {
            Task.Run(() => dataRequest.ProvideScoreData(movieId, cinemaId)).Wait();
        }

        public override void Prepare(BasicShowData parameter)
        {
            cinemaID = parameter.IdCinema;
            movieID = parameter.IdMovie;
        }
    }
}