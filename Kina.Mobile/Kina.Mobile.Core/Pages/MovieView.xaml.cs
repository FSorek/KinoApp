using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Kina.Mobile.Core.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MovieView : MvxContentPage
	{
		public MovieView ()
		{
			InitializeComponent ();
		}

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            if (height > width)
            {
                ButtonStackLayout.Orientation = StackOrientation.Vertical;
                if (width <= 384)
                {
                    SetMinimumLayout();
                }
                else
                {
                    SetWideLayout();
                }
            }
            else
            {
                ButtonStackLayout.Orientation = StackOrientation.Horizontal;
                if (width <= 640)
                {
                    SetMinimumLayout();
                }
                else
                {
                    SetWideLayout();
                }
            }
        }

        private void SetMinimumLayout()
        {
            //Tile.IsVisible = false;
            DetailsStackLayout.Padding = new Thickness(0);
        }

        private void SetWideLayout()
        {
            DetailsStackLayout.Padding = new Thickness(18, 0);
            //Tile.IsVisible = true;
        }
    }
}
