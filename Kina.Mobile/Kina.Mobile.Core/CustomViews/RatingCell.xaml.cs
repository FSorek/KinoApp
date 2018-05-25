using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Kina.Mobile.Core.CustomViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RatingCell : ContentView
	{
        public static readonly BindableProperty CellNumberProperty =
            BindableProperty.Create("CellNumber", typeof(int), typeof(View), default(int));
        public static readonly BindableProperty IsMarkedProperty =
            BindableProperty.Create("IsMarked", typeof(bool), typeof(View), false);

        public int CellNumber
        {
            get => (int)GetValue(CellNumberProperty);
            set => SetValue(CellNumberProperty, value);
        }

        public bool IsMarked
        {
            get => (bool)GetValue(IsMarkedProperty);
            set => SetValue(IsMarkedProperty, value);
        }

        public RatingCell()
		{
			InitializeComponent();
		}
	}
}
