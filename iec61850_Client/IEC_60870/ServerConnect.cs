using System.Collections.Generic;
using System.Threading;
using lib60870;

namespace IEC_60870
{
	static class ServerConnect
	{
		private static bool _workServer;
		private static readonly Server _server = new Server();
		private static Thread _serverThread;
		private static readonly object Locker = new object();
		private static readonly EventWaitHandle EventWaitHandle = new AutoResetEvent(false);
		private static readonly Queue<ASDU> QueueASDU = new Queue<ASDU>();
		private static ConnectionParameters _connectionParameters;
		
		public static void ServerQueueSize(int maxQueueSize)
		{
			if(!_workServer)
				_server.MaxQueueSize = maxQueueSize;
		}

		public static void ServerMode(ServerMode serverMode, int maxOpenConnections)
		{
			if (!_workServer)
			{
				_server.ServerMode = serverMode;
				_server.MaxOpenConnections = maxOpenConnections;
			}
		}

		public static void ServerStart()
		{
			_server.Start();
			_workServer = true;
		}
		
		public static void ServerStop()
		{
			_server.Stop();
			_workServer = false;
		}

		public static ConnectionParameters GetConnectionParameters()
		{
			if (!_workServer) return null;
			_connectionParameters = _server.GetConnectionParameters();
			return _connectionParameters;
		}

		public static void AddQueueASDU(ASDU asdu)
		{
			if (_workServer)
			{
				if (_serverThread == null)
				{
					_serverThread = new Thread(ProcessQueue)
					{
						Name = "Server_iec60870",
					};
					_serverThread.Start();
				}
				lock (Locker)
				{
					QueueASDU.Enqueue(asdu);
					EventWaitHandle.Set();
				}
			}
		}

		private static void ProcessQueue()
		{
			while (_workServer)
			{
				ASDU asdu = null;

				lock (Locker)
				{
					if (QueueASDU.Count != 0)
						asdu = QueueASDU.Dequeue();
				}

				if (asdu != null)
					RunServer(asdu);
				else
					EventWaitHandle.WaitOne();
			}
		}

		private static void RunServer(ASDU asdu)
		{
			_server.EnqueueASDU(asdu);
		}
	}
}