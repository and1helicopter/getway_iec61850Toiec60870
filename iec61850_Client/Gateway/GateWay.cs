using System.Collections.Generic;
using Gateway.Destination;
using Gateway.Source;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Gateway
{
    public static class GateWay
    {
        delegate void Heandler(List<JObject> list);
        private static Heandler _heandler;
        private static readonly List<JObject> Data = new List<JObject>();
        private static bool _run;

        public static void InitializeGateWay(JObject file, Destinations destinations, Sources sources)
        {

            //Распарсить на объекты
            //Получаем все Destination
            foreach (var itemDestination in ParseDestination.Destination(file))
            {
               Destination.Destination destination = DestinationTemp(destinations, itemDestination);
                if(destination == null)
                    break;
                //Получаем все Source для данного Destination
                foreach (var itemSource in ParseSource.Source(itemDestination))
                {
                    Source.Source source = SourceTemp(sources, itemSource);
                    if (source == null)
                        break;
                    //Data 
                    foreach (var itemData in ParseDestination.InfoDestination(itemSource))
                    {
                        if(itemData == null)
                            break;
                        foreach (var item in ParseSource.InfoSource(itemData))
                        {
                            if(item == null)
                                break;
                            //
                            var infoDestination = DestinationPathTemp(destinations, itemData, item);
                            var infoSource = SourcePathTemp(sources, item);
                            //Заносим элементы в список Data 
                            Data.Add(JObject.Parse(JsonConvert.SerializeObject(new Data.Data(source, destination, infoSource, infoDestination))));
                        }
                    }
                }
            }

            //Инициализировать обработчик обработки данных
            //Оброботчик находится на стороне Destination
            switch (destinations)
            {
                case Destinations.IEC60870:
                    _heandler = IEC_60870_BL.Logic.Heandler;
                    break;
            }
        }

        private static DestinationPath DestinationPathTemp(Destinations destinations, JObject itemData, JObject item)
        {
            DestinationPath destination = null;
            switch (destinations)
            {
                case Destinations.IEC60870:
                    destination = new iec60870Path(itemData, item);
                    break;
            }
            return destination;
        }

        private static SourcePath SourcePathTemp(Sources sources,  JObject item)
        {
            SourcePath source = null;
            switch (sources)
            {
                case Sources.IEC61850:
                    source = new iec61850Path(item);
                    break;
                case Sources.MODBUS:

                    break;
            }
            return source;
        }

        private static Destination.Destination DestinationTemp(Destinations destinations, JObject itemDestination)
        {
            Destination.Destination destination = null;
            switch (destinations)
            {
                case Destinations.IEC60870:
                    destination = new iec60870(ParseDestination.DestinationShort(itemDestination));
                    break;
            }
            return destination;
        }

        private static Source.Source SourceTemp(Sources sources, JObject itemSource)
        {
            Source.Source source = null;
            switch (sources)
            {
                case Sources.IEC61850:
                    source = new iec61850(ParseSource.SourceShort(itemSource));
                    break;
                case Sources.MODBUS:
                    source = new modBus(ParseSource.SourceShort(itemSource));
                    break;
            }
            return source;
        }

        public static void DeInitializeGateWay()
        {
            if (!_run)
            { 

            }
            //DataItems.Clear();
        }

        public static void StartGateWay()
        {
            if (!_run)
            {
                //Передать обработчик 
                _heandler?.Invoke(Data);
                _run = true;
            }
        }

        public static void StopGateWay()
        {
            if (_run)
            {
                //Передать обработчик 
                _run = false;
            }
        }
    }
}