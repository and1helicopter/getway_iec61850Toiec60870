using System.Collections.Generic;
using System.Linq;
using Abstraction;
using Newtonsoft.Json.Linq;

namespace IEC_60870
{
    public class IEC60870 : Destination
    {
        private string Host { get; }
        private int Port { get; }
        private int MaxQueue { get; }
        private int MaxConnection { get; }
        private bool StatusTls { get; }

        private Server iec60870 { get; set; }

        public override bool IsRun { get; set; }

        public IEC60870(JObject destination)
        {
            Host = destination.GetValue("host").ToString().ToLower() == "localhost" ? "127.0.0.1" : destination.GetValue("host").ToString().ToLower();
            Port = (int)destination.GetValue("port");
            MaxQueue = (int)destination.GetValue("maxQueue");
            MaxConnection = (int)destination.GetValue("maxConnection");
            StatusTls = (bool)destination.GetValue("statusTls");
            
            List<string> bLip = new List<string>();
            List<string> wLip = new List<string>();

            foreach (var item in destination.GetValue("blackListIP").ToList())
            {
                bLip.Add(item.ToString());
            }

            foreach (var item in destination.GetValue("whiteListIP").ToList())
            {
                wLip.Add(item.ToString());
            }

            iec60870 = new Server(Host, Port, MaxQueue, MaxConnection, StatusTls, wLip, bLip);
        }
        
        public override bool Start()
        {
            IsRun = iec60870.ServerStart();
            return IsRun;
        }

        public override bool Stop()
        {
            IsRun = iec60870.ServerStop();
            return !IsRun;
        }

        public override dynamic GetValue()
        {
            throw new System.NotImplementedException();
        }

        public override dynamic SetValue()
        {
            throw new System.NotImplementedException();
        }

        public override dynamic ShortInfo()
        {
               return (Host, Port);
        }
    }
}
