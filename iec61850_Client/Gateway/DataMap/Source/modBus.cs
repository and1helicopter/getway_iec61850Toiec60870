using System;
using Newtonsoft.Json.Linq;

namespace Gateway.DataMap.Source
{
    public class modBus : Source
    {


        public override Source GetSource()
        {
            return this;
        }

        public override void SetSource(JObject source)
        {

        }

        public modBus(JObject source)
        {

        }
    }
}