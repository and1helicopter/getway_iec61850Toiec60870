using System.Collections.Generic;
using Gateway.DataMap.Destination;
using Gateway.DataMap.Source;
using Newtonsoft.Json.Linq;

namespace Gateway.DataMap
{
    public static class GateWay
    {
        private static readonly List<Source.Source> _source = new List<Source.Source>();
        private static readonly List<Destination.Destination> _destination = new List<Destination.Destination>(); 

        public static void InitializeGateWay(JObject file, Destinations destinations, Sources sources)
        {
            //Распарсить на объекты: 

            //Destination
            if (destinations == Destinations.IEC60870)
            {
                //Получаем все Destination
                foreach (var  itemDestination in Destination.ParseDestination.Destination(file))
                {
                    //Добавляем Destination

                    var destinationIEC60870 = new iec60870(ParseDestination.DestinationShort(itemDestination));

                    //Получаем все Source для данного Destination
                    //foreach (var VARIABLE in COLLECTION)
                    //{
                    //    new iec60870(asfasd)
                    //}

                }
            }


            //Item
            _source.Add(new source_iec61850(_source));
            _source.Add(new source_modBus());
            foreach (var itemSource in _source)
            {
                itemSource.GetSource();
            }
        }
    }


}