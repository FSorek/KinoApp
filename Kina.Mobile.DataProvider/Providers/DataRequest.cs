using Kina.Mobile.DataProvider.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Kina.Mobile.DataProvider.Providers
{
    public class DataRequest
    {
        public List<Cinema> CinemaList { get; set; }
        public List<string> CityList { get; set; }

        private static string getShowsUri(string city) => String.Format("https://epertuar.azurewebsites.net/api/Show/{0}", city);
        private static string getCityUri() => "https://epertuar.azurewebsites.net/api/Cinema/Cities";

        public async Task<string> GetShowsResponse(string city)
        {
            var client = new HttpClient();
            string uri = getShowsUri(city);

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

        public async Task<string> GetCityResponse()
        {
            var client = new HttpClient();
            string uri = getCityUri();

            var httpResponse = await client.GetAsync(uri);
            httpResponse.EnsureSuccessStatusCode();
            var responseStream = await httpResponse.Content.ReadAsStringAsync();
            return responseStream;
        }

        public async Task ProvideCities()
        {
            string dataString = await GetCityResponse();
            CityList = City.FromJson(dataString);
        }
    }
}
