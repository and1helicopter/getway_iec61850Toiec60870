using System;
using System.Collections.Generic;
using System.Linq;
using Logger;
using Newtonsoft.Json.Linq;

namespace Gateway.Destination
{
    public static class DestinationAPI
    {
        private static readonly List<Abstraction.Destination> Destinations = new List<Abstraction.Destination>();

        public static Abstraction.Destination Add(JObject destination)
        {
            Abstraction.Destination destTemp = null;
            var temp = Parse.Destination(destination);
            var type = temp.Property("type").Value.ToString().ToLower();

            switch (type)
            {
                case "iec60870":
                {
                    try
                    {
                        destTemp = new IEC_60870.IEC60870_Server(temp);
                    }
                    catch 
                    {
                        Log.Write(new Exception("Gateway.Destination.Add()"), Log.Code.WARNING);
                        return null;
                    }
                    break;
                }
            }

            //Проверка есть ли уже такой объект
            var dest = Destinations.Find(x => destTemp != null && x.ShortInfo().Equals(destTemp.ShortInfo()));
            if(dest != null)
                return dest;
            Destinations.Add(destTemp);
            return destTemp;
        }

        public static bool InitHandlers()
        {
            try
            {
                foreach (var destination in Destinations)
                {
                    destination.InitHandlers();
                }
                return true;
            }
            catch
            {
                Log.Write(new Exception("Gateway.Destination.InitHandlers()"), Log.Code.ERROR);
                return true;
            }

        }

        public static bool Start()
        {
            foreach (var destTemp in Destinations)
            {
                destTemp.Start();
            }
            return !Destinations.FindAll(x => !x.IsRun).Any();
        }

        public static bool Stop()
        {
            foreach (var item in Destinations)
            {
                item.Stop();
            }
            return !Destinations.FindAll(x => x.IsRun).Any();
        }

        public static bool Clear()
        {
            Destinations.Clear();
            return !Destinations.Any();
        }
    }

    //public abstract class DestinationPath
    //{
    //    public abstract DestinationPath GetDestinationPath();
    //    public abstract void SetDestinationPath(JObject destination);
    //}

    public static class Parse
    {
        public static IEnumerable<JObject> Destinations(JObject file)
        {
            JObject tempDestination = file;
            //Проверка на существование
            if (tempDestination.ContainsKey("Destination"))
            {
                var destinations = tempDestination["Destination"];
                foreach (var itemDestination in destinations)
                {
                    yield return (JObject) itemDestination;
                }
            }
        }

        public static JObject Destination(JObject file)
        {
            JObject destination = null;
            //Проверка на существование
            if (file.ContainsKey("Source"))
            {
                destination = (JObject) file.DeepClone();
                destination.Property("Source").Remove();
            }

            return destination;
        }

        public static IEnumerable<JObject> InfoDestination(JObject file)
        {
            return new JObject[1];
        }
    }


    //    public static IEnumerable<JObject> InfoDestination(JObject file)
    //    {
    //        JObject tempDestination = file;
    //        //Проверка на существование
    //        if (tempDestination.ContainsKey("itemsDestination"))
    //        {
    //            var destinations = tempDestination["itemsDestination"];
    //            foreach (var itemDestination in destinations)
    //            {
    //                yield return (JObject)itemDestination;
    //            }
    //        }
    //    }
    //}
}
