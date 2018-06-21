using System;
using System.Threading;
using Abstraction;
using Logger;

namespace IEC_60870.Sever.Handlers
{
    public class HandlerBasic : Handler
    {
        public override Thread HandlerThread { get; set; }
        public override bool IsRun { get; set; }
        public override bool IsUse { get; set; }

        public override bool Start()
        {
            try
            {
                if (HandlerThread != null)
                {
                    IsRun = true;
                    HandlerThread.Start();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                Log.Write(new Exception("IEC_60870.Sever.Handlers." + GetType() +".Start()"), Log.Code.ERROR);
                return false;
            }
        }

        public override bool Stop()
        {
            try
            {
                if (HandlerThread.IsAlive)
                {
                    IsRun = false;
                    HandlerThread.Abort();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                Log.Write(new Exception("IEC_60870.Sever.Handlers." + GetType() + ".Stop()"), Log.Code.ERROR);
                return false;
            }
        }
    }
}
