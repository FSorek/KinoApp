// ---------------------------------------------------------------
// <author>Paul Datsyuk</author>
// <url>https://www.linkedin.com/in/pauldatsyuk/</url>
// ---------------------------------------------------------------

using MvvmCross.Core.ViewModels;
using MvvmCross.Forms.Core;
using MvvmCross.Forms.iOS;
using MvvmCross.Platform;
using MvvmCross.Platform.Platform;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using UIKit;

namespace Kina.Mobile.iOS
{
	public class Setup : MvxFormsIosSetup
	{
		public Setup(MvxFormsApplicationDelegate applicationDelegate, UIWindow window)
			: base(applicationDelegate, window)
		{
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
