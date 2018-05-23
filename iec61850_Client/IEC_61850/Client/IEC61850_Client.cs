using Abstraction;

namespace IEC_61850
{
    public class IEC61850_Client:Source
    {
        public override bool IsRun { get; set; }

        public IEC61850_Client()
        {

        }

        public override dynamic SetValue()
        {
            throw new System.NotImplementedException();
        }

        public override bool Start()
        {
            throw new System.NotImplementedException();
        }

        public override bool Stop()
        {
            throw new System.NotImplementedException();
        }

        public override dynamic ShortInfo()
        {
            throw new System.NotImplementedException();
        }

        public override bool InitHandlers()
        {
            throw new System.NotImplementedException();
        }

        public override dynamic GetValue()
        {
            throw new System.NotImplementedException();
        }
    }
}
