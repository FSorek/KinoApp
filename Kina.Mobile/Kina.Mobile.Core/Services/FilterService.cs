using Kina.Mobile.DataProvider.Models;
using System;
using System.Collections.Generic;

namespace Kina.Mobile.Core.Services
{
    public class FilterService : IFilterService
    {
        public string Category { get; set; }
        public List<Cinema> Cinemas { get; set; }
        public string City { get; set; }
        public string End { get; set; }
        public string Start { get; set; }
        public string Title { get; set; }

        public FilterService()
        {
            End = "00:00";
            Start = "00:00";
        }

        public bool Check(Movie movie)
        {
            if (Title != null)
            {
                if (!movie.Name.ToLower().Contains(Title.ToLower()))
                {
                    return false;
                }
            }

            if (Category != null)
            {
                if (!movie.Genre.Contains(Category))
                {
                    return false;
                }
            }

            if (movie.Shows.Count == 0)
            {
                return false;
            }
            else
            {
                int showsAfter = 0;
                foreach (var s in movie.Shows)
                {
                    if (s.ShowDate.Date.Equals(DateTime.Today.Date))
                    {
                        int showHour = int.Parse(s.Start.Split(':')[0]);
                        int startParameter = int.Parse(Start.Split(':')[0]);
                        int endParameter = int.Parse(End.Split(':')[0]);
                        if (((showHour > startParameter) && (showHour < endParameter)) || (startParameter == 0 && endParameter == 0))
                        {
                            showsAfter++;
                        }
                    }
                }

                if (showsAfter == 0)
                {
                    return false;
                }
            }

            return true;
        }

        public void ClearFilter()
        {
            Category = null;
            Start = null;
            End = null;
            Title = null;
        }
    }
}
