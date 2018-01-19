using CoreMultikinoJson;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kina.Mobile.Core.ViewModels
{
    class FilterViewModel : MvxViewModel<Showing>
    {
        private readonly IMvxNavigationService _navigationService;

        private MvxAsyncCommand _goToShowsPageCommandCommand;
        private MvxAsyncCommand _goBackCommandCommand;

        private Showing _parameter;

        public IMvxAsyncCommand GoToShowsPageCommand => _goToShowsPageCommandCommand;
        public IMvxAsyncCommand GoBackCommand => _goBackCommandCommand;

        public List<string> Genre { get; set; }
        public string SelectedGenre { get; set; }

        public string Title { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        //public FilterViewModel(IMvxNavigationService navigationService)
        //{
        //    _navigationService = navigationService;

        //    InitCommands();

        //    // Temporary hardcoded list
        //    Genre = new List<string>();
        //    Genre.Add("Horror");
        //    Genre.Add("Fantasy");
        //    Genre.Add("Sci-Fi");

        //}

        public FilterViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;

            InitCommands();

            // Temporary hardcoded list
            Genre = new List<string>();
            Genre.Add("Horror");
            Genre.Add("Fantasy");
            Genre.Add("Sci-Fi");

        }

        public override Task Initialize(Showing parameter)
        {
            _parameter = parameter;
            // return base.Initialize();
            return Task.FromResult(true);
        }

        private async Task GoToShowsPageAction()
        {
            await _navigationService.Navigate<MainViewModel>();
        }

        private async Task GoBackAction()
        {
            await _navigationService.Close(this);
        }

        private void InitCommands()
        {
            _goToShowsPageCommandCommand = new MvxAsyncCommand(GoToShowsPageAction);
            _goBackCommandCommand = new MvxAsyncCommand(GoBackAction);
        }
    }
}
