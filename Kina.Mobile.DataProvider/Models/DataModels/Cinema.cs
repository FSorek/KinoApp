using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace DataModel
{
    [Table("Cinema")]
    public class Cinema
    {
        [PrimaryKey, AutoIncrement, Column("Id_Cinema")]
        public int Id_Cinema { get; set; }
        [Column("Id_Self")]
        public int Id_Self { get; set; }
        //public Address Id_Address { get; set; }
        public String Name { get; set; }
        public String Phone { get; set; }
        [Column("Longtitude")]
        public double Longtitude { get; set; }
        [Column("Latitude")]
        public double Latitude { get; set; }
        [Column("City")]
        public string City { get; set; }
        [Ignore]
        public List<Movie> MoviesPlayed { get; set; }
    }
}
