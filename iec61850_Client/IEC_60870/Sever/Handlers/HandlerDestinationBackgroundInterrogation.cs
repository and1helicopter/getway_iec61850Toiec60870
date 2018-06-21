using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Abstraction;
using Logger;

namespace IEC_60870.Sever.Handlers
{
    public class HandlerDestinationBackgroundInterrogation : HandlerBasic, IHandlerDestination
    {
        public Dictionary<Source, Item> ListDictionary { get; set; }
        
        public void Process()
        {
            Dictionary<Source, ItemBridge> dictionary = ListDictionary.ToDictionary(item => item.Key, item => (ItemBridge)item.Value);
            Random rand = new Random();

            while (IsRun)
            {
                var item = dictionary.ElementAt(rand.Next(0,dictionary.Count));
                lock (_server._locker)
                    _server.GetValueAsync(item.Key, item.Value);
            }
        }

        public bool InitHandler(Dictionary<Source, Item> dictinory, Destination destination)
        {
            try
            {
                if (HandlerThread == null)
                {
                    //Создаем список объектов с данным идентификатором
                    if (ListDictionary == null)
                        ListDictionary = new Dictionary<Source, Item>();
                    if (_server == null)
                        lock (_server._locker)
                        {
                            _server = (IEC60870_Server)destination;
                        }

                    Dictionary<Source, ItemBridge> outputDictionary = dictinory.ToDictionary(item => item.Key, item => (ItemBridge)item.Value);
                    foreach (var item in outputDictionary.Where(item => item.Value.Item.Cot == 2).ToList())
                    {
                        ListDictionary.Add(item.Key, item.Value);
                    }

                    HandlerThread = new Thread(Process) { Name = GetType() + @"_" + $"{new Random(100000)}" };
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                Log.Write(new Exception("IEC_60870.Sever.Handlers.HandlerDestinationBackgroundInterrogation.InitHandlers()"), Log.Code.ERROR);
                return false;
            }
        }
    }

    public static partial class InitHandlers
    {
        public static bool ValidationBackgroundInterrogation(Dictionary<Source, Item> dictinory)
        {
            try
            {
                //Проверка есть ли объекты подходящие для этого обработчика
                Dictionary<Source, ItemBridge> outputDictionary = dictinory.ToDictionary(item => item.Key, item => (ItemBridge)item.Value);
                return outputDictionary.Any(item => item.Value.Item.Cot == 2);
            }
            catch
            {
                Log.Write(new Exception("IEC_60870.Sever.Handlers.ValidationBackgroundInterrogation()"), Log.Code.ERROR);
                return false;
            }
        }
    }
}
