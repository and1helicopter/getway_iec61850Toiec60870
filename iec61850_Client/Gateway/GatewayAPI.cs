using Newtonsoft.Json.Linq;

namespace Gateway
{
    public static class GateWayAPI
    {
        public static void Initialize(JObject data)
        {
            GateWay.InitializeGateWay(data);
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
}