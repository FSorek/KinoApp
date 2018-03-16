using Newtonsoft.Json;
using System.Collections.Generic;
using J = Newtonsoft.Json.JsonPropertyAttribute;

namespace Kina.Mobile.DataProvider.Models
{
    public class Cinema
    {
        [J("idCinema")] public long IdCinema { get; set; }
        //[J("id_Self")] public long IdSelf { get; set; }
        [J("cinemaName")] public string Name { get; set; }
        //[J("phone")] public string Phone { get; set; }
        //[J("longtitude")] public long Longtitude { get; set; }
        //[J("latitude")] public long Latitude { get; set; }
        [J("cinemaCity")] public string City { get; set; }
        [J("cinemaType")] public short CinemaType { get; set; }
        [J("movies")] public List<SimpleMovie> MoviesPlayed { get; set; }

        public static List<Cinema> FromJson(string json) => JsonConvert.DeserializeObject<List<Cinema>>(json, Converter.Settings);
    }
}