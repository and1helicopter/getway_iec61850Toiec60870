using Newtonsoft.Json.Linq;

namespace Gateway
{
    public static class GateWayAPI
    {
        public static void Initialize(JObject data, Destinations destinations, Sources sources)
        {
            GateWay.InitializeGateWay(data, destinations, sources);
        }

        public static void DeInitialize()
        {
            GateWay.DeInitializeGateWay();   
        }

        public static void Start()
        {
            GateWay.StartGateWay();
        }

        public static void Stop()
        {
            GateWay.StopGateWay();
        }
    }

    public enum Destinations
    {
        IEC60870
    }

    public enum Sources
    {
        IEC61850,
        MODBUS
    }
}