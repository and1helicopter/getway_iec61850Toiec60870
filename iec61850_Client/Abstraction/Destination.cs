using Newtonsoft.Json.Linq;

namespace Abstraction
{
    public abstract class Destination:IDestination
    {
        public abstract bool IsRun { get; set; }

        public abstract dynamic GetValue();
        public abstract dynamic SetValue();
        public abstract bool Start();
        public abstract bool Stop();
        public abstract dynamic ShortInfo();
        public abstract bool InitHandlers();
        public abstract bool AddDatum(Datum datum);

        public abstract Item InitItem(JObject itemDestination, Source source);
    }

    public abstract class ItemDestination
    {

    }

    public interface IDestination
    {
        dynamic GetValue();
        dynamic SetValue();
        bool Start();
        bool Stop();
        dynamic ShortInfo();
        bool InitHandlers();
    }
}
