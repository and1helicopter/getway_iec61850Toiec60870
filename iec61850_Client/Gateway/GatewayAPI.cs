using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using IEC_61850;
using Newtonsoft.Json;

namespace Gateway
{
	public static class GatewayAPI_Class
	{
		private static List<Servers61850> ServersList = new List<Servers61850>();

		public static void Add_Server61850(dynamic obj)
		{
			//Поступление объекта сервера из Configurator JS json
			Servers61850 newObjServer61850 = JsonConvert.DeserializeObject<Servers61850>(obj);
			ServersList.Add(newObjServer61850);
			//Добавить в модуль 
			var host = newObjServer61850.host;
			var port = newObjServer61850.port;
			ClientAPI.NewConnection(host, port);
			//var pSelectorL = Convert.ToUInt32(newObjServer61850.pSelectorL);
			//var sSelectorL = Encoding.ASCII.GetBytes(newObjServer61850.sSelectorL);
			//var tSelectorL = Encoding.ASCII.GetBytes(newObjServer61850.tSelectorL);
			//tempServer.DefineLocalAddresses(pSelectorL, sSelectorL, tSelectorL);
			//var apTitleL = newObjServer61850.apTitleL;
			//var aeQualifierL = Convert.ToInt32(newObjServer61850.aeQualifierL);
			//tempServer.DefineLocalApTitle(apTitleL, aeQualifierL);
			//var pSelectorR = Convert.ToUInt32(newObjServer61850.pSelectorR);
			//var sSelectorR = Encoding.ASCII.GetBytes(newObjServer61850.sSelectorR);
			//var tSelectorR = Encoding.ASCII.GetBytes(newObjServer61850.tSelectorR);
			//tempServer.DefineLocalAddresses(pSelectorR, sSelectorR, tSelectorR);
			//var apTitleR = newObjServer61850.apTitleR;
			//var aeQualifierR = Convert.ToInt32(newObjServer61850.aeQualifierR);
			//tempServer.DefineLocalApTitle(apTitleR, aeQualifierR);
			//var password = newObjServer61850.password;
			//tempServer.DefinePassword(password);
		}

		public static void Remove_Server61850(dynamic index)
		{
		
		}

		public static bool Start_Server61850(dynamic obj, dynamic index)
		{
			Servers61850 newObjServer61850 = JsonConvert.DeserializeObject<Servers61850>(obj);
			//Проверка существует ли уже такие настройки Сервера
			if (!ServersList[index].Equals(newObjServer61850))
			{
				ServersList[index] = newObjServer61850;
				var host = newObjServer61850.host;
				var port = newObjServer61850.port;
				ClientAPI.ConnectionDefineConnection(index, host, port);
				//var password = newObjServer61850.password;
				//tempServer.DefinePassword(password);
			}
			//Запуск сервера и проверка на успешность запуска 
			var status = ClientAPI.StartConnection(index);
			if (!status)
				return false;
			else
				return true;
		}

		public static string Get_Items_Server61850(dynamic index)
		{
			ServersList[index].items61850.Clear();
			var listItems = ClientAPI.GetPathDA(index);
			foreach (var item in listItems)
			{
				ServersList[index].items61850.Add(new Item61850 { path = item.path, typeMMS = item.typeMMS, typeFC = item.typeFC });
			}
			return JsonConvert.SerializeObject(ServersList[index].items61850, Formatting.Indented);
		}

		public static void Stop_Server61850(dynamic index)
		{
			
		}

		private static void SetParametrServer61850()
		{
			
		}
	}
}