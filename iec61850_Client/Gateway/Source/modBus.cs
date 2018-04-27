using Newtonsoft.Json.Linq;

namespace Gateway.Source
{
    public class modBus : Gateway.Source.Source
    {


        public override Gateway.Source.Source GetSource()
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