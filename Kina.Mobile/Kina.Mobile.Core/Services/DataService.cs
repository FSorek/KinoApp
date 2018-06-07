using Kina.Mobile.DataProvider.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Kina.Mobile.Core.Helpers;

namespace Kina.Mobile.Core.Services
{
    public class DataService : IDataService
    {
        private IHttpService _httpService;
        private IDataConverter _converter;

        public DataService(IHttpService httpService, IDataConverter converter)
        {
            _httpService = httpService;
            _converter = converter;
        }

        public async Task<List<string>> GetCategories()
        {
            List<string> categories = null;
            string uri = ResourceIdentifier.CategoryUri();
            try
            {
                string dataString = await _httpService.Get(uri);
                categories = (List<string>)_converter.FromJson(dataString, typeof(List<string>));
            }
            catch
            {
                return new List<string>();
            }

            return categories;
        }

        public async Task<List<Cinema>> GetCinemasInCity(string city)
        {
            List<Cinema> cinemas = null;
            string uri = ResourceIdentifier.CinemasInCityUri(city);
            try
            {
                string dataString = await _httpService.Get(uri);
                cinemas = (List<Cinema>)_converter.FromJson(dataString, typeof(List<Cinema>));
            }
            catch
            {
                return new List<Cinema>();
            }

            return cinemas;
        }

        public async Task<List<Cinema>> GetCinemasInRange(double latitude, double longtitude, int distance)
        {
            List<Cinema> cinemas = null;
            string uri = ResourceIdentifier.CinemasInRangeUri(latitude, longtitude, distance);
            try
            {
                string dataString = await _httpService.Get(uri);
                cinemas = (List<Cinema>)_converter.FromJson(dataString, typeof(List<Cinema>));
            }
            catch
            {
                return new List<Cinema>();
            }

            return cinemas;
        }

        public async Task<List<string>> GetCities()
        {
            List<string> cities = null;
            string uri = ResourceIdentifier.CityUri();
            try
            {
                string dataString = await _httpService.Get(uri);
                cities = (List<string>)_converter.FromJson(dataString, typeof(List<string>));
            }
            catch
            {
                return new List<string>();
            }

            return cities;
        }

        public async Task<Movie> GetMovie(long id)
        {
            Movie movie = null;
            string uri = ResourceIdentifier.MovieUri(id);
            try
            {
                string dataString = await _httpService.Get(uri);
                movie = (Movie)_converter.FromJson(dataString, typeof(Movie));
            }
            catch
            {
                return new Movie();
            }

            return movie;
        }

        public async Task<List<UserScore>> GetRating(long movieId, long cinemaId)
        {
            List<UserScore> rating;
            string uri = ResourceIdentifier.ScoreUri(movieId, cinemaId);
            try
            {
                string dataString = await _httpService.Get(uri);
                rating = (List<UserScore>)_converter.FromJson(dataString, typeof(List<UserScore>));
            }
            catch
            {
                return new List<UserScore>();
            }

            return rating;
        }

        public async Task<bool> PostScore(UserScore userScore)
        {
            var stringContent = new StringContent(_converter.SerializeScore(userScore), System.Text.Encoding.UTF8, "application/json");
            var isSuccessStatusCode = await _httpService.Post(ResourceIdentifier.RatingUri(), stringContent);
            return isSuccessStatusCode;
        }
    }
}
