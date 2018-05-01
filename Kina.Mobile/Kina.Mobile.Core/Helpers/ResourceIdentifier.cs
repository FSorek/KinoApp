using System;
using System.Globalization;

namespace Kina.Mobile.Core.Helpers
{
    public static class ResourceIdentifier
    {
        public static string CategoryUri() => "https://epertuar.azurewebsites.net/api/Movie/Genres";
        public static string CinemasInCityUri(string city) => String.Format("https://epertuar.azurewebsites.net/api/Show/{0}", city);
        public static string CinemasInRangeUri(double latitude, double longtitude, int distance)
        {
            NumberFormatInfo format = new NumberFormatInfo
            {
                NumberDecimalSeparator = "."
            };
            return String.Format("https://epertuar.azurewebsites.net/api/Show/Distance?Lng={0}&Lat={1}&range={2}",
                longtitude.ToString(format), latitude.ToString(format), distance.ToString(format));
        }

        public static string CityUri() => "https://epertuar.azurewebsites.net/api/Cinema/Cities";
        public static string MovieUri(long id) => String.Format("https://epertuar.azurewebsites.net/api/Movie/{0}", id);
        public static string RatingUri() => "https://epertuar.azurewebsites.net/api/Rating";
        public static string ScoreUri(long movieId, long cinemaId)
        {
            return String.Format("https://epertuar.azurewebsites.net/api/Rating?IdC={0}&IdMovie={1}", cinemaId, movieId);
        }
    }
}
