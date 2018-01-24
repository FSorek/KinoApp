using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataModel;
using Kina.Mobile.Core.Model;
using Kina.Mobile.Core.Services;

namespace Kina.Mobile.Core.ViewModels
{
    class FilterViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly IAppSettings _settings;

        private MvxAsyncCommand _goToShowsPageCommandCommand;
        private MvxAsyncCommand _goBackCommandCommand;

        private List<Genre> genre;

        public IMvxAsyncCommand GoToShowsPageCommand => _goToShowsPageCommandCommand;
        public IMvxAsyncCommand GoBackCommand => _goBackCommandCommand;

        public List<string> Genre { get; set; }
        public string SelectedGenre { get; set; }

        public string Title { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public FilterViewModel(IMvxNavigationService navigationService, IAppSettings settings)
        {
            _navigationService = navigationService;
            _settings = settings;

            InitCommands();

            GetGenres();

            Genre = new List<string>();

            foreach(var g in genre)
            {
                Genre.Add(g.EngName);
            }
        }

        private async Task GoToShowsPageAction()
        {
            FilterSet parameter = new FilterSet();
            if (SelectedGenre != null)
            {
                foreach (var g in genre)
                {
                    if (SelectedGenre.ToLower().Equals(g.EngName.ToLower()))
                    {
                        parameter.Genre = g;
                    }
                }
            }
            if(StartTime != null)
            {
                parameter.Start = StartTime.ToString(@"h\:mm");
            }
            //parameter.End = EndTime.ToString(@"h\:mm");
            if(Title != null)
            {
                parameter.Title = Title;
            }
            MvxApp.UsingFilter = true;
            await _navigationService.Navigate<ShowsViewModel, FilterSet>(parameter);
        }

        private async Task GoBackAction()
        {
            MvxApp.UsingFilter = false;
            await _navigationService.Close(this);
        }

        private void InitCommands()
        {
            _goToShowsPageCommandCommand = new MvxAsyncCommand(GoToShowsPageAction);
            _goBackCommandCommand = new MvxAsyncCommand(GoBackAction);
        }

        private void GetGenres()
        {
            Task.Run(() => GetGenreAsync()).Wait();
        }

        private async Task GetGenreAsync()
        {
            genre = await MvxApp.Database.GetGenreAsync();
        }
    }
}
