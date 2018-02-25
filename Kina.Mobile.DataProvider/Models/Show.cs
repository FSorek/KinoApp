using System;
using J = Newtonsoft.Json.JsonPropertyAttribute;

namespace Kina.Mobile.DataProvider.Models
{
    public class Show
    {
        [J("id_Show")] public long IdShow { get; set; }
        [J("id_Cinema")] public long IdCinema { get; set; }
        [J("id_Movie")] public long IdMovie { get; set; }
        [J("showDate")] public DateTime ShowDate { get; set; }
        [J("start")] public string Start { get; set; }
        [J("room")] public long Room { get; set; }
        [J("is3D")] public bool Is3D { get; set; }
        [J("language")] public string Language { get; set; }
    }
}
