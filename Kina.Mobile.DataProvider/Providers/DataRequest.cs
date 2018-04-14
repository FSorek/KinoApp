using Kina.Mobile.DataProvider.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using Kina.Mobile.DataProvider.Helpers;

namespace Kina.Mobile.DataProvider.Providers
{
    public class DataRequest
    {
        public List<Cinema> CinemaList { get; set; }
        public List<UserScore> ShowScore { get; set; }
        public List<string> CityList { get; set; }
        public List<string> CategoryList { get; set; }
        public Movie SelectedMovie { get; set; }

        private static string GetShowsUri(string city) => String.Format("https://epertuar.azurewebsites.net/api/Show/{0}", city);
        private static string GetCityUri() => "https://epertuar.azurewebsites.net/api/Cinema/Cities";
        private static string GetCategoryUri() => "https://epertuar.azurewebsites.net/api/Movie/Genres";
        private static string GetMovieUri(long id) => String.Format("https://epertuar.azurewebsites.net/api/Movie/{0}", id);
        private static HttpClient responseClient = new HttpClient();
        private static HttpResponseMessage httpResponseMsg = new HttpResponseMessage();
        HttpClientHandler hch = new HttpClientHandler();
        
        private static string GetScoreUri(long movieId, long cinemaId)
        {
            return String.Format("https://epertuar.azurewebsites.net/api/Rating?IdC={0}&IdMovie={1}", cinemaId, movieId);
        }

        private static string GetCinemasInRangeUri(double latitude, double longtitude, int distance)
        {
            NumberFormatInfo format = new NumberFormatInfo
            {
                NumberDecimalSeparator = "."
            };
            return String.Format("https://epertuar.azurewebsites.net/api/Show/Distance?Lng={0}&Lat={1}&range={2}",
                longtitude.ToString(format), latitude.ToString(format), distance.ToString(format));
        }

        private async Task<string> GetResponse(string uri)
        {
            Diagnostics.CreateReportMessage("GetResponse - Started for " + uri);
            string responseStream = null;
            try
            {
                httpResponseMsg = await responseClient.GetAsync(uri);
                httpResponseMsg.EnsureSuccessStatusCode();
                responseStream = await httpResponseMsg.Content.ReadAsStringAsync();
            }
            catch
            {
                throw new Exception();
            }
            Diagnostics.CreateReportMessage("GetResponse - Finished");
            return responseStream;
        }

        public async Task ProvideShowsFromCity(string city)
        {
            Diagnostics.CreateReportMessage("Provide Shows From City - Started");
            hch.Proxy = null;
            hch.UseProxy = false;
            CinemaList = new List<Cinema>();
            string uri = GetShowsUri(city);
            Diagnostics.CreateReportMessage("Provide Shows From City - Try/Catch Started");
            try
            {
                string dataString = await GetResponse(uri);
                List<Cinema> cinemas = Cinema.FromJson(dataString);
                CinemaList.AddRange(cinemas);
            }
            catch(Exception e)
            {
                Diagnostics.CreateReportMessage(e.Message);
            }
            Diagnostics.CreateReportMessage("Provide Shows From City - Finished");
        }

        public async Task ProvideCinemasInRange(double latitude, double longtitude, int distance)
        {
            CinemaList = new List<Cinema>();
            string uri = GetCinemasInRangeUri(latitude, longtitude, distance);
            try
            {
                string dataString = await GetResponse(uri);
                List<Cinema> cinemas = Cinema.FromJson(dataString);
                CinemaList.AddRange(cinemas);
            }
            catch
            {

            }
        }

        public async Task ProvideCities()
        {
            string uri = GetCityUri();
            try
            {
                string dataString = await GetResponse(uri);
                CityList = StringList.FromJson(dataString);
            }
            catch
            {
                CityList = new List<string>();
            }
        }

        public async Task ProvideCategories()
        {
            string uri = GetCategoryUri();
            try
            {
                string dataString = await GetResponse(uri);
                CategoryList = StringList.FromJson(dataString);
            }
            catch
            {
                CategoryList = new List<string>();
            }
        }

        public async Task ProvideMovieData(long id)
        {
            string uri = GetMovieUri(id);
            try
            {
                string dataString = await GetResponse(uri);
                SelectedMovie = Movie.FromJson(dataString);
            }
            catch
            {
                SelectedMovie = new Movie();
            }
        }

        public async Task ProvideScoreData(long movieId, long cinemaId)
        {
            string uri = GetScoreUri(movieId, cinemaId);
            try
            {
                string dataString = await GetResponse(uri);
                ShowScore = UserScore.FromJson(dataString);
            }
            catch
            {
                ShowScore = new List<UserScore>();
            }
        }

        public async Task PostScoreAsync(UserScore userScore)
        {
            var client = new HttpClient();
            //client.BaseAddress = new Uri("https://epertuar.azurewebsites.net");
            var json = Serialize.ToJson(userScore);
            //var json = Newtonsoft.Json.JsonConvert.SerializeObject(userScore);
            await client.PostAsync("https://epertuar.azurewebsites.net/api/Rating", new StringContent(json));
        }
    }
}
