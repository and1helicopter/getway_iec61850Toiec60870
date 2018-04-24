using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Gateway.DataMap.Destination
{
    public abstract class Destination
    {
        public abstract JObject GetDestination();
        public abstract void SetDestination(JObject destination);
    }

    public static class ParseDestination
    {
        public static IEnumerable<JObject> Destination(JObject file)
        {
            JObject tempDestination = file;
            //Проверка на существование
            if (!tempDestination.ContainsKey("Destination"))
            {
                
            }

            var destinations = tempDestination["Destination"];
            foreach (var itemDestination in destinations)
            {
                yield return (JObject)itemDestination;
            }
         }

        public static JObject DestinationShort(JObject file)
        {
            //Проверка на существование
            if (!file.ContainsKey("Source"))
            {

            }

            JObject destination = (JObject)file.DeepClone();
            destination.Property("Source").Remove();
            return destination;
        }
    }
}
