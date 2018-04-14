using Newtonsoft.Json;
using System.Collections.Generic;
using J = Newtonsoft.Json.JsonPropertyAttribute;

namespace Kina.Mobile.DataProvider.Models
{
    public class SimpleMovie
    {
        [J("id")]public long Id { get; set; }
        [J("movieName")]public string Name { get; set; }
        [J("showList")]public List<SimpleShow> Shows { get; set; }
        [J("genres")]public List<string> Genre { get; set; }
        [J("averageRating")] public float AverageRating { get; set; }

        public static SimpleMovie FromJson(string json) => JsonConvert.DeserializeObject<SimpleMovie>(json, Converter.Settings);
    }
}
