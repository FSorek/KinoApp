using Kina.Mobile.DataProvider.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kina.Mobile.Core.Services
{
    public interface IDataService
    {
        Task<List<string>> GetCategories();
        Task<List<Cinema>> GetCinemasInCity(string city);
        Task<List<Cinema>> GetCinemasInRange(double latitude, double longtitude, int distance);
        Task<List<string>> GetCities();
        Task<Movie> GetMovie(long id);
        Task<List<UserScore>> GetRating(long movieId, long cinemaId);
        Task<bool> PostScore(UserScore userScore);
    }
}
