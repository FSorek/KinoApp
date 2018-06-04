using Kina.Mobile.Core.CustomViews;
using Kina.Mobile.Core.ViewModels;
using MvvmCross.Forms.Views;
using MvvmCross.Forms.Views.Attributes;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Kina.Mobile.Core.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxMasterDetailPagePresentation()]
    public partial class ShowsView : MvxContentPage<ShowsViewModel>
	{
		public ShowsView ()
		{
            InitializeComponent ();
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (BindingContext.DataContext is ShowsViewModel context)
            {
                context.FillWithData();
            }
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            bool isTileVisible = false;
            var oldTemplate = RepertoireList.ItemTemplate;
            if (oldTemplate != null)
            {
                if (oldTemplate.Values.Keys.Contains(ShowCell.IsTileVisibleProperty))
                {
                    isTileVisible = (bool)oldTemplate.Values[ShowCell.IsTileVisibleProperty];
                }
            }

            if (width <= 352)
            {
                if (isTileVisible || oldTemplate == null)
                {
                    CreateTemplate(false);
                }
            }
            else
            {
                if (!isTileVisible)
                {
                    CreateTemplate(true);
                }
            }
        }

        private void CreateTemplate(bool isTileVisible)
        {
            var template = new DataTemplate(typeof(ShowCell));
            template.SetBinding(ShowCell.CommandProperty, "GoToMoviePageCommand");
            template.SetBinding(ShowCell.TitleProperty, "Title");
            template.SetBinding(ShowCell.RatingProperty, "Rating");
            template.SetBinding(ShowCell.GenreProperty, "Genre");
            template.SetBinding(ShowCell.ShowsProperty, "Shows");
            template.SetBinding(ShowCell.ImageSourceProperty, "ImageSource");
            template.SetValue(ShowCell.IsTileVisibleProperty, isTileVisible);
            RepertoireList.ItemTemplate = template;
        }
    }
}
