using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Gateway.DataMap.Destination
{
    public abstract class Destination
    {
        public abstract Destination GetDestination();
        public abstract void SetDestination(JObject destination);
    }

    public abstract class DestinationPath
    {
        public abstract DestinationPath GetDestinationPath();
        public abstract void SetDestinationPath(JObject destination);
    }

    public static class ParseDestination
    {
        public static IEnumerable<JObject> Destination(JObject file)
        {
            JObject tempDestination = file;
            //Проверка на существование
            if (tempDestination.ContainsKey("Destination"))
            {
                var destinations = tempDestination["Destination"];
                foreach (var itemDestination in destinations)
                {
                    yield return (JObject)itemDestination;
                }
            }
         }

        public static JObject DestinationShort(JObject file)
        {
            JObject destination = null;
            //Проверка на существование
            if (file.ContainsKey("Source"))
            {
                destination = (JObject)file.DeepClone();
                destination.Property("Source").Remove();
            }
            return destination;
        }

        public static IEnumerable<JObject> InfoDestination(JObject file)
        {
            JObject tempDestination = file;
            //Проверка на существование
            if (tempDestination.ContainsKey("itemsDestination"))
            {
                var destinations = tempDestination["itemsDestination"];
                foreach (var itemDestination in destinations)
                {
                    yield return (JObject)itemDestination;
                }
            }
        }
    }
}
