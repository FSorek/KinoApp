// ---------------------------------------------------------------
// <author>Paul Datsyuk</author>
// <url>https://www.linkedin.com/in/pauldatsyuk/</url>
// ---------------------------------------------------------------

using Acr.UserDialogs;
using Kina.Mobile.Core.Model;
using Kina.Mobile.Core.Services;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Platform.IoC;
using MvvmCross.Platform.Platform;
using MvvmCross.Plugins.Json;

namespace Kina.Mobile.Core
{
    public class MvxApp : MvxApplication
	{
        public static bool UsingFilter { get; set; }
	    public static Filter FilterSettings { get; set; }

		public override void Initialize()
		{
			CreatableTypes()
				.EndingWith("Service")
				.AsInterfaces()
				.RegisterAsLazySingleton();

			CreatableTypes().
				EndingWith("Repository")
				.AsTypes()
				.RegisterAsLazySingleton();

			Mvx.RegisterType<Services.IAppSettings, Services.AppSettings>();
			Mvx.RegisterType<IMvxJsonConverter, MvxJsonConverter>();
            Mvx.RegisterType<IDataConverter, DataConverter>();
			Mvx.RegisterSingleton<IUserDialogs>(() => UserDialogs.Instance);

            FilterSettings = new Filter();
            Resources.AppResources.Culture = Mvx.Resolve<Services.ILocalizeService>().GetCurrentCultureInfo();

            RegisterNavigationServiceAppStart<ViewModels.LocationViewModel>();
		}
	}
}
