using System.IO;
using System.Linq;
using System.Reflection;
using System;
using CoreMultikinoJson;

namespace Kina.Mobile.Core.Model
{
    class JsonReader
    {
        public Multikino DeserializeMultikino()
        {
            var assembly = GetType().GetTypeInfo().Assembly;
            var resources = assembly.GetManifestResourceNames();
            var resourceName = resources.Single(r => r.EndsWith("multikino.json", StringComparison.OrdinalIgnoreCase));
            var stream = assembly.GetManifestResourceStream(resourceName);

            string json;

            //try
            //{
            using (var reader = new StreamReader(stream))
            {
                json = reader.ReadToEnd();
                Multikino mutikinoRoot = Multikino.FromJson(json);
                return mutikinoRoot;
            }
            //}
            //catch(Exception e)
            //{
            //    throw e;
            //}

            //mutikinoRoot = Multikino.FromJson(json);
            //return mutikinoRoot;
        }
    }
}
