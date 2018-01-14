// ---------------------------------------------------------------
// <author>Paul Datsyuk</author>
// <url>https://www.linkedin.com/in/pauldatsyuk/</url>
// ---------------------------------------------------------------

using Kina.Mobile.Core.Resources;
using Kina.Mobile.Core.Services;

namespace Kina.Mobile.Core.Helpers
{
	public static class TranslateExtension
	{
		public static string Translate(this ILocalizeService localizeService, string str)
		{
			var tranlation = AppResources.ResourceManager.GetString(str, localizeService.GetCurrentCultureInfo());
			return string.IsNullOrEmpty(tranlation) ? str : tranlation;
		}
	}
}
