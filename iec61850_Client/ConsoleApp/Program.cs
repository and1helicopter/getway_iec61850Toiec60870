using System;
using System.Collections.Generic;
using System.Threading;
using IEC61850.Common;
using Logger;
using IEC_61850;
using IEC_60870;
using lib60870;

namespace ConsoleApp
{
	class Program
	{
		static void Main(string[] args)
		{
			ServerAPI.ServerMode(ServerMode.SINGLE_REDUNDANCY_GROUP, 1);
			ServerAPI.QueueSize(50);
			ServerAPI.Start();


			var io = new List<InformationObject>
			{
				new SinglePointInformation(10, true, QualityDescriptor.VALID()),
				new SinglePointInformation(11, false, QualityDescriptor.VALID())
			};
			var temp = ServerAPI.newASDU(CauseOfTransmission.INITIALIZED, false, false, 0, 1, false, io);

			var io2 = new List<InformationObject>
			{
				new SinglePointWithCP56Time2a(10, true, QualityDescriptor.VALID(), new CP56Time2a(DateTime.Now.ToUniversalTime())),
				new SinglePointWithCP56Time2a(11, false, QualityDescriptor.VALID(), new CP56Time2a(DateTime.Now.ToUniversalTime()))
			};
			var temp2 = ServerAPI.newASDU(CauseOfTransmission.INITIALIZED, false, false, 1, 2, false, io2);
			
			var io3 = new List<InformationObject>
			{
			//	new MeasuredValueNormalized(40, (float) 0.5, QualityDescriptor.VALID())
				new MeasuredValueNormalizedWithCP56Time2a(30, (float)1.5, QualityDescriptor.VALID(), new CP56Time2a(DateTime.Now.ToUniversalTime())),
				new MeasuredValueNormalizedWithCP56Time2a(35, (float)2.5, QualityDescriptor.VALID(), new CP56Time2a(DateTime.Now.ToUniversalTime()))
			};
			var temp3 = ServerAPI.newASDU(CauseOfTransmission.INITIALIZED, false, false, 2, 3, false, io3);

			while (true)
			{
				//ServerAPI.AddASDU(temp);
				//ServerAPI.AddASDU(temp2);
				ServerAPI.AddASDU(temp3);


				Thread.Sleep(100);
			}
		}
 
//		public static async void Loop(List<ClientConnect.PathDA> lol,  string str)
//		{
//			while (true)
//			{
//				foreach (var itme in lol)
//				{
//					dynamic test = await ClientAPI.GetValue(str, itme);
//					Console.WriteLine($"{str} : {itme.Path} = {test}");
//				}
//				Console.WriteLine("\n");
//				Thread.Sleep(2500);
//			}
//		}
//
//		static void Main(string[] args)
//		{
//			try
//			{
//				Log.WorkLogging(true);
//
//				Log.Write(new Exception("Test1"), Log.Code.WARNING);
//
//				ClientAPI.NewConnection("localhost", 102);
//				ClientAPI.StartConnection(ClientAPI.GetClientConnect("localhost:102"));
//				
//
//				//var temp2 = new ClientConnect();
//				//temp2.DefineConnection("192.168.48.129", 102);
//				//temp2.StartConnection();
//				//temp2.FillPathDA();
//				//Client.AddConnection(temp2);
//
//				List<ClientConnect.PathDA> lol = new List<ClientConnect.PathDA>
//				{
//					new ClientConnect.PathDA("ESSrvLD0/8DO_GGIO1.SPCSO1.Oper.ctlVal", FunctionalConstraint.CO, MmsType.MMS_BOOLEAN),
//					new ClientConnect.PathDA("ESSrvLD0/8DO_GGIO1.SPCSO2.Oper.ctlVal", FunctionalConstraint.CO, MmsType.MMS_BOOLEAN),
//					new ClientConnect.PathDA("ESSrvLD0/8DO_GGIO1.SPCSO3.Oper.ctlVal", FunctionalConstraint.CO, MmsType.MMS_BOOLEAN),
//					new ClientConnect.PathDA("ESSrvLD0/8DO_GGIO1.SPCSO4.Oper.ctlVal", FunctionalConstraint.CO, MmsType.MMS_BOOLEAN)
//				};
//
//
//				Loop(lol, "localhost:102");
//				//	Loop(lol, "192.168.48.129:102");
//				foreach (var x in ClientAPI.GetPathDA(ClientAPI.GetClientConnect("localhost:102")))
//				{
//					Console.WriteLine($"{x.Path} : {x.FC} : {x.TypeMms}");
//				}
//				var status = false;
//				while (true)
//				{
//					status = !status;
//					ClientAPI.SetValue("localhost:102", status, null, 0, lol[0], false, false, false, null, OrCat.PROCESS); //Direct_WithNormalSecurity
//					ClientAPI.SetValue("localhost:102", status, null, 0, lol[1], false, false, false, null, OrCat.PROCESS); //Sbo_WithNormalSecurity
//					ClientAPI.SetValue("localhost:102", status, null, 0, lol[2], false, false, false, null, OrCat.PROCESS); //Direct_EnhanceadSecurity
//					ClientAPI.SetValue("localhost:102", status, !status, 0, lol[3], false, false, false, "test", OrCat.PROCESS); //Sbo_EnhanceadSecurity
//
//					Thread.Sleep(2500);
//				}
//			}
//			catch (Exception ex)
//			{
//				//ex.Message = "Test1";
//
//			}
//		}
	}
}
