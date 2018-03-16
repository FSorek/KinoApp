using MvvmCross.Core.ViewModels;
using System.Windows.Input;
using Xamarin.Forms;

namespace Kina.Mobile.Core.CustomViews
{
    class NavigationCell : ViewCell
    {
		protected override void OnTapped()
        {
			if(Command != null)
            {
                Command.Execute();
            }
        }

        public static readonly BindableProperty CommandProperty =
			BindableProperty.Create("Command", typeof(ICommand), typeof(NavigationCell), null, propertyChanged:OnCommandPropertyChanged);

        private static void OnCommandPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
        }

        public IMvxAsyncCommand Command
        {
            get { return (IMvxAsyncCommand) GetValue(CommandProperty); }
			set { SetValue(CommandProperty, value); }
        }
    }
}
