using Kina.Mobile.DataProvider.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kina.Mobile.Core.Services
{
    public interface IFilterService
    {
        bool Check(Movie movie);
        void ClearFilter();
    }
}
