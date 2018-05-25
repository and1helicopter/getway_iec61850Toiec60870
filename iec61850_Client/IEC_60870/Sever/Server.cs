using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using lib60870.CS101;
using Logger;

namespace IEC_60870
{
	public partial class Server
	{
	    private static lib60870.CS104.Server _server;
        private static bool _workServer;
	    private static readonly object _locker = new object();
	    private static List<string> WhiteListIP { get; set; } = new List<string>();
	    private static List<string> BlackListIP { get; set; } = new List<string>();

	    public Server(string host, int port, int maxQueue, int maxConnection, bool statusTls, List<string> whiteListIp, List<string> blackListIp)
        {
            try
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
            catch (Exception e)
            {
                Log.Write(e, Log.Code.ERROR);
            }
        }

		public bool ServerStart()
		{
            _server.Start();
			_workServer = true;
		    return _workServer;
		}
		
        public void ServerSetHandlers()
        {
            _server.SetConnectionRequestHandler(connectionRequestHandler, null);                 //Проверка  ip - адреса клиента на белый и черный список
            _server.SetASDUHandler(asduHandler, null);                                                                         //Оброботчик запросов от клиента
            _server.SetInterrogationHandler(interrogationHandler, null);                                         //Обработчик команды опроса|Handler for interrogation command (C_IC_NA_1 - 100)|    
            _server.SetCounterInterrogationHandler(counterInterrogationHandler, null);          //Обработчик команды опроса счетчиков|Handler for counter interrogation command (C_CI_NA_1 - 101)|
            _server.SetReadHandler(readHandler, null);                                                                           //Обработчик команды чтения|Handler for read command (C_RD_NA_1 - 102)|
            _server.SetClockSynchronizationHandler(clockSynchronizationHandler, null);          //Обработчик команды синхронизации часов|Handler for clock synchronization command (C_CS_NA_1 - 103)|
            _server.SetResetProcessHandler(resetProcessHandler, null);                                          //Обработчик команды сброса процесса в исходное состояние|Handler for reset process command (C_RP_NA_1 - 105)|
            _server.SetDelayAcquisitionHandler(delayAcquisitionHandler, null);                             //Обработчик команды определения запаздывания|Handler for delay acquisition command (C_CD_NA:1 - 106)|
        }

        public bool ServerStop()
		{
			_server.Stop();
			_workServer = false;
		    return _workServer;
		}
        


		public void AddASDUServer(ASDU asdu)
		{
		    lock (_locker)   
                _server.EnqueueASDU(asdu);
		}
	}
}