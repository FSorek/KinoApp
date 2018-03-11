using Kina.Mobile.Core.ViewModels;
using MvvmCross.Forms.Views;
using MvvmCross.Forms.Views.Attributes;
using System;
using Xamarin.Forms;

namespace Kina.Mobile.Core.Pages
{
    [MvxMasterDetailPagePresentation(MasterDetailPosition.Master)]
    public partial class NavigationView : MvxContentPage<NavigationViewModel>
    {
        public NavigationView()
        {
            InitializeComponent();
            NavigationMenu.ItemTapped += (sender, e) => { ToggleClicked(); };
        }

        public void ToggleClicked(object sender, EventArgs e)
        {
            ToggleClicked();
        }

        private void ToggleClicked()
        {
            if (Parent is MasterDetailPage md)
            {
                md.MasterBehavior = MasterBehavior.Popover;
                md.IsPresented = !md.IsPresented;
            }
        }
    }
}