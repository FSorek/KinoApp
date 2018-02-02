using System.Collections.Generic;
using Newtonsoft.Json;

namespace Kina.Mobile.DataProvider.Models
{
    public class StringList
    {
        public static List<string> FromJson(string json) => JsonConvert.DeserializeObject<List<string>>(json, Converter.Settings);
    }
}
