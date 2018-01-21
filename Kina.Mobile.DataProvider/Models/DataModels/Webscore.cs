using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class Webscore
    {
        public int Id_Webscore { get; set; }
        public MetaCritic Id_MetaCritic { get; set; }
        public Filmweb Id_Filmweb { get; set; }
        public RottenTomatoes Id_RottenTomatoes { get; set; }
    }
}
