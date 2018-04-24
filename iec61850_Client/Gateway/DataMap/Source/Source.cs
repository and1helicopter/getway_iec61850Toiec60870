using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Gateway.DataMap.Source
{
    public abstract class Source
    {
        public abstract void GetSource();
        public abstract void SetSource();
    }

    public static class ParseSource
    {
        public static IEnumerable<JObject> Source(JObject file)
        {
            JObject tempDestination = file;
            //Проверка на существование
            if (!tempDestination.ContainsKey("Source"))
            {

            }

            var destinations = tempDestination["Source"];
            foreach (var itemDestination in destinations)
            {
                yield return (JObject)itemDestination;
            }
        }

        //public static JObject SourceShort(JObject file)
        //{
        //    //Проверка на существование
        //    if (!file.ContainsKey("Source"))
        //    {

        //    }

        //    JObject destination = (JObject)file.DeepClone();
        //    destination.Property("Source").Remove();
        //    return destination;
        //}
    }
}