using Kina.Mobile.DataProvider.Models;
using Newtonsoft.Json;
using System;

namespace Kina.Mobile.Core.Services
{
    public class DataConverter : IDataConverter
    {
        public object FromJson(string json, Type type)
        {
            return JsonConvert.DeserializeObject(json, type, Converter.Settings);
        }

        public string SerializeScore(UserScore userScore)
        {
            return JsonConvert.SerializeObject(userScore);
        }
    }
}
