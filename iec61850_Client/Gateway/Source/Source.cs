using System;
using System.Collections.Generic;
using Logger;
using Newtonsoft.Json.Linq;

namespace Gateway.Source
{
    public static class SourceAPI
    {
        private static readonly List<Abstraction.Source> Sources = new List<Abstraction.Source>();

        public static Abstraction.Source Add(JObject source)
        {
            Abstraction.Source sourceTemp = null;
            try
            {
                var temp = Parse.Source(source);
                var type = temp.Property("type").Value.ToString().ToLower();

                switch (type)
                {
                    case "iec61850":
                    {
                        try
                        {
                            sourceTemp = new IEC_61850.IEC61850_Client(temp);
                        }
                        catch (Exception e)
                        {
                            Log.Write(e, Log.Code.WARNING);
                            return null;
                        }
                        break;
                    }
                }

                //Проверка есть ли уже такой объект
                var sour = Sources.Find(x => sourceTemp != null && x.ShortInfo().Equals(sourceTemp.ShortInfo()));
                if (sour != null)
                    return sour;
                Sources.Add(sourceTemp);

                return sourceTemp;
            }
            catch (Exception e)
            {
                Log.Write(e, Log.Code.ERROR);
                return sourceTemp;
            }
        }

        public static bool Start()
        {
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