using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kina.Mobile.Core.Model
{
    public class FilterSet
    {
        public Genre Genre { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public string Title { get; set; }
        public List<Cinema> Cinemas { get; set; }

        public void ClearFilter()
        {
            Genre = null;
            Start = null;
            End = null;
            Title = null;
        }

        public void ClearLocalization()
        {
            Cinemas = null;
        }
    }
}
