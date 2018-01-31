using System.Collections.Generic;
using J = Newtonsoft.Json.JsonPropertyAttribute;

namespace Kina.Mobile.DataProvider.Models
{
    public class Movie
    {
        [J("id")] public long Id { get; set; }
        [J("id_Movie")] public object IdMovie { get; set; }
        [J("name")] public string Name { get; set; }
        [J("original_Name")] public string OriginalName { get; set; }
        [J("length")] public long Length { get; set; }
        [J("director")] public string Director { get; set; }
        [J("writers")] public string Writers { get; set; }
        [J("stars")] public string Stars { get; set; }
        [J("storyline")] public string Storyline { get; set; }
        [J("trailer")] public string Trailer { get; set; }
        [J("music")] public string Music { get; set; }
        [J("cinematography")] public string Cinematography { get; set; }
        [J("rating")] public string Rating { get; set; }
        [J("shows")] public List<Show> Shows { get; set; }
        [J("genre")] public List<string> Genre { get; set; }
    }
}
