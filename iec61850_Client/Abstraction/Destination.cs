using System.Runtime.InteropServices;

namespace Abstraction
{
    public abstract class Destination:IDestination
    {
        public abstract bool IsRun { get; set; }

        public abstract bool Start();
        public abstract bool Stop();
        public abstract dynamic GetValue();
        public abstract dynamic SetValue();
        public abstract dynamic ShortInfo();
        public abstract bool InitHandlers();
    }

    public interface IDestination
    {
        bool Start();
        bool Stop();

        dynamic GetValue();
        dynamic SetValue();

        dynamic ShortInfo();
        bool InitHandlers();
    }
}
