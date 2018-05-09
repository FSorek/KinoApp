using Kina.Mobile.DataProvider.Models;
using System;
using System.Collections.Generic;

namespace Kina.Mobile.Core.Services
{
    public interface IFilterService
    {
        bool IsActive { get; set; }
        string Category { get; set; }
        List<Cinema> Cinemas { get; set; }
        string City { get; set; }
        TimeSpan End { get; set; }
        TimeSpan Start { get; set; }
        string Title { get; set; }

        bool Check(SimpleMovie movie);
        void ClearFilter();
    }
}
