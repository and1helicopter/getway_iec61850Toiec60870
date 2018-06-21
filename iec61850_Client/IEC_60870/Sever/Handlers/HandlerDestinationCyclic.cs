using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Abstraction;
using Logger;

namespace IEC_60870.Sever.Handlers
{
    public class HandlerCyclic: HandlerBasic, IHandlerDestination
    {
        public Dictionary<Source, Item> ListDictionary { get; set; }

        public void Process()
        {
            Dictionary<Source, ItemBridge> dictionary = ListDictionary.ToDictionary(item => item.Key, item => (ItemBridge)item.Value);

            while (IsRun)
            {
                foreach (var item in dictionary)
                {
                    lock (Server._locker)
                        Server.GetValueAsync(item.Key, item.Value);
                }
            }
        }

        public bool InitHandler(Dictionary<Source, Item> dictinory, Destination destination)
        {
            try
            {
                if (HandlerThread == null)
                {
                    //Создаем список объектов с данным идентификатором
                    if(ListDictionary == null)
                        ListDictionary = new Dictionary<Source, Item>();
                    if (Server == null)
                        Server = (IEC60870_Server) destination;
                    Dictionary<Source, ItemBridge> outputDictionary = dictinory.ToDictionary(item => item.Key, item => (ItemBridge)item.Value);
                    foreach (var item in outputDictionary.Where(item => item.Value.Item.Cot == 1).ToList())
                    {
                        ListDictionary.Add(item.Key, item.Value);
                    }

                    HandlerThread = new Thread(Process) {Name = GetType() + @"_" + $"{new Random().Next(100000)}"};
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                Log.Write(new Exception("IEC_60870.Sever.Handlers.HandlerCyclic.InitHandlers()"), Log.Code.ERROR);
                return false;
            }
        }
    }

    public static partial class InitHandlers
    {
        public static bool ValidationCyclic(Dictionary<Source, Item> dictinory)
        {
            try
            {
                //Проверка есть ли объекты подходящие для этого обработчика
                Dictionary<Source, ItemBridge> outputDictionary = dictinory.ToDictionary(item => item.Key, item => (ItemBridge)item.Value);
                return outputDictionary.Any(item => item.Value.Item.Cot == 1);
            }
            catch
            {
                Log.Write(new Exception("IEC_60870.Sever.Handlers.ValidationCyclic()"), Log.Code.ERROR);
                return false;
            }
        }

    }
}
