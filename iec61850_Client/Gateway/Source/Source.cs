using System;
using System.Collections.Generic;
using Logger;
using Newtonsoft.Json.Linq;

namespace Gateway.Source
{
    public static class SourceAPI
    {
        private static readonly List<Abstraction.Source> Sources = new List<Abstraction.Source>();

        public static bool Add(JObject source)
        {
            var temp = Parse.Source(source);

            var type = temp.Property("type").Value.ToString().ToLower();
            switch (type)
            {
                case "iec60870":
                {
                    try
                    {
                        Abstraction.Source sourceTemp = new IEC_61850.IEC61850_Client();
                        //Проверка есть ли уже такой объект
                        if (Sources.Find(x => x.ShortInfo() == sourceTemp.ShortInfo()) != null)
                                return false;
                            //Abstraction.Source destTemp = new IEC_60870.IEC60870_Server(temp);
                            ////Проверка есть ли уже такой объект

                            //Destinations.Add(destTemp);
                        }
                        catch (Exception e)
                    {
                        Log.Write(e, Log.Code.WARNING);
                        return false;
                    }
                    break;
                }
            }
            return true;
        }
    }

    public abstract class Source
    {
        public abstract Source GetSource();
        public abstract void SetSource(JObject source);
    }

    public abstract class SourcePath
    {
        public abstract SourcePath GetSourcePath();
        public abstract void SetSourcePath(JObject source);
    }

    public static class Parse
    {
        public static IEnumerable<JObject> Sources(JObject file)
        {
            JObject tempSource = file;
            //Проверка на существование
            if (tempSource.ContainsKey("Source"))
            {
                var source = tempSource["Source"];
                foreach (var itemSource in source)
                {
                    yield return (JObject)itemSource;
                }
            }
        }

        public static JObject Source(JObject file)
        {
            JObject source = null;
            //Проверка на существование
            if (file.ContainsKey("itemsDestination"))
            {
                source = (JObject)file.DeepClone();
                source.Property("itemsDestination").Remove();
                source.Property("itemsSource").Remove();
            }
            return source;
        }

        public static IEnumerable<JObject> InfoSource(JObject file)
        {
            JObject infoSource = file;
            //Проверка на существование
            if (infoSource.ContainsKey("objects"))
            {
                var source = infoSource["objects"];
                foreach (var itemSource in source)
                {
                    yield return (JObject)itemSource;
                }
            }
        }
    }
}