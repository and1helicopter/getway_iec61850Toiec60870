using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using IEC61850.Common;
using Logger;
using IEC_61850;
using IEC_60870;
using lib60870;
using Gateway;
using Gateway.DataMap;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ConsoleApp
{
	class Program
	{
		static void Main(string[] args)
		{
		    JObject objectsList;

            {
		        using (StreamReader reader = new StreamReader("settings.json"))
		        {
		           objectsList = JObject.Parse(reader.ReadToEnd());
		        }
		    }

            GateWayAPI.Initialize(objectsList, Destinations.IEC60870, Sources.IEC61850);	
		}
	}
}
