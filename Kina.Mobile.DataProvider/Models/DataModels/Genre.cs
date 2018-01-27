using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    [Table("Genre")]
    public class Genre
    {
        [PrimaryKey, AutoIncrement, Column("id")]
        public int GenreID { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("eng_name")]
        public string EngName { get; set; }
    }
}
