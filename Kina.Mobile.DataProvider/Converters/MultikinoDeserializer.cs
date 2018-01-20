using DataModel;
using Kina.Mobile.DataProvider.Models.AccessModels.Multikino;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kina.Mobile.DataProvider.Converters
{
    class MultikinoDeserializer
    {
        private Multikino root;
        private string json;

        public List<Movie> deserialize(Stream dataStream)
        {
            using (var reader = new StreamReader(dataStream))
            {
                json = reader.ReadToEnd();
                root = Multikino.FromJson(json);
            }
        }
    }
}
