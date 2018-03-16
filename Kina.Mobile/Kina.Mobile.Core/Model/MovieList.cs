using System.Collections.Generic;
using Xamarin.Forms;

namespace Kina.Mobile.Core.Model
{
    public class MovieList
    {
        public string CinemaName { get; set; }
        public List<MovieListItem> Movies { get; set; }
        public Color CinemaColor { get; set; }

        public MovieList(string cinemaName, List<MovieListItem> movies, Color cinemaColor)
        {
            CinemaName = cinemaName;
            Movies = movies;
            CinemaColor = cinemaColor;
        }
    }
}
