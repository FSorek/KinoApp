using Kina.Mobile.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kina.Mobile.Core.Model
{
    class MovieList
    {
        public string CinemaName { get; set; }
        public List<ShowsMovieModel> movieList { get; set; }
    }
}
