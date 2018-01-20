using DataModel;
using Kina.Mobile.DataProvider.Models.AccessModels.CinemaCity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kina.Mobile.DataProvider.Converters
{
    class CinemaCityDeserializer
    {
        private CinemaCity root;
        private string json;

        public List<Movie> deserialize(Stream dataStream)
        {
            using (var reader = new StreamReader)
            {
                json = reader.ReadToEnd();
                root = CinemaCity.FromJson(json);
            }
        }
    }
}
