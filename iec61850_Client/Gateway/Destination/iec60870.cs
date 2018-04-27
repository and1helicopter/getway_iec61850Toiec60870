using System.Collections.Generic;
using Gateway.Destination;
using Newtonsoft.Json.Linq;

namespace Gateway.Destination
{
    public class iec60870 : Destination
    {
        public string host { get; set; }
        public int port  { get; set; }
        public int maxQueue { get; set; }
        public int  maxConnection  { get; set; }
        public bool statusTls  { get; set; }

        public override Destination GetDestination()
        {
            return this;
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

    public class iec60870Path : DestinationPath
    {
        public int typeID { get; }
        public bool sq { get; }
        public int length { get; }
        public int cot { get; }
        public bool isNegative { get; }
        public bool isTest { get; }
        public int oa { get; }
        public int ca { get; }
        public int addrObj { get; }
        public List<string> typeElement { get; } = new List<string>();

        public override DestinationPath GetDestinationPath()
        {
            return this;
        }

        public override void SetDestinationPath(JObject destination)
        {
            throw new System.NotImplementedException();
        }

        public iec60870Path(JObject itemData, JObject item)
        {
            typeID =(int)itemData.GetValue("typeID");
            sq = (bool) itemData.GetValue("sq");
            length = (int) itemData.GetValue("length");
            cot = (int)itemData.GetValue("cot");
            isNegative = (bool)itemData.GetValue("isNegative");
            isTest = (bool)itemData.GetValue("isTest");
            oa = (int)itemData.GetValue("oa");
            ca = (int)itemData.GetValue("ca");
            addrObj = (int) item.GetValue("addrObj");

            foreach (var temp in item["attributeObj"])
            {
                typeElement.Add((string)((JObject)temp[0]).GetValue("typeElement"));
            }
        }
    }
}
