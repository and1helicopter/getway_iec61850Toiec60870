using System;
using System.Collections.Generic;
using IEC_60870;
using lib60870;

namespace IEC_60870
{
	public static class ServerAPI
	{
		public static void Start()
		{
			ServerConnect.ServerStart();
		}

		public static void Stop()
		{
			ServerConnect.ServerStop();
		}

		public static void AddASDU(ASDU asdu)
		{
			ServerConnect.AddQueueASDU(asdu);
		}

		public static void QueueSize(int count)
		{
			ServerConnect.ServerQueueSize(count);
		}

		public static void ServerMode(ServerMode serverMode, int maxOpenConnections)
		{
			ServerConnect.ServerMode(serverMode, maxOpenConnections);
		}
		
		//Одноэлементная информация
		public static ASDU newASDU(CauseOfTransmission cot, bool isTest, bool isNegative, byte oa, int ca, bool isSequence, List<InformationObject> io) 
		{
			var temp = new ASDU(ServerConnect.GetConnectionParameters(), cot, isTest, isNegative, oa,ca, isSequence);
			foreach (var item in io)
			{
				temp.AddInformationObject(item);
			}
			return temp;
		}
	}
}