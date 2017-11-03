using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IEC_61850
{
    public static class Client
	{
		public static async Task<dynamic> GetValue(string host, ClientConnect.PathDA item)
		{
			dynamic value =  await Task.Run(() =>
			{
				return _connectionList.First(x => x.GetConnetionHostPort().Equals(host)).GetValue(item); ;
			});

			return value;
		}
		
		private static List<ClientConnect> _connectionList = new List<ClientConnect>();

		public static void AddConnection(ClientConnect connect)
		{
			_connectionList.Add(connect);
		}
	}
}
