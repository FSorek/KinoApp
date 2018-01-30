using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
