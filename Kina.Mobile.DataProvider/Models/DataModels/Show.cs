using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
	[Table("Show")]
    public class Show
    {
		[PrimaryKey, AutoIncrement, Column("Id_Show")]
        public int Id_Show { get; set; }
        public int Id_Cinema { get; set; }
        public String Id_Movie { get; set; }
        public DateTime ShowDate { get; set; }
        public string Start { get; set; }
        public int Room { get; set; }
        public bool is3D { get; set; }
        public String Language { get; set; }

        //public string Time { get { return Start; } }
    }
}
