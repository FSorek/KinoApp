using System;
using J = Newtonsoft.Json.JsonPropertyAttribute;

namespace Kina.Mobile.DataProvider.Models
{
    public class SimpleShow
    {
        [J("showDate")]public DateTime ShowDate { get; set; }
        [J("start")]public string Start { get; set; }
        [J("is3D")]public bool Is3D { get; set; }
        [J("language")]public string Language { get; set; }

        //public static SimpleShow FromJson(string json) => JsonConvert.DeserializeObject<SimpleShow>(json, Converter.Settings);
    }
}
