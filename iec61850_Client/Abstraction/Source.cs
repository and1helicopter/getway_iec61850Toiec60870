namespace Abstraction
{
    public abstract class Source : ISource
    {
        public abstract bool IsRun { get; set; }

        public abstract dynamic GetValue();
        public abstract dynamic SetValue();
        public abstract bool Start();
        public abstract bool Stop();
        public abstract dynamic ShortInfo();
        public abstract bool InitHandlers();
    }

    public interface ISource
    {
        dynamic GetValue();
        dynamic SetValue();
        bool Start();
        bool Stop();
        dynamic ShortInfo();
        bool InitHandlers();
    }
}
