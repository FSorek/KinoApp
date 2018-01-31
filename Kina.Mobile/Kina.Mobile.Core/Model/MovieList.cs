using Kina.Mobile.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Kina.Mobile.Core.Model
{
    class MovieList
    {
        public string CinemaName { get; set; }
        public List<ShowsMovieModel> Movies { get; set; }
        public Color CinemaColor { get; set; }

        public MovieList(string cinemaName, List<ShowsMovieModel> movies, Color cinemaColor)
        {
            CinemaName = cinemaName;
            Movies = movies;
            CinemaColor = cinemaColor;
        }
    }
}
