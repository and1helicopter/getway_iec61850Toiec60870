using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Gateway.Source
{
    public class iec61850 : Gateway.Source.Source
    {
        public string host { get; set; }
        public int port { get; set; }
        public string apTitleR { get; set; }
        public string aeQualifierR { get; set; }
        public string pSelectorR { get; set; }
        public string sSelectorR { get; set; }
        public string tSelectorR { get; set; }
        public string apTitleL { get; set; }
        public string aeQualifierL { get; set; }
        public string pSelectorL { get; set; }
        public string sSelectorL { get; set; }
        public string tSelectorL { get; set; }
        public bool enabled { get; set; }
        public string password { get; set; }

        public override Gateway.Source.Source GetSource()
        {
            return this;
        }

        public override void SetSource(JObject source)
        {
            host = (string)source.GetValue("host");
            port = (int)source.GetValue("port");
            apTitleR = (string)source.GetValue("apTitleR");
            aeQualifierR = (string)source.GetValue("aeQualifierR");
            pSelectorR = (string)source.GetValue("pSelectorR");
            sSelectorR = (string)source.GetValue("sSelectorR");
            tSelectorR = (string)source.GetValue("tSelectorR");
            apTitleL = (string)source.GetValue("apTitleL");
            aeQualifierL = (string)source.GetValue("aeQualifierL");
            pSelectorL = (string)source.GetValue("pSelectorL");
            sSelectorL = (string)source.GetValue("sSelectorL");
            tSelectorL = (string)source.GetValue("tSelectorL");
            enabled = (bool)source.GetValue("aeQualifierR");
            password = (string)source.GetValue("password");
        }

        public iec61850(JObject source)
        {
            if(source == null) return;
            host = (string)source.GetValue("host");
            port = (int)source.GetValue("port");
            apTitleR = (string)source.GetValue("apTitleR");
            aeQualifierR = (string)source.GetValue("aeQualifierR"); 
            pSelectorR = (string)source.GetValue("pSelectorR");
            sSelectorR = (string)source.GetValue("sSelectorR");
            tSelectorR = (string)source.GetValue("tSelectorR");
            apTitleL = (string)source.GetValue("apTitleL");
            aeQualifierL = (string)source.GetValue("aeQualifierL");
            pSelectorL = (string)source.GetValue("pSelectorL");
            sSelectorL = (string)source.GetValue("sSelectorL");
            tSelectorL = (string)source.GetValue("tSelectorL");
            enabled = (bool) source.GetValue("enabled");
            password = (string)source.GetValue("password");
        }
    }

    public class iec61850Path:SourcePath
    {
        public List<string> attributeElement { get; set; } = new List<string>();

        public override SourcePath GetSourcePath()
        {
            return this;
        }

        public override void SetSourcePath(JObject source)
        {
            throw new System.NotImplementedException();
        }

        public iec61850Path(JObject item)
        {
            foreach (var temp in item["attributeObj"])
            {
                attributeElement.Add((string)((JObject)temp[0]).GetValue("attributeElement"));
            }
        }
    }
}