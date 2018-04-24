using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Gateway.DataMap.Destination
{
    public class iec60870 : Destination
    {
        private string host { get; set; }
        private int port  { get; set; }
        private int maxQueue { get; set; }
        private int  maxConnection  { get; set; }
        private bool statusTls  { get; set; }

        public override JObject GetDestination()
        {
            dynamic destination = new JObject();
            destination.host = host;
            destination.port = port;
            destination.maxQueue = maxQueue;
            destination.maxConnection = maxConnection;
            destination.statusTls = statusTls;
            return destination;
        }

        public override void SetDestination(JObject destination)
        {
            host = (string)destination.GetValue("host");
            port = (int)destination.GetValue("port");
            maxQueue = (int)destination.GetValue("maxQueue");
            maxConnection = (int)destination.GetValue("maxConnection");
            statusTls = (bool)destination.GetValue("statusTls");
        }

        public iec60870(JObject destination)
        {
            host = (string) destination.GetValue("host");
            port = (int) destination.GetValue("port");
            maxQueue = (int)destination.GetValue("maxQueue");
            maxConnection = (int)destination.GetValue("maxConnection");
            statusTls = (bool)destination.GetValue("statusTls");
        }
    }
}
