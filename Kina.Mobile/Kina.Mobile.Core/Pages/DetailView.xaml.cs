using Kina.Mobile.Core.ViewModels;
using MvvmCross.Forms.Views;
using MvvmCross.Forms.Views.Attributes;

namespace Kina.Mobile.Core.Pages
{
    [MvxMasterDetailPagePresentation()]
    public partial class DetailView : MvxContentPage<DetailViewModel>
    {
        public DetailView()
        {
            InitializeComponent();
        }
    }
}