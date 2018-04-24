using System;
using System.Collections.Generic;

namespace Gateway.DataMap.Source
{
    public class source_iec61850 : Source
    {
        public string host { get; }
        public int port { get; }
        public string apTitleR { get; }
        public string aeQualifierR { get; }
        public string pSelectorR { get; }
        public string sSelectorR { get; }
        public string tSelectorR { get; }
        public string apTitleL { get; }
        public string aeQualifierL { get; }
        public string pSelectorL { get; }
        public string sSelectorL { get; }
        public string tSelectorL { get; }
        public bool enabled { get; }
        public string password { get; }

        public override void GetSource()
        {
            Console.WriteLine("iec61870");
        }

        public override void SetSource()
        {

        }

        public source_iec61850(dynamic _source)
        {

        }

        /*
            string _host, int _port, string _apTitleR, string _aeQualifierR, string _pSelectorR, string _sSelectorR, string _tSelectorR,
            string _apTitleL, string _aeQualifierL, string _pSelectorL, string _sSelectorL, string _tSelectorL, bool _enabled, string _password

            host = _host;
            port = _port;
            apTitleR = _apTitleR;
            aeQualifierR = _aeQualifierR;
            pSelectorR = _pSelectorR;
            sSelectorR = _sSelectorR;
            tSelectorR = _tSelectorR;
            apTitleL = _apTitleL;
            aeQualifierL = _aeQualifierL;
            pSelectorL = _pSelectorL;
            sSelectorL = _sSelectorL;
            tSelectorL = _tSelectorL;
            enabled = _enabled;
            password = _password;
         */

    }
}