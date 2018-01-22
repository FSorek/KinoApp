using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using DataModel;

namespace Kina.Mobile.Core.ViewModels
{
    public class RateViewModel : MvxViewModel<Movie>
    {
        private readonly IMvxNavigationService _navigationService;

        private MvxAsyncCommand _submitCommandCommand;
        private MvxAsyncCommand _goBackCommandCommand;

        public IMvxAsyncCommand SubmitCommand => _submitCommandCommand;
        public IMvxAsyncCommand GoBackCommand => _goBackCommandCommand;

        private Movie _parameter;
        private int cinemaID;
        private string movieID;

        public int ScreenRate { get; set; }
        public int SeatsRate { get; set; }
        public int SoundRate { get; set; }
        public int PopcornRate { get; set; }

        public List<String> Cinemas { get; set; }
        public string SelectedCinema { get; set; }

        public RateViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;

            InitCommands();

            Cinemas = new List<String>();
            Cinemas.Add("Multikino X");
            Cinemas.Add("Mutlikino Y");
            Cinemas.Add("CinemaCity X");
        }

        private void InitCommands()
        {
            _submitCommandCommand = new MvxAsyncCommand(SubmitAction);
            _goBackCommandCommand = new MvxAsyncCommand(GoBackAction);
        }

        private async Task SubmitAction()
        {
            UserScore score = new UserScore();
            score.Id_User = 1;
            score.Id_Cinema = cinemaID;
            score.Id_Movie = movieID;
            score.Screen = ScreenRate;
            score.Seat = SeatsRate;
            score.Sound = SoundRate;
            score.Popcorn = PopcornRate;
            await MvxApp.Database.SaveUserScoreAsync(score);
            await _navigationService.Navigate<ShowsViewModel>();
        }

        private async Task GoBackAction()
        {
            await _navigationService.Close(this);
        }

        public override Task Initialize(Movie parameter)
        {
            _parameter = parameter;
            Show show = _parameter.Shows[0];
            cinemaID = show.Id_Cinema;
            movieID = parameter.Id_Movie;
            return Task.FromResult(true);
        }
    }
}