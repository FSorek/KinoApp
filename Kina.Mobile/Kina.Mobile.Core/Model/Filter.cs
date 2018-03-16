using Kina.Mobile.DataProvider.Models;
using System.Collections.Generic;

namespace Kina.Mobile.Core.Model
{
    public class Filter
    {
        public string Category { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public string Title { get; set; }
        public List<Cinema> Cinemas { get; set; }
        public string City { get; set; }

        public void ClearFilter()
        {
            Category = null;
            Start = null;
            End = null;
            Title = null;
        }

        public void ClearLocalization()
        {
            Cinemas = null;
            City = null;
        }
    }
}
