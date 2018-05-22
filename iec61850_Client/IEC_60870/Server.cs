using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using lib60870;
using lib60870.CS101;
using lib60870.CS104;

namespace IEC_60870
{
	public class Server
	{
	    private readonly lib60870.CS104.Server _server;
        private bool _workServer;
	    private readonly object _locker = new object();
	    private static List<string> WhiteListIP { get; set; } = new List<string>();
	    private static List<string> BlackListIP { get; set; } = new List<string>();

	    public Server(string host, int port, int maxQueue, int maxConnection, bool statusTls, List<string> whiteListIp, List<string> blackListIp)
        {
            _server = new lib60870.CS104.Server();
            _server.SetLocalAddress(host);
            _server.SetLocalPort(port);
            _server.MaxQueueSize = maxQueue;
            _server.MaxOpenConnections = maxConnection;

            if (whiteListIp != null)
                WhiteListIP = whiteListIp;
            if (blackListIp != null)
                BlackListIP = blackListIp;
        }

		public bool ServerStart()
		{
            _server.SetConnectionRequestHandler(connectionRequestHandler, null);            //Проверка  ip - адреса клиента на белый и черный список
            //_server.SetInterrogationHandler();                                                                                    //Обработчик команд опроса

            _server.Start();
			_workServer = true;
		    return _workServer;
		}
		
		public bool ServerStop()
		{
			_server.Stop();
			_workServer = false;
		    return _workServer;
		}
        
        //Проверка  ip - адреса клиента на белый и черный список
	    private static bool connectionRequestHandler(object parameter, IPAddress ipAddress)
	    {
	        if (!WhiteListIP.FindAll(x => x.ToString().Equals(ipAddress.ToString())).Any() && WhiteListIP.Any())
	        {
	            return false;
            }
            else if (BlackListIP.FindAll(x => x.ToString().Equals(ipAddress.ToString())).Any())
	        {
	            return false;
            }
            else
                return true;
	    }

		public void AddASDUServer(ASDU asdu)
		{
		    lock (_locker)
                _server.EnqueueASDU(asdu);
		}
	}
}