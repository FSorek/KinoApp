using Kina.Mobile.DataProvider.Models;

namespace Kina.Mobile.Core.Model
{
    class MovieDataSet
    {
        public Movie MovieData { get; set; }
        public string CinemaName { get; set; }

        public MovieDataSet(Movie movieData, string cinemaName)
        {
            MovieData = movieData;
            CinemaName = cinemaName;
        }
    }
}
