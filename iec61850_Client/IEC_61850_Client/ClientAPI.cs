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

		public static void ConnectionDefinePassword(ClientConnect connect, string password)
		{
			if(!connect.RunConnect())
				connect.DefinePassword(password);
		}

		public static  void ConnectionDefineLocalAddresses(ClientConnect connect, uint pSelector, byte[] sSelector, byte[] tSelector)
		{
			if(!connect.RunConnect())
				connect.DefineLocalAddresses(pSelector, sSelector, tSelector);
		}

		public static void ConnectionDefineLocalApTitle(ClientConnect connect, string apTitle, int aeQualifier)
		{
			if(!connect.RunConnect())
				connect.DefineLocalApTitle(apTitle, aeQualifier);
		}

		public static void ConnectionDefineRemoteAddresses(ClientConnect connect, uint pSelector, byte[] sSelector, byte[] tSelector)
		{			
			if(!connect.RunConnect())
				connect.DefineRemoteAddresses(pSelector, sSelector, tSelector);
		}

		public static void ConnectionDefineRemoteApTitle(ClientConnect connect, string apTitle, int aeQualifier)
		{
			if(!connect.RunConnect())
				connect.DefineRemoteApTitle(apTitle, aeQualifier);
		}
		
		public static void StartConnection(ClientConnect connect)
		{
			if (!connect.RunConnect())
			{
				connect.StartConnection();
				connect.FillPathDA();
			}
		}

		public static void StopConnection(ClientConnect connect)
		{
			if(connect.RunConnect())
				connect.StopConnection();
		}

		public static void DelateConnection(ClientConnect connect)
		{
			if (!connect.RunConnect())
			{
				connect.StopConnection();
				ConnectionList.Remove(connect);
			}
		}

		public static ClientConnect GetClientConnect(string host)
		{
			var temp = ConnectionList.First(x => x.GetConnetionHostPort().Equals(host));
			return temp;
		}

		public static List<ClientConnect.PathDA> GetPathDA(ClientConnect connect)
		{
			return connect.GetListPathDA();
		}

		public static async Task<dynamic> GetValue(string host, ClientConnect.PathDA item)
		{
			dynamic value =  await Task.Run(() =>
			{
				return ConnectionList.First(x => x.GetConnetionHostPort().Equals(host)).GetValue(item);
			});

			return value;
		}

		public static void SetValue(string host, dynamic newValue, dynamic oldValue, ulong operTm, ClientConnect.PathDA item, bool test, bool cheakInterlock, bool cheakSynchro, string originator, OrCat orCat)
		{
			var connection = ConnectionList.First(x => x.GetConnetionHostPort().Equals(host));
			connection.SetValue(newValue, oldValue, operTm, item, test, cheakInterlock, cheakSynchro, originator, orCat);
		}
	}
}
