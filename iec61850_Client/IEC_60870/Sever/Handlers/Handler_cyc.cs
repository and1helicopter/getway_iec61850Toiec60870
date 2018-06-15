using System;
using Logger;

namespace IEC_60870.Sever.Handlers
{
    public static class Handler_cyc
    {



    }

    public static partial class Init_Handler
    {
        public static bool Validation_cyc()
        {
            try
            {
                

                return true;
            }
            catch
            {
                Log.Write(new Exception("IEC_60870.Sever.Handlers.Validation_cyc()"), Log.Code.ERROR);
                return false;
            }
        }


    }
}
