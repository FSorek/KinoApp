using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;


namespace Kina.Mobile.Core.ViewModels
{
    public class RateViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;


        private MvxAsyncCommand _submitCommandCommand;
        private MvxAsyncCommand _goBackCommandCommand;

        public IMvxAsyncCommand SubmitCommand => _submitCommandCommand;
        public IMvxAsyncCommand GoBackCommand => _goBackCommandCommand;

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
            await _navigationService.Navigate<MovieViewModel>();
        }

        private async Task GoBackAction()
        {
            await _navigationService.Close(this);
        }

    }
}