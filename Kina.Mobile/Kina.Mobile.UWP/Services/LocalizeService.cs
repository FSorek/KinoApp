﻿// ---------------------------------------------------------------
// <author>Paul Datsyuk</author>
// <url>https://www.linkedin.com/in/pauldatsyuk/</url>
// ---------------------------------------------------------------

using System.Globalization;

namespace Kina.Mobile.UWP.Services
{
	public class LocalizeService : Core.Services.ILocalizeService
	{
		public CultureInfo GetCurrentCultureInfo()
		{
			return CultureInfo.CurrentUICulture;
		}
	}
}