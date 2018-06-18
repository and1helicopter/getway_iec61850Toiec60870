using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Abstraction
{
    public class Datum: IDatum
    {
        public Source Source { get; }
        public Destination Destination { get; }
        public Item Item { get; }
        public dynamic Value { get; set; }

        public Datum(Source source, Destination destination, JObject itemData)
        {
            Source = source;
            Destination = destination;

            Item = destination.InitItem(itemData, source);
        }
    }

    public abstract class Item
    {
        public abstract Dictionary<ItemSource, ItemDestination> Dictionary { get; set; }
    }

    interface IDatum
    {
        
    }

}
