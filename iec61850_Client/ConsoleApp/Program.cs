using System.IO;
using Gateway;
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

            GateWayAPI.Initialize(objectsList);	
            GateWayAPI.Start();
		}
	}
}
