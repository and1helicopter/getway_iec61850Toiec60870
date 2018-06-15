using System.Collections;
using System.Collections.Generic;
using Logger;
using Newtonsoft.Json.Linq;

namespace Gateway.Datum
{
    public static class Parse
    {
        public static IEnumerable<JObject> Data(JObject file)
        {
            JObject tempDatum = file;
            //Проверка на существование
            if (tempDatum.ContainsKey("itemsDestination"))
            {
                var data = tempDatum["itemsDestination"];
                foreach (var itemDatum in data)
                {
                    yield return (JObject)itemDatum;
                }
            }
        }
    }
}