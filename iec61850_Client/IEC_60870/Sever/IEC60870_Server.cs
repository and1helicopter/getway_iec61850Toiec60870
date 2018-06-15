using System;
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

        private Server iec60870 { get; }

        public override bool IsRun { get; set; }

        private Dictionary<Source, Item> dictinory { get; } = new Dictionary<Source, Item>();

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

                iec60870 = new Server(Host, Port, MaxQueue, MaxConnection, StatusTls, wLip, bLip);
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
                iec60870.ServerSetHandlers();
                IsRun = iec60870.ServerStart();
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
            IsRun = iec60870.ServerStop();
            return !IsRun;
        }

        public override dynamic GetValue()
        {
            throw new System.NotImplementedException();
        }

        public override dynamic SetValue()
        {
            throw new System.NotImplementedException();
        }

        public override dynamic ShortInfo()
        {
               return (Host, Port);
        }

        public override bool InitHandlers()
        {
            if (Init_Handler.Validation_cyc())
            {
                //Добавляем к обработчику
            }

            return true;
        }

        public override bool AddDatum(Datum datum)
        {
            try
            {
                dictinory.Add(datum.Source, datum.Item);
                return true;
            }
            catch
            {
                Log.Write(new Exception("IEC60870_Server.AddDatum()"), Log.Code.ERROR);
                return false;
            }
        }

        public override Item InitItem(JObject itemDestination, Source source)
        {
            try
            {
                var typeId = (int) itemDestination.GetValue("typeID");
                var sq = (bool) itemDestination.GetValue("sq");
                var length = (int) itemDestination.GetValue("length");
                var cot = (int) itemDestination.GetValue("cot");
                var isNegative = (bool) itemDestination.GetValue("isNegative");
                var isTest = (bool) itemDestination.GetValue("isTest");
                var oa = (int) itemDestination.GetValue("oa");
                var ca = (int) itemDestination.GetValue("ca");

                var item60870 = new Item60870(typeId, sq, length, cot, isNegative, isTest, oa, ca);
                Dictionary<ItemSource, ItemDestination> dictionary = new Dictionary<ItemSource, ItemDestination>();

                foreach (JObject itemObj in itemDestination["objects"])
                {
                    var addrObj = (int) itemObj.GetValue("addrObj");
                    var attributeObj = itemObj["attributeObj"];

                    var listAttributeObj = attributeObj.ToList();


                    foreach (var itemAttributeObj in listAttributeObj)
                    {
                        //Создаем Destination
                        var item = itemAttributeObj.First;
                        var typeElement = (string)item["typeElement"];
                        var indexElement = listAttributeObj.IndexOf(itemAttributeObj);
                        //Получить ItemDestination
                        var _itemDestination = new Obj(addrObj, typeElement, indexElement);

                        //Создаем Source
                        var attributeElement = new JObject(item["attributeElement"].Parent);
                        //Получить ItemSource
                        var _itemSource = source.InitItemSource(attributeElement);
                        dictionary.Add(_itemSource, _itemDestination);
                    }
                }

                return new ItemDictinory(item60870, dictionary);
            }
            catch
            {
                Log.Write(new Exception("InitItem()"), Log.Code.ERROR);
                return null;
            }
        }

        private static IEnumerable<JObject> JObjectEnumerable(JObject file, string key)
        {
            JObject temp = file;
            //Проверка на существование
            if (temp.ContainsKey(key))
            {
                var list = temp[key];
                foreach (var item in list)
                {
                    yield return (JObject)item;
                }
            }
        }
    }
}
