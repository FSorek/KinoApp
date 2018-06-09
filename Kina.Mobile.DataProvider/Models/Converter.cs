using Newtonsoft.Json;
using System.Globalization;

namespace Kina.Mobile.DataProvider.Models
{
    public class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Culture = new CultureInfo("en-US")
        };
    }
}