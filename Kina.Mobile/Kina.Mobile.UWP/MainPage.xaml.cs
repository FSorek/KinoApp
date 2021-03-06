﻿// ---------------------------------------------------------------
// <author>Paul Datsyuk</author>
// <url>https://www.linkedin.com/in/pauldatsyuk/</url>
// ---------------------------------------------------------------

using MvvmCross.Core.ViewModels;
using MvvmCross.Core.Views;
using MvvmCross.Core.Platform;
using MvvmCross.Forms.Uwp.Presenters;
using MvvmCross.Platform;
using System.Text;
using Xamarin.Forms.Platform.UWP;
using MvvmCross.Forms.Views;

namespace Kina.Mobile.UWP
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class MainPage : WindowsPage
	{
		public MainPage()
		{
			this.InitializeComponent();

			var start = Mvx.Resolve<IMvxAppStart>();
			start.Start();

			var presenter = Mvx.Resolve<IMvxFormsViewPresenter>() as MvxFormsUwpViewPresenter;
			LoadApplication(presenter.FormsApplication);

			Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
		}
	}
}
