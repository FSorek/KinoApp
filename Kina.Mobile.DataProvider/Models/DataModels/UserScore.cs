using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    [Table("UserScore")]
    public class UserScore
    {
        [PrimaryKey, AutoIncrement, Column("Id_UserScore")]
        public int Id_UserScore { get; set; }

        [Column("Id_User")]
        public int Id_User { get; set; }
        //public Cinema Id_Cinema { get; set; }
        //public Movie Id_Movie { get; set; }
        //public Score Id_Score { get; set; }
        public int Screen { get; set; }
        public int Seat { get; set; }
        public int Sound { get; set; }
        public int Popcorn { get; set; }

        [Column("Id_Cinema")]
        public int Id_Cinema { get; set; }
        [Column("Id_Movie")]
        public string Id_Movie { get; set; }
    }
}
