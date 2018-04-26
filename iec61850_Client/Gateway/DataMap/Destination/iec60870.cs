using System;
using System.Collections.Generic;
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
        private int typeID { get; set; } 
        private  bool sq { get; set; } 
        private int length { get; set; }
        private int cot { get; set; }
        private bool isNegative { get; set; }
        private bool isTest { get; set; }
        private int oa { get; set; }
        private int ca { get; set; }
        private int addrObj { get; set; }
        private List<string> typeElement { get; set; } = new List<string>();

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
