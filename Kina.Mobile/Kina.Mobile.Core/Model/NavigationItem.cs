using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kina.Mobile.Core.Model
{
    public class NavigationItem
    {
        private MvxAsyncCommand _itemNavigationCommandCommand;
        private string _itemName;

        public IMvxAsyncCommand ItemNavigationCommand => _itemNavigationCommandCommand;
        public string ItemName
        {
            get { return _itemName; }
        }

        public NavigationItem(string itemName, IMvxAsyncCommand itemNavigationCommand)
        {
            _itemName = itemName;
            _itemNavigationCommandCommand = itemNavigationCommand as MvxAsyncCommand;
        }
    }
}
