using Kina.Mobile.DataProvider.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;

namespace Kina.Mobile.DataProvider.Providers
{
    public class DataRequest
    {
        public List<Cinema> CinemaList { get; set; }
        public List<string> CityList { get; set; }

        private static string GetShowsUri(string city) => String.Format("https://epertuar.azurewebsites.net/api/Show/{0}", city);
        private static string GetCityUri() => "https://epertuar.azurewebsites.net/api/Cinema/Cities";
        private static string GetCinemasInRangeUri(double latitude, double longtitude, int distance)
        {
            NumberFormatInfo format = new NumberFormatInfo();
            format.NumberDecimalSeparator = ".";
            return String.Format("https://epertuar.azurewebsites.net/api/Show/Distance?Lng={0}&Lat={1}&range={2}",
                longtitude.ToString(format), latitude.ToString(format), distance.ToString(format));
        }

        public async Task<string> GetShowsResponse(string city)
        {
            var client = new HttpClient();
            string uri = GetShowsUri(city);

            var httpResponse = await client.GetAsync(uri);
            httpResponse.EnsureSuccessStatusCode();
            var responseStream = await httpResponse.Content.ReadAsStringAsync();
            return responseStream;
        }

        public async Task<string> GetCityResponse()
        {
            var client = new HttpClient();
            string uri = GetCityUri();

            var httpResponse = await client.GetAsync(uri);
            httpResponse.EnsureSuccessStatusCode();
            var responseStream = await httpResponse.Content.ReadAsStringAsync();
            return responseStream;
        }

        public async Task<string> GetCinemasInRangeResponse(double latitude, double longtitude, int distance)
        {
            var client = new HttpClient();
            string uri = GetCinemasInRangeUri(latitude, longtitude, distance);

            var httpResponse = await client.GetAsync(uri);
            httpResponse.EnsureSuccessStatusCode();
            var responseStream = await httpResponse.Content.ReadAsStringAsync();
            return responseStream;
        }

        public async Task ProvideShowsFromCity(string city)
        {
            CinemaList = new List<Cinema>();
            string dataString = await GetShowsResponse(city);
            List<Cinema> cinemas = Cinema.FromJson(dataString);
            CinemaList.AddRange(cinemas);
        }

        public async Task ProvideCinemasInRange(double latitude, double longtitude, int distance)
        {
            CinemaList = new List<Cinema>();
            string dataString = await GetCinemasInRangeResponse(latitude, longtitude, distance);
            List<Cinema> cinemas = Cinema.FromJson(dataString);
            CinemaList.AddRange(cinemas);
        }

        public async Task ProvideCities()
        {
            string dataString = await GetCityResponse();
            CityList = City.FromJson(dataString);
        }
    }
}
