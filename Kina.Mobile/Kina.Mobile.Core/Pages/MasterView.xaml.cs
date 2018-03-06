using Kina.Mobile.Core.ViewModels;
using MvvmCross.Forms.Views;
using MvvmCross.Forms.Views.Attributes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Kina.Mobile.Core.Pages
{
    [MvxMasterDetailPagePresentation(MasterDetailPosition.Master)]
    public partial class MasterView : MvxContentPage<MasterViewModel>
    {
        public MasterView()
        {
            InitializeComponent();
        }

        public void ToggleClicked(object sender, EventArgs e)
        {
            if(Parent is MasterDetailPage md)
            {
                md.MasterBehavior = MasterBehavior.Popover;
                md.IsPresented = !md.IsPresented;
            }
        }
    }
}