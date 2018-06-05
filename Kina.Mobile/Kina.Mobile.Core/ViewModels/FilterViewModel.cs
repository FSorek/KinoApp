using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kina.Mobile.Core.Services;

namespace Kina.Mobile.Core.ViewModels
{
    class FilterViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly IDataService _dataService;
        private readonly IFilterService _filterService;
        private readonly IAppSettings _settings;

        private MvxAsyncCommand _goToShowsPageCommandCommand;

        public IMvxAsyncCommand GoToShowsPageCommand => _goToShowsPageCommandCommand;

        public List<string> Categories { get; set; }
        public string SelectedCategory { get; set; }

        public string Title { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public FilterViewModel(IMvxNavigationService navigationService, IDataService dataService, IFilterService filterService, IAppSettings settings)
        {
            _navigationService = navigationService;
            _dataService = dataService;
            _filterService = filterService;
            _settings = settings;

            InitCommands();

            Categories = Task.Run(() => _dataService.GetCategories()).Result;
        }

        private async Task GoToShowsPageAction()
        {
            _filterService.ClearFilter();
            if (SelectedCategory != null)
            {
                foreach (var g in Categories)
                {
                    if (SelectedCategory.ToLower().Equals(g.ToLower()))
                    {
                        _filterService.Category = g;
                        _filterService.IsActive = true;
                    }
                }
            }
            else _filterService.Category = null;

            if (EndTime != default(TimeSpan))
            {
                _filterService.Start = StartTime;
                _filterService.IsActive = true;
            }

            if (EndTime != default(TimeSpan))
            {
                _filterService.End = EndTime;
                _filterService.IsActive = true;
            }

            if (Title != null)
            {
                if (Title.Length > 0)
                {
                    _filterService.Title = Title;
                    _filterService.IsActive = true;
                }
            }
            else _filterService.Title = null;

            await _navigationService.Close(this);
        }

        private void InitCommands()
        {
            _goToShowsPageCommandCommand = new MvxAsyncCommand(GoToShowsPageAction);
        }
    }
}
