using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Gateway.DataMap.Source
{
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

    public static class ParseSource
    {
        public static IEnumerable<JObject> Source(JObject file)
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

        public static JObject SourceShort(JObject file)
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