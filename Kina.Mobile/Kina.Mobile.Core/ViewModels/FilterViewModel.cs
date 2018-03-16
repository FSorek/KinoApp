using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kina.Mobile.Core.Services;
using Kina.Mobile.DataProvider.Providers;

namespace Kina.Mobile.Core.ViewModels
{
    class FilterViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly IAppSettings _settings;

        private MvxAsyncCommand _goToShowsPageCommandCommand;
        private MvxAsyncCommand _goBackCommandCommand;

        public IMvxAsyncCommand GoToShowsPageCommand => _goToShowsPageCommandCommand;
        public IMvxAsyncCommand GoBackCommand => _goBackCommandCommand;

        public List<string> Categories { get; set; }
        public string SelectedCategory { get; set; }

        public string Title { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public FilterViewModel(IMvxNavigationService navigationService, IAppSettings settings)
        {
            _navigationService = navigationService;
            _settings = settings;

            InitCommands();

            DataRequest dataRequest = new DataRequest();
            GetCategories(dataRequest);

            Categories = dataRequest.CategoryList;
        }

        private async Task GoToShowsPageAction()
        {
            if (SelectedCategory != null)
            {
                foreach (var g in Categories)
                {
                    if (SelectedCategory.ToLower().Equals(g.ToLower()))
                    {
                        MvxApp.FilterSettings.Category = g;
                    }
                }
            }
            else MvxApp.FilterSettings.Category = null;
            if (StartTime != null)
            {
                MvxApp.FilterSettings.Start = StartTime.ToString(@"h\:mm");
            }
            else MvxApp.FilterSettings.Start = null;
            if (EndTime != null)
            {
                MvxApp.FilterSettings.End = EndTime.ToString(@"h\:mm");
            }
            else MvxApp.FilterSettings.End = null;
            MvxApp.FilterSettings.Title = Title;
            MvxApp.UsingFilter = true;
            await _navigationService.Navigate<ShowsViewModel>();
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

        private void GetCategories(DataRequest dataRequest)
        {
            Task.Run(() => dataRequest.ProvideCategories()).Wait();
        }
    }
}
