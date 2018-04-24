using Newtonsoft.Json.Linq;

namespace Gateway.DataMap
{
    public static class GateWayAPI
    {
        public static void Initialize(JObject data, Destinations destinations, Sources sources)
        {
            GateWay.InitializeGateWay(data, destinations, sources);
        }

        public static void DeInitialize()
        {
            
        }
    }

    public enum Destinations
    {
        IEC60870
    }

    public enum Sources
    {
        IEC61870,
        MODBUS
    }
}