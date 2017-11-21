using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Gateway
{
	public static class DataMapping
	{
		private static List<DataItem> ObjectsList = new List<DataItem>();  
		
		public static void OpenSetting()
		{
			using (StreamReader reader = new StreamReader("test.json"))
			{	
				ObjectsList = JsonConvert.DeserializeObject<List<DataItem>>(reader.ReadToEnd());
			}
		}
	}
}