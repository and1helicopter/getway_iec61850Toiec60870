using System;
using System.Collections.Generic;
using System.Text;
using Abstraction;
using Logger;
using Newtonsoft.Json.Linq;

namespace IEC_61850
{
    public class IEC61850_Client:Source
    {
        private string Host { get; }
        private int Port { get; }
        private string ApTitleR { get; }
        private int AeQualifierR { get; }
        private uint PSelectorR { get; }
        private byte[] SSelectorR { get; }
        private byte[] TSelectorR { get; }
        private string ApTitleL { get; }
        private int AeQualifierL { get; }
        private uint PSelectorL { get; }
        private byte[] SSelectorL { get; }
        private byte[] TSelectorL { get; }
        private bool Enabled { get; }
        private string Password { get; }

        private Client iec61850 { get; }

        public override bool IsRun { get; set; }

        private Dictionary<Destination, Item> dictinory { get; } = new Dictionary<Destination, Item>();

        public IEC61850_Client(JObject source)
        {
            try
            {
                Host = (string)source.GetValue("host");
                Port = (int)source.GetValue("port");
                ApTitleR = (string)source.GetValue("apTitleR");
                AeQualifierR = (int)source.GetValue("aeQualifierR");
                PSelectorR = (uint)source.GetValue("pSelectorR");

                
                SSelectorR = GetByte(source.GetValue("sSelectorR").ToString());
                TSelectorR = GetByte(source.GetValue("tSelectorR").ToString());
                ApTitleL = (string)source.GetValue("apTitleL");
                AeQualifierL = (int)source.GetValue("aeQualifierL");
                PSelectorL = (uint)source.GetValue("pSelectorL");
                SSelectorL = GetByte(source.GetValue("sSelectorL").ToString());
                TSelectorL = GetByte(source.GetValue("tSelectorL").ToString());
                Enabled = (bool)source.GetValue("enabled");
                Password = (string)source.GetValue("password");

               iec61850 = new Client(Host, Port, ApTitleR, AeQualifierR, PSelectorR, SSelectorR, TSelectorR, ApTitleL, AeQualifierL, PSelectorL, SSelectorL, TSelectorL, Enabled, Password);
            }
            catch (Exception e)
            {
                Log.Write(e, Log.Code.ERROR);
            }
        }

        private byte[] GetByte(string str)
        {
            return Encoding.ASCII.GetBytes(str); ;
        }

        public override dynamic GetValue(ItemSource datum)
        {
            throw new NotImplementedException();
        }

        public override dynamic SetValue(ItemSource datum)
        {
            throw new System.NotImplementedException();
        }

        public override bool Start()
        {
            try
            {
                IsRun = iec61850.Start();
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
            throw new System.NotImplementedException();
        }

        public override dynamic ShortInfo()
        {
            return (Host, Port);
        }

        public override bool InitHandlers()
        {
            throw new System.NotImplementedException();
        }

        public override ItemSource InitItemSource(JObject itemSource)
        {
            return new Item61850((string)itemSource.GetValue("attributeElement"));
        }

        public override bool AddDatum(Datum datum)
        {
            try
            {
                dictinory.Add(datum.Destination, datum.Item);
                return true;
            }
            catch
            {
                Log.Write(new Exception("IEC61850_Client.AddDatum()"), Log.Code.ERROR);
                return false;
            }
        }

        public class Item61850:ItemSource
        {
            public string PathItem { get; }

            public Item61850(string pathItem)
            {
                PathItem = pathItem;
            }
        }
    }
}
