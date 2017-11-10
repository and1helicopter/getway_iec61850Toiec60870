using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IEC_61850
{
    public static class Client
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
			connect.DefinePassword(password);
		}

		public static  void ConnectionDefineLocalAddresses(ClientConnect connect, uint pSelector, byte[] sSelector, byte[] tSelector)
		{
			connect.DefineLocalAddresses(pSelector, sSelector, tSelector);
		}

		public static void ConnectionDefineLocalAddresses(ClientConnect connect, string apTitle, int aeQualifier)
		{
			connect.DefineLocalApTitle(apTitle, aeQualifier);
		}

		public static void ConnectionDefineRemoteAddresses(ClientConnect connect, uint pSelector, byte[] sSelector, byte[] tSelector)
		{
			connect.DefineRemoteAddresses(pSelector, sSelector, tSelector);
		}

		public static void ConnectionDefineRemoteApTitle(ClientConnect connect, string apTitle, int aeQualifier)
		{
			connect.DefineRemoteApTitle(apTitle, aeQualifier);
		}
		
		public static void StartConnection(ClientConnect connect)
		{
			connect.StartConnection();
			connect.FillPathDA();
		}

		public static void StopConnection(ClientConnect connect)
		{
			connect.StopConnection();
		}

		public static void DelateConnection(ClientConnect connect)
		{
			connect.StopConnection();
			ConnectionList.Remove(connect);
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

		public static void SetValue(string host, dynamic value, ClientConnect.PathDA item)
		{
			var connection = ConnectionList.First(x => x.GetConnetionHostPort().Equals(host));
			connection.SetValue(value, item);
		}
	}
}
