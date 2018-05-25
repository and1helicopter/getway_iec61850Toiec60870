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
    }

    public abstract class ItemDestination
    {
        public abstract dynamic Path { get; set; }
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
