using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class Show
    {
        public int Id_Show { get; set; }
        public Cinema Id_Cinema { get; set; }
        public Movie Id_Movie { get; set; }
        public DateTime ShowDate { get; set; }
        public string Start { get; set; }
        public int Room { get; set; }
        public bool is3D { get; set; }
        public String Language { get; set; }

        //public string Time { get { return Start; } }
    }
}
