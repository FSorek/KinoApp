// ---------------------------------------------------------------
// <author>Paul Datsyuk</author>
// <url>https://www.linkedin.com/in/pauldatsyuk/</url>
// ---------------------------------------------------------------

namespace Kina.Mobile.Core.Services
{
	public interface IAppSettings
	{
		int SuperNumber { get; set; }
        int ActiveUserID { get; set; }
	}
}