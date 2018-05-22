namespace Abstraction
{
    public abstract class Source : ISource
    {
        public abstract bool IsRun { get; set; }

        public abstract bool Start();
        public abstract bool Stop();
        public abstract dynamic GetValue();
        public abstract dynamic SetValue();
    }

    public interface ISource
    {
        dynamic GetValue();
        dynamic SetValue();
        bool Start();
        bool Stop();
    }
}
