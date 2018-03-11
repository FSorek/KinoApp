using MvvmCross.Core.ViewModels;
using Xamarin.Forms;

namespace Kina.Mobile.Core.Model
{
    public class NavigationItem
    {
        private MvxAsyncCommand _itemNavigationCommandCommand;

        private ImageSource _icon;
        private string _itemName;

        public IMvxAsyncCommand ItemNavigationCommand => _itemNavigationCommandCommand;

        public ImageSource Icon => _icon;
        public string ItemName => _itemName;

        public NavigationItem(string itemName, ImageSource icon, IMvxAsyncCommand itemNavigationCommand)
        {
            _itemName = itemName;
            _icon = icon;
            _itemNavigationCommandCommand = itemNavigationCommand as MvxAsyncCommand;
        }
    }
}
