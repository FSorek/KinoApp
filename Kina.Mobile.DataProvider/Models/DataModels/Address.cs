using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class Address
    {
        public int Id_address { get; set; }
        public String Street { get; set; }
        public String Street_number { get; set; }
        public String City { get; set; }
        public String Postcode { get; set; }
    }
}
