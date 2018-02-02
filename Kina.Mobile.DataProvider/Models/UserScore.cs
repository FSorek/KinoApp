using Newtonsoft.Json;
using System.Collections.Generic;
using J = Newtonsoft.Json.JsonPropertyAttribute;

namespace Kina.Mobile.DataProvider.Models
{
    public class UserScore
    {
        [J("cleanliness")] public long Cleanliness { get; set; }
        [J("id_Cinema")] public long IdCinema { get; set; }
        [J("id_Movie")] public long IdMovie { get; set; }
        [J("id_StringUser")] public string IdStringUser { get; set; }
        [J("id_User")] public long IdUser { get; set; }
        [J("popcorn")] public long Popcorn { get; set; }
        [J("screen")] public long Screen { get; set; }
        [J("seat")] public long Seat { get; set; }
        [J("sound")] public long Sound { get; set; }

        public static List<UserScore> FromJson(string json) => JsonConvert.DeserializeObject<List<UserScore>>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this UserScore self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }
}