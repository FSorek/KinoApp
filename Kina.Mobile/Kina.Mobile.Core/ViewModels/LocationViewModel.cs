using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Kina.Mobile.Core.ViewModels
{
    class LocationViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private MvxAsyncCommand _confirmLocationCommandCommand;
        private MvxAsyncCommand _autoLocateCommandCommand;

        private List<string> location;
        private string selectedLocation;

        public ICommand ConfirmLocationCommand => _confirmLocationCommandCommand;
        public ICommand AutoLocateCommand => _autoLocateCommandCommand;

        public List<string> Location
        {
            get { return location; }
            set { SetProperty(ref location, value); }
        }

        public string SelectedLocation
        {
            get { return selectedLocation; }
            set { SetProperty(ref selectedLocation, value); }
        }

        public LocationViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;

            #region Temporary hardcoded content
            location = new List<string>();
            location.Add("Gdańsk");
            #endregion

            InitCommands();
        }

        private async Task ConfirmLocationAction()
        {
            var param = selectedLocation;

            // To be determined, what acctualy this task should do
            await _navigationService.Close(this);
        }

        private async Task AutoLocateAction()
        {
            // To be determined, what acctualy this task should do
            await _navigationService.Close(this);
        }

        private void InitCommands()
        {
            _autoLocateCommandCommand = new MvxAsyncCommand(AutoLocateAction);
            _confirmLocationCommandCommand = new MvxAsyncCommand(ConfirmLocationAction);
        }
    }
}
