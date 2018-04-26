using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Gateway.DataMap.Source
{
    public class iec61850 : Source
    {
        private string host { get; set; }
        private int port { get; set; }
        private string apTitleR { get; set; }
        private string aeQualifierR { get; set; }
        private string pSelectorR { get; set; }
        private string sSelectorR { get; set; }
        private string tSelectorR { get; set; }
        private string apTitleL { get; set; }
        private string aeQualifierL { get; set; }
        private string pSelectorL { get; set; }
        private string sSelectorL { get; set; }
        private string tSelectorL { get; set; }
        private bool enabled { get; set; }
        private string password { get; set; }

        public override Source GetSource()
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
        private List<string> attributeElement { get; set; } = new List<string>();

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