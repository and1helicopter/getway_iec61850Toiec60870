using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Abstraction
{
    public abstract class Destination:IDestination
    {
        public abstract bool IsRun { get; set; }

        public abstract void GetValueAsync(Source source, Item item);
        public abstract void SetValue(Source source, Item item, dynamic value);
        public abstract bool Start();
        public abstract bool Stop();
        public abstract dynamic ShortInfo();
        public abstract bool InitHandlers();
        public abstract bool AddDatum(Datum datum);

        public abstract Item InitItem(JObject itemsDestination, Source source);
    }

    public abstract class ItemDestination
    {

    }

    public interface IDestination
    {
        void GetValueAsync(Source source, Item item);
        void SetValue(Source source, Item item, dynamic value);
        bool Start();
        bool Stop();
        dynamic ShortInfo();
        bool InitHandlers();
    }
}
