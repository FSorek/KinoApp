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
using Kina.Mobile.Core.Converters;

namespace Kina.Mobile.Core.ViewModels
{
    public class RateViewModel : MvxViewModel<BasicShowData>
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly IAppSettings _settings;

        private MvxAsyncCommand _submitCommandCommand;
        private MvxAsyncCommand _goBackCommandCommand;

        private bool[] screenRateMarked;
        private bool[] seatsRateMarked;
        private bool[] soundRateMarked;
        private bool[] popcornRateMarked;
        private bool[] cleanlinessRateMarked;
        private long cinemaID;
        private long movieID;

        public IMvxAsyncCommand SubmitCommand => _submitCommandCommand;
        public IMvxAsyncCommand GoBackCommand => _goBackCommandCommand;

        public bool[] ScreenRateMarked => screenRateMarked;
        public bool[] SeatsRateMarked => seatsRateMarked;
        public bool[] SoundRateMarked => soundRateMarked;
        public bool[] PopcornRateMarked => popcornRateMarked;
        public bool[] CleanlinessRateMarked => cleanlinessRateMarked;

        public string SelectedCinema { get; set; }

        public RateViewModel(IMvxNavigationService navigationService, IAppSettings settings)
        {
            _navigationService = navigationService;
            _settings = settings;
            screenRateMarked = new bool[5];
            seatsRateMarked = new bool[5];
            soundRateMarked = new bool[5];
            popcornRateMarked = new bool[5];
            cleanlinessRateMarked = new bool[5];

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
            BooleanRateConverter booleanRateConverter = new BooleanRateConverter();
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
                    Screen = booleanRateConverter.Convert(screenRateMarked),
                    Seat = booleanRateConverter.Convert(seatsRateMarked),
                    Sound = booleanRateConverter.Convert(soundRateMarked),
                    Popcorn = booleanRateConverter.Convert(popcornRateMarked),
                    Cleanliness = booleanRateConverter.Convert(cleanlinessRateMarked)
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