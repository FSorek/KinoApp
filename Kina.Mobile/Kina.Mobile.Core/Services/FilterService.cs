using Kina.Mobile.DataProvider.Models;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Kina.Mobile.Core.Services
{
    public class FilterService : IFilterService
    {

        private CultureInfo _cultureInfo = new CultureInfo("en-US");
        public bool IsActive { get; set; }
        public string Category { get; set; }
        public List<Cinema> Cinemas { get; set; }
        public string City { get; set; }
        public TimeSpan End { get; set; }
        public TimeSpan Start { get; set; }
        public string Title { get; set; }

        public bool Check(SimpleMovie movie)
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
                    var date = DateTime.Parse(s.ShowDate.Date.ToString(), _cultureInfo);
                    if (date.Equals(DateTime.Today))
                    {
                        if (Start != default(TimeSpan) && End != default(TimeSpan))
                        {
                            TimeSpan showHour = TimeSpan.Parse(s.Start);
                            if ((showHour > Start) && (showHour < End))
                            {
                                showsAfter++;
                            }
                        }
                        else
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
            Start = default(TimeSpan);
            End = default(TimeSpan);
            Title = null;
            IsActive = false;
        }
    }
}
