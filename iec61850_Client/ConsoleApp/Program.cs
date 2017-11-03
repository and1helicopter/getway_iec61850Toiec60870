using System;
using System.Collections.Generic;
using System.Threading;
using IEC61850.Common;
using Logger;
using IEC_61850;

namespace ConsoleApp
{
	class Program
	{
		public static async void Loop(List<ClientConnect.PathDA> lol,  string str)
		{
			while (true)
			{
				foreach (var itme in lol)
				{
					dynamic test = await Client.GetValue(str, itme);
					Console.WriteLine($"{str} : {itme.Path} = {test}");
					;
				}

				Thread.Sleep(0);
			}
		}

		static void Main(string[] args)
		{
			try
			{
				Log.WorkLogging(true);

				Log.Write(new Exception("Test1"), Log.Code.WARNING);

				var temp = new ClientConnect();
				temp.DefineConnection("localhost", 102);
				temp.StartConnection();
				temp.FillPathDA();
				Client.AddConnection(temp);

				//var temp2 = new ClientConnect();
				//temp2.DefineConnection("192.168.48.129", 102);
				//temp2.StartConnection();
				//temp2.FillPathDA();
				//Client.AddConnection(temp2);

				List<ClientConnect.PathDA> lol = new List<ClientConnect.PathDA>();
				lol.Add(new ClientConnect.PathDA("ESSrvLD0/CALH1.GrAlm.stVal", FunctionalConstraint.ST, MmsType.MMS_BOOLEAN));
				lol.Add(new ClientConnect.PathDA("ESSrvLD0/CALH1.GrAlm.d", FunctionalConstraint.DC, MmsType.MMS_STRING));
				lol.Add(new ClientConnect.PathDA("ESSrvLD0/CALH1.GrAlm.q", FunctionalConstraint.ST, MmsType.MMS_BIT_STRING));

				Loop(lol, "localhost:102");
			//	Loop(lol, "192.168.48.129:102");

				while (true)
				{
					Thread.Sleep(2500);
				}


				//var test = new ClientConnect();

				//test.DefineConnection("localhost", 102);
				//test.StartConnection();
				//test.FillPathDA();

				//Console.WriteLine("lol");
				//Console.ReadLine();
				//foreach (var item in test.GetListPathDA())
				//{
				//	Console.WriteLine($"{item.Path}   {item.FC}   {item.TypeMms}");
				//}

				//var testPathDA = test.GetListPathDA();
				//var i = 0;

				//while (i < 1)
				//{
				//	foreach (var pathDA in testPathDA)
				//	{
				//		if (pathDA.TypeMms == MmsType.MMS_UTC_TIME)
				//		{
				//			var lol = (Timestamp)test.GetValue(pathDA);
				//			Console.WriteLine(new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime().AddMilliseconds(lol.TimeInMilliseconds));
				//		}
				//		else
				//		{
				//			var lol = test.GetValue(pathDA);
				//			Console.WriteLine(lol);
				//		}
				//	}

				//	i++;
				//	Thread.Sleep(200);
				//}


				Console.ReadLine();

			}
			catch (Exception ex)
			{
				//ex.Message = "Test1";

			}
		}
	}
}
