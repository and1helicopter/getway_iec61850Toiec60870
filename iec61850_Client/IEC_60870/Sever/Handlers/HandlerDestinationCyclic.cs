using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Abstraction;
using Logger;

namespace IEC_60870.Sever.Handlers
{
    public class HandlerCyclic:Handler,IHandlerDestination
    {
        public override Thread HandlerThread { get; set; }

        public override bool IsRun { get; set; }

        public override bool IsUse { get; set; }

        public Dictionary<Source, Item> ListDictionary { get; set; }

        private IEC60870_Server server;

        public override void Process()
        {
            Dictionary<Source, ItemBridge> dictionary = ListDictionary.ToDictionary(item => item.Key, item => (ItemBridge)item.Value);

            while (IsRun)
            {
                foreach (var item in dictionary)
                {
                    server.GetValue(item.Key, item.Value);
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
                    if (server == null)
                        server = (IEC60870_Server) destination;
                    Dictionary<Source, ItemBridge> outputDictionary = dictinory.ToDictionary(item => item.Key, item => (ItemBridge)item.Value);
                    foreach (var item in outputDictionary.Where(item => item.Value.Item.Cot == 1).ToList())
                    {
                        ListDictionary.Add(item.Key, item.Value);
                    }

                    HandlerThread = new Thread(Process) {Name = GetType() + @"_" + $"{new Random()}"};
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                Log.Write(new Exception("IEC_60870.Sever.Handlers.HandlerCyclic.InitHandler()"), Log.Code.ERROR);
                return false;
            }
        }

        public override bool Start()
        {
            try
            {
                if (HandlerThread != null)
                {
                    IsRun = true;
                    HandlerThread.Start();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                Log.Write(new Exception("IEC_60870.Sever.Handlers.HandlerCyclic.Start()"), Log.Code.ERROR);
                return false;
            }

        }

        public override bool Stop()
        {
            try
            {
                if (HandlerThread.IsAlive)
                {
                    IsRun = false;
                    HandlerThread.Abort();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                Log.Write(new Exception("IEC_60870.Sever.Handlers.HandlerCyclic.Stop()"), Log.Code.ERROR);
                return false;
            }
        }
    }

    public static partial class Init_Handler
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
