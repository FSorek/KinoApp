// ---------------------------------------------------------------
// <author>Paul Datsyuk</author>
// <url>https://www.linkedin.com/in/pauldatsyuk/</url>
// ---------------------------------------------------------------

using MvvmCross.Core.ViewModels;
using MvvmCross.Forms.Core;
using MvvmCross.Forms.Uwp;
using MvvmCross.Platform;
using MvvmCross.Platform.Platform;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using Windows.ApplicationModel.Activation;
using XamlControls = Windows.UI.Xaml.Controls;

namespace Kina.Mobile.UWP
{
	public class Setup : MvxFormsWindowsSetup
	{
		private readonly LaunchActivatedEventArgs _launchActivatedEventArgs;

		public Setup(XamlControls.Frame rootFrame, LaunchActivatedEventArgs e) : base(rootFrame, e)
		{
			_launchActivatedEventArgs = e;
		}

		protected override void InitializeFirstChance()
		{
			base.InitializeFirstChance();

			Mvx.RegisterSingleton<Core.Services.ILocalizeService>(new Services.LocalizeService());
			Mvx.RegisterSingleton<ISettings>(CrossSettings.Current);
		}

		protected override MvxFormsApplication CreateFormsApplication()
		{
			return new Core.FormsApp();
		}

		protected override IMvxApplication CreateApp()
		{
			return new Core.MvxApp();
		}

		protected override IMvxTrace CreateDebugTrace()
		{
			return new Core.DebugTrace();
		}
	}
}