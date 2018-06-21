using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Abstraction
{

    public abstract class Handler : IHandler
    {
        public abstract Thread HandlerThread { get; set; }
        public abstract bool IsRun { get; set; }
        public abstract bool IsUse { get; set; }
        
        public abstract void Process();

        public abstract bool Start();
        public abstract bool Stop();
    }

    public interface IHandler
    {
        Thread HandlerThread {get; set; }
        bool IsRun { get; set; }
        bool IsUse { get; set; }

        void Process();
        bool Start();
        bool Stop();
    }

    public interface IHandlerDestination
    {
        Dictionary<Source, Item> ListDictionary { get; set; }
        bool InitHandler(Dictionary<Source, Item> dictinory, Destination destination);
    }

    public interface IHandlerSource
    {
        bool InitHandler(Dictionary<Source, Item> dictinory);
    }
}
