using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class UserScore
    {
        public int Id_UserScore { get; set; }
        public int Id_User { get; set; }
        public Cinema Id_Cinema { get; set; }
        public Movie Id_Movie { get; set; }
        public Score Id_Score { get; set; }
    }
}
