using Gateway.Destination;
using Gateway.Source;
using Newtonsoft.Json.Linq;

namespace Gateway
{
    public static class GateWay
    {
        private static bool _run;

        public static void InitializeGateWay(JObject file)
        {
            //Распарсить на объекты
            //Получаем все Destination
            foreach (var itemDestination in Destination.Parse.Destinations(file))
            {
                var destination = DestinationAPI.Add(itemDestination);
                if(destination == null)
                    continue;
                //Получаем все Source для данного Destination
                foreach (var itemSource in Source.Parse.Sources(itemDestination))
                {
                    var source = SourceAPI.Add(itemSource);
                    if (source == null)
                        continue;
                    //Получаем все Datum
                    foreach (var itemData in Datum.Parse.Data(itemSource))
                    {
                        var item = new Abstraction.Datum(source, destination, itemData);
                        destination.AddDatum(item);
                        source.AddDatum(item);
                    }
                }
            }

            //Инициализировать обработчик обработки данных
            DestinationAPI.InitHandlers();
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
                //Запустить 
                if (SourceAPI.Start())
                    if (DestinationAPI.Start())
                    {
                        _run = true;
                        ProccessRun();
                    }
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

        private static void ProccessRun()
        {
            while (_run)
            {

            }
        }
    }
}