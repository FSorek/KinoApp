// ---------------------------------------------------------------
// <author>Paul Datsyuk</author>
// <url>https://www.linkedin.com/in/pauldatsyuk/</url>
// ---------------------------------------------------------------

using Plugin.Settings.Abstractions;

namespace Kina.Mobile.Core.Services
{
	public class AppSettings : IAppSettings
	{
        private const string activeUserKey = "ActiveUserID";
        private const string settingsFileName = "Settings.inf";
		public const string SuperNumberKey = "SuperNumberKey";

        private const int DefaultAciveUserID = 1;
		public const int SuperNumberDefaultValue = 1;

		private readonly ISettings _settings;

		public AppSettings(ISettings settings)
		{
			_settings = settings;
		}

		public int SuperNumber
		{
			get { return _settings.GetValueOrDefault(SuperNumberKey, 1); }
			set { _settings.AddOrUpdateValue(SuperNumberKey, value); }
		}

        public int ActiveUserID
        {
            get { return _settings.GetValueOrDefault(activeUserKey, DefaultAciveUserID, settingsFileName); }
            set { _settings.AddOrUpdateValue(activeUserKey, value, settingsFileName); }
        }
    }
}
