using Newtonsoft.Json.Linq;

namespace Abstraction
{
    public abstract class Source : ISource
    {
        public abstract bool IsRun { get; set; }

        public abstract dynamic GetValue(Datum datum);
        public abstract dynamic SetValue(Datum datum);
        public abstract bool Start();
        public abstract bool Stop();
        public abstract dynamic ShortInfo();
        public abstract bool InitHandlers();
        public abstract ItemSource InitItemSource(JObject itemSource);
        public abstract bool AddDatum(Datum item);
    }

    public abstract class ItemSource
    {
        //public abstract dynamic GetItem { get; set; }
    }

    public interface ISource
    {
        dynamic GetValue(Datum datum);
        dynamic SetValue(Datum datum);            //Объект который изменяет значение и который изменяется
        bool Start();
        bool Stop();
        dynamic ShortInfo();
        bool InitHandlers();
        ItemSource InitItemSource(JObject itemSource);
    }
}
