using Kina.Mobile.Core.Behaviors;
using Kina.Mobile.Core.ViewModels;
using MvvmCross.Forms.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Kina.Mobile.Core.CustomViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RatingBox : MvxContentView
    {
        public readonly BindableProperty RatingProperty =
            BindableProperty.Create("Rating", typeof(bool),
                typeof(RatingBox), false);

        public readonly BindableProperty GroupProperty =
            BindableProperty.Create("Group", typeof(string),
                typeof(RatingBox), null);

        public readonly BindableProperty BoxNumberProperty =
            BindableProperty.Create("BoxNumber", typeof(string),
                typeof(RatingBox), null);

        public bool Rating
        {
            get { return (bool)GetValue(RatingProperty); }
            set { SetValue(RatingProperty, value); }
        }

        public string Group
        {
            get { return (string)GetValue(GroupProperty); }
            set { SetValue(GroupProperty, value); }
        }

        public string BoxNumber
        {
            get { return (string)GetValue(BoxNumberProperty); }
            set { SetValue(BoxNumberProperty, value); }
        }

        public RatingBox()
        {
            InitializeComponent();
        }
    }
}