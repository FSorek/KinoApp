using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Kina.Mobile.Core.CustomViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RatingBox : MvxContentView
    {
        public static readonly BindableProperty BoxMarkedProperty =
            BindableProperty.Create("BoxMarked", typeof(bool),
                typeof(RatingBox), false);

        public readonly BindableProperty GroupProperty =
            BindableProperty.Create("Group", typeof(string),
                typeof(RatingBox), null);

        public readonly BindableProperty BoxNumberProperty =
            BindableProperty.Create("BoxNumber", typeof(string),
                typeof(RatingBox), null);

        public bool BoxMarked
        {
            get { return (bool)GetValue (BoxMarkedProperty); }
            set { SetValue (BoxMarkedProperty, value); }
        }

        public string Group
        {
            get { return (string)GetValue (GroupProperty); }
            set { SetValue (GroupProperty, value); }
        }

        public string BoxNumber
        {
            get { return (string)GetValue (BoxNumberProperty); }
            set { SetValue (BoxNumberProperty, value); }
        }

        public RatingBox()
        {
            InitializeComponent();
        }
    }
}