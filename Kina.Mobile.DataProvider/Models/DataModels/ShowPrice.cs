using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class ShowPrice
    {
        public int Id_ShowPrice { get; set; }
        public Show Id_Show { get; set; }
        public Price Id_Price { get; set; }
    }
}
