using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using IEC_61850;
using Logger;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Gateway
{
	public static class GatewayAPI_Class
	{
		private static List<Servers61850> ServersList = new List<Servers61850>();

		public static bool Add_Server61850(dynamic obj)
		{
			try
			{
				//Поступление объекта сервера из Configurator JS json
				Servers61850 newObjServer61850 = JsonConvert.DeserializeObject<Servers61850>(obj);
				ServersList.Add(newObjServer61850);
				var index = ServersList.Count - 1;
				//Добавить в модуль 
				ClientAPI.NewConnection("localhost", 102);
				SetParametrServer61850(newObjServer61850, index);
				return true;
			}
			catch (Exception e)
			{
				Logger.Log.Write(e, Log.Code.ERROR);
				return false;
			}
		}

		private static void SetParametrServer61850(Servers61850 objServer61850, int index)
		{
			var host = objServer61850.host;
			var port = objServer61850.port;
			ClientAPI.ConnectionDefineConnection(index, host, port);
			ClientAPI.ConnectionNewParameters(index);
			if (objServer61850.enabled)
			{
				var password = objServer61850.password;
				ClientAPI.ConnectionDefinePassword(index, password);
			}

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

		}

		public static bool Remove_Server61850(dynamic index)
		{
			if (ClientAPI.DelateConnection(index))
			{
				ServersList.RemoveAt(index);
				return true;
			}
			return false;
		}

		public static bool Start_Server61850(dynamic obj, dynamic index)
		{
			Servers61850 newObjServer61850 = JsonConvert.DeserializeObject<Servers61850>(obj);
			//Проверка существует ли уже такие настройки Сервера
			if (!ServersList[index].Equals(newObjServer61850))
			{
				ServersList[index] = newObjServer61850;
				//ServersList[index].items61850.Clear();
				SetParametrServer61850(newObjServer61850, index);
			}
			//Запуск сервера и проверка на успешность запуска 
			return ClientAPI.StartConnection(index);
		}

		public static bool Stop_Server61850(dynamic index)
		{
			return ClientAPI.StopConnection(index);
		}

		public static string Get_Items_Server61850(dynamic index)
		{
			ServersList[index].items61850.Clear();
			var listItems = ClientAPI.GetPathDA(index);
			foreach (var item in listItems)
			{
				ServersList[index].items61850.Add(new Item61850 { path = item.path, typeMMS = item.typeMMS, typeFC = item.typeFC });
			}

			return JsonConvert.SerializeObject(ServersList[index].items61850, Formatting.Indented, new StringEnumConverter(false));
		}

		public static bool Start_Server60870(dynamic obj)
		{
			return false;
		}


	}
}