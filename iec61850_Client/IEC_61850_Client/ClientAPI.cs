using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IEC61850.Common;

namespace IEC_61850
{
  public static class ClientAPI
	{
		private static readonly List<ClientConnect> ConnectionList = new List<ClientConnect>();

		public static void NewConnection(string host, int port)
		{
			var connect = new ClientConnect();
			connect.DefineConnection(host, port);
			ConnectionList.Add(connect);
		}

		public static void ConnectionDefineConnection(int index, string host, int port)
		{
			ConnectionList[index].DefineConnection(host, port);
		}

		public static void ConnectionDefinePassword(int index, string password)
		{
			if(!ConnectionList[index].RunConnect())
				ConnectionList[index].DefinePassword(password);
		}

		public static  void ConnectionDefineLocalAddresses(int index, uint pSelector, byte[] sSelector, byte[] tSelector)
		{
			if(!ConnectionList[index].RunConnect())
				ConnectionList[index].DefineLocalAddresses(pSelector, sSelector, tSelector);
		}

		public static void ConnectionDefineLocalApTitle(int index, string apTitle, int aeQualifier)
		{
			if(!ConnectionList[index].RunConnect())
				ConnectionList[index].DefineLocalApTitle(apTitle, aeQualifier);
		}

		public static void ConnectionDefineRemoteAddresses(int index, uint pSelector, byte[] sSelector, byte[] tSelector)
		{			
			if(!ConnectionList[index].RunConnect())
				ConnectionList[index].DefineRemoteAddresses(pSelector, sSelector, tSelector);
		}

		public static void ConnectionDefineRemoteApTitle(int index, string apTitle, int aeQualifier)
		{
			if(!ConnectionList[index].RunConnect())
				ConnectionList[index].DefineRemoteApTitle(apTitle, aeQualifier);
		}
		
		public static bool StartConnection(int index)
		{
			if (!ConnectionList[index].RunConnect())
			{
				if (ConnectionList[index].StartConnection())
				{
					ConnectionList[index].FillPathDA();
					return true;
				}
				return false;
			}
			return false;
		}

		public static void StopConnection(int index)
		{
			if(ConnectionList[index].RunConnect())
				ConnectionList[index].StopConnection();
		}

		public static void DelateConnection(int index)
		{
			if (!ConnectionList[index].RunConnect())
			{
				ConnectionList[index].StopConnection();
				ConnectionList.Remove(ConnectionList[index]);
			}
		}

		public static ClientConnect GetClientConnect(int index)
		{
			return ConnectionList[index];
		}

		public static List<ClientConnect.PathDA> GetPathDA(int index)
		{
			return ConnectionList[index].GetListPathDA();
		}

		public static async Task<dynamic> GetValue(int index, ClientConnect.PathDA item)
		{
			dynamic value =  await Task.Run(() =>
			{
				return ConnectionList[index].GetValue(item);
			});

			return value;
		}

		public static void SetValue(int index, dynamic newValue, dynamic oldValue, ulong operTm, ClientConnect.PathDA item, bool test, bool cheakInterlock, bool cheakSynchro, string originator, OrCat orCat)
		{
			var connection = ConnectionList[index];
			connection.SetValue(newValue, oldValue, operTm, item, test, cheakInterlock, cheakSynchro, originator, orCat);
		}
	}
}
