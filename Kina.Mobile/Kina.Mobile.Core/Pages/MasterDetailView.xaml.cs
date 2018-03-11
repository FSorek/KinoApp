using Kina.Mobile.Core.ViewModels;
using MvvmCross.Forms.Views;
using MvvmCross.Forms.Views.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Kina.Mobile.Core.Pages
{
    [MvxMasterDetailPagePresentation(MasterDetailPosition.Root, WrapInNavigationPage = false)]
    public partial class MasterDetailView : MvxMasterDetailPage<MasterDetailViewModel>
    {
        public MasterDetailView()
        {
            InitializeComponent();

            if (Device.RuntimePlatform == Device.UWP)
            {
                MasterBehavior = MasterBehavior.Popover;
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
    }
}