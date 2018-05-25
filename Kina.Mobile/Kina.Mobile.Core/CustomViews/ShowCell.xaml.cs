using MvvmCross.Core.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Kina.Mobile.Core.CustomViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ShowCell : ViewCell
	{
        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create("Command", typeof(IMvxAsyncCommand), typeof(ShowCell), null, BindingMode.TwoWay);
        public static readonly BindableProperty GenreProperty =
            BindableProperty.Create("Genre", typeof(string), typeof(ShowCell), "?", BindingMode.TwoWay);
        public static readonly BindableProperty ImageSourceProperty =
            BindableProperty.Create("ImageSource", typeof(ImageSource), typeof(ShowCell), null, BindingMode.TwoWay);
        public static readonly BindableProperty IsTileVisibleProperty =
            BindableProperty.Create("IsTileVisible", typeof(bool), typeof(ShowCell), true, BindingMode.TwoWay);
        public static readonly BindableProperty RatingProperty =
            BindableProperty.Create("Rating", typeof(double), typeof(ShowCell), default(double), BindingMode.TwoWay);
        public static readonly BindableProperty ShowsProperty =
            BindableProperty.Create("Shows", typeof(string), typeof(ShowCell), "?", BindingMode.TwoWay);
        public static readonly BindableProperty TitleProperty =
            BindableProperty.Create("Title", typeof(string), typeof(ShowCell), "?", BindingMode.TwoWay);

        public IMvxAsyncCommand Command
        {
            get => (IMvxAsyncCommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public string Genre
        {
            get => (string)GetValue(GenreProperty);
            set => SetValue(GenreProperty, value);
        }

        public ImageSource ImageSource
        {
            get => (ImageSource)GetValue(ImageSourceProperty);
            set => SetValue(ImageSourceProperty, value);
        }

        public bool IsTileVisible
        {
            get => (bool)GetValue(IsTileVisibleProperty);
            set => SetValue(IsTileVisibleProperty, value);
        }

        public double Rating
        {
            get => (double)GetValue(RatingProperty);
            set => SetValue(RatingProperty, value);
        }

        public string Shows
        {
            get => (string)GetValue(ShowsProperty);
            set => SetValue(ShowsProperty, value);
        }

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

		public ShowCell ()
		{
			InitializeComponent ();
		}

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            if (BindingContext != null)
            {
                //Tile.Source = ImageSource;
                //Tile.IsVisible = IsTileVisible;
                TitleLabel.Text = Title;
                GenreLabel.Text = Genre;
                ReadOnlyRatingControl.Value = Rating;
                ShowsLabel.Text = Shows;

                if (IsTileVisible)
                {
                    DetailsStackLayout.Padding = new Thickness(18, 0);
                }
                else
                {
                    DetailsStackLayout.Padding = new Thickness(0);
                }
            }
        }

        protected override void OnTapped()
        {
            if (Command != null)
            {
                Command.Execute();
            }
        }
    }
}
