// ---------------------------------------------------------------
// <author>Paul Datsyuk</author>
// <url>https://www.linkedin.com/in/pauldatsyuk/</url>
// ---------------------------------------------------------------

using Acr.UserDialogs;
using DataModel;
using Kina.Mobile.Core.Services;
using Kina.Mobile.DataProvider.Helpers;
using Kina.Mobile.DataProvider.Providers;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Platform.IoC;
using MvvmCross.Platform.Platform;
using MvvmCross.Plugins.Json;
using MvvmCross.Plugins.Location;
using Xamarin.Forms;

namespace Kina.Mobile.Core
{
	public class MvxApp : MvxApplication
	{
        static KinaMobileDatabase _database;

        public static KinaMobileDatabase Database
        {
            get
            {
                if (_database == null)
                {
                    _database = new KinaMobileDatabase(DependencyService.Get<IFileHelper>().GetLocalFilePath("KinaMobileSQLite.db3"));
                }
                return _database;
            }
        }

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
			Mvx.RegisterSingleton<IUserDialogs>(() => UserDialogs.Instance);

            Resources.AppResources.Culture = Mvx.Resolve<Services.ILocalizeService>().GetCurrentCultureInfo();

			RegisterAppStart<ViewModels.ShowsViewModel>();
		}
	}
}
