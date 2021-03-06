﻿using System;
using System.Collections.Generic;
using System.Linq;
using Abstraction;
using IEC_60870.Sever.Handlers;
using Newtonsoft.Json.Linq;
using Logger;

namespace IEC_60870
{
    public class IEC60870_Server : Destination
    {
        private string Host { get; }
        private int Port { get; }
        private int MaxQueue { get; }
        private int MaxConnection { get; }
        private bool StatusTls { get; }

        private Server IEC60870 { get; }

        public override bool IsRun { get; set; }
        internal readonly object _locker = new object();

        private Dictionary<Source, Item> Dictinory { get; } = new Dictionary<Source, Item>();
        private List<Handler> Handlers { get; } = new List<Handler>();

        public IEC60870_Server(JObject destination)
        {
            try
            {
                Host = destination.GetValue("host").ToString().ToLower() == "localhost" ? "127.0.0.1" : destination.GetValue("host").ToString().ToLower();
                Port = (int)destination.GetValue("port");
                MaxQueue = (int)destination.GetValue("maxQueue");
                MaxConnection = (int)destination.GetValue("maxConnection");
                StatusTls = (bool)destination.GetValue("statusTls");

                List<string> bLip = new List<string>();
                List<string> wLip = new List<string>();

                foreach (var item in destination.GetValue("blackListIP").ToList())
                {
                    bLip.Add(item.ToString());
                }

                foreach (var item in destination.GetValue("whiteListIP").ToList())
                {
                    wLip.Add(item.ToString());
                }

                IEC60870 = new Server(Host, Port, MaxQueue, MaxConnection, StatusTls, wLip, bLip);
            }
            catch (Exception e)
            {
                Log.Write(e, Log.Code.ERROR);
            }
        }
        
        public override bool Start()
        {
            try
            {
                //iec60870.ServerSetHandlers();
                foreach (var handler in Handlers)
                {
                    handler.Start();
                }
                IsRun = IEC60870.ServerStart();
                return IsRun;
            }
            catch (Exception e)
            {
                Log.Write(e, Log.Code.ERROR);
                return IsRun;
            }
        }

        public override bool Stop()
        {
            IsRun = IEC60870.ServerStop();
            return !IsRun;
        }

        public override void GetValueAsync(Source source, Item item)
        {
           TaskGetValue(source, IEC60870, item);
        }

        static void TaskGetValue(Source source, Server destination, Item items)
        {
            var value = new dynamic[items.Dictionary.Count];
            //(ItemBridge)items.Dictionary.
            foreach (var item in items.Dictionary.Select((item, index) => new { item, index }))
            {
                value[item.index] = source.GetValueAsync(item.item.Key);
            }

            //Созданый ASDU и отправить на сервер
            destination.AddASDUServer(items, value);
        }

        public override void SetValue(Source source, Item item, dynamic value)
        {
             
        }

        public override dynamic ShortInfo()
        {
               return (Host, Port);
        }

        public override bool InitHandlers()
        {
            try
            {
                if (Sever.Handlers.InitHandlers.ValidationCyclic(Dictinory))
                {
                    var cyclicHandler = new HandlerCyclic();
                    //Добавляем к обработчику
                    if (cyclicHandler.InitHandler(Dictinory, this))
                    {
                        Handlers.Add(cyclicHandler);
                    }
                }

                if (Sever.Handlers.InitHandlers.ValidationBackgroundInterrogation(Dictinory))
                {
                    var backgroundInterrogation = new HandlerDestinationBackgroundInterrogation();
                    //Добавляем к обработчику
                    if (backgroundInterrogation.InitHandler(Dictinory, this))
                    {
                        Handlers.Add(backgroundInterrogation);
                    }
                }
                
                if (Sever.Handlers.InitHandlers.ValidationSpontaneous(Dictinory))
                {
                    var spontaneous = new HandlerDestinationSpontaneous();
                    //Добавляем к обработчику
                    if (spontaneous.InitHandler(Dictinory, this))
                    {
                        Handlers.Add(spontaneous);
                    }
                }

                //Добавить обработчики 
                IEC60870.ServerSetHandlers();
                
                return true;
            }
            catch
            {
                Log.Write(new Exception("IEC60870_Server.InitHandlers()"), Log.Code.ERROR);
                return false;
            }

        }

        public override bool AddDatum(Datum datum)
        {
            try
            {
                Dictinory.Add(datum.Source, datum.Item);
                return true;
            }
            catch
            {
                Log.Write(new Exception("IEC60870_Server.AddDatum()"), Log.Code.ERROR);
                return false;
            }
        }

        public override Item InitItem(JObject itemsDestination, Source source)
        {
            try
            {
                var typeId = (int) itemsDestination.GetValue("typeID");
                var sq = (bool) itemsDestination.GetValue("sq");
                var length = (int) itemsDestination.GetValue("length");
                var cot = (int) itemsDestination.GetValue("cot");
                var isNegative = (bool) itemsDestination.GetValue("isNegative");
                var isTest = (bool) itemsDestination.GetValue("isTest");
                var oa = (int) itemsDestination.GetValue("oa");
                var ca = (int) itemsDestination.GetValue("ca");

                var item60870 = new Item60870(typeId, sq, length, cot, isNegative, isTest, oa, ca);
                Dictionary<ItemSource, ItemDestination> dictionary = new Dictionary<ItemSource, ItemDestination>();
                var index = 0;

                foreach (var jToken in itemsDestination["objects"])
                {
                    var itemObj = (JObject) jToken;
                    var addrObj = (int) itemObj.GetValue("addrObj");
                    var attributeObj = itemObj["attributeObj"];

                    var listAttributeObj = attributeObj.First().ToList();
                    
                    foreach (var itemAttributeObj in listAttributeObj)
                    {
                        //Создаем Destination
                        var item = itemAttributeObj;
                        var typeElement = (string)item["typeElement"];
                        var indexElement = index++;
                        //Получить ItemDestination
                        var itemDestination = new Obj(addrObj, typeElement, indexElement);

                        //Создаем Source
                        var attributeElement = new JObject(item["attributeElement"].Parent);
                        //Получить ItemSource
                        var itemSource = source.InitItemSource(attributeElement);
                        if(itemSource != null)
                            dictionary.Add(itemSource, itemDestination);
                    }
                }

                return new ItemBridge(item60870, dictionary);
            }
            catch
            {
                Log.Write(new Exception("IEC60870_Server.InitItem()"), Log.Code.ERROR);
                return null;
            }
        }
    }
}
