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
					//Console.WriteLine($"{str} : {itme.Path} = {test}");
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

				Client.NewConnection("localhost", 102);
				Client.StartConnection(Client.GetClientConnect("localhost:102"));

				//var temp2 = new ClientConnect();
				//temp2.DefineConnection("192.168.48.129", 102);
				//temp2.StartConnection();
				//temp2.FillPathDA();
				//Client.AddConnection(temp2);

				List<ClientConnect.PathDA> lol = new List<ClientConnect.PathDA>();
				lol.Add(new ClientConnect.PathDA("ESSrvLD0/CALH1.GrAlm.stVal", FunctionalConstraint.ST, MmsType.MMS_BOOLEAN));
				lol.Add(new ClientConnect.PathDA("ESSrvLD0/CALH1.GrAlm.d", FunctionalConstraint.DC, MmsType.MMS_STRING));
				lol.Add(new ClientConnect.PathDA("ESSrvLD0/CALH1.GrAlm.q", FunctionalConstraint.ST, MmsType.MMS_BIT_STRING));
				lol.Add(new ClientConnect.PathDA("ESSrvLD0/8DO_GGIO1.SPCSO1.stVal", FunctionalConstraint.ST, MmsType.MMS_BOOLEAN));
				lol.Add(new ClientConnect.PathDA("ESSrvLD0/8DO_GGIO1.SPCSO1.Oper.ctlVal", FunctionalConstraint.CO, MmsType.MMS_BOOLEAN));
				lol.Add(new ClientConnect.PathDA("ESSrvLD0/8DO_GGIO1.SPCSO2.Oper.ctlVal", FunctionalConstraint.CO, MmsType.MMS_BOOLEAN));
				lol.Add(new ClientConnect.PathDA("ESSrvLD0/8DO_GGIO1.SPCSO3.Oper.ctlVal", FunctionalConstraint.CO, MmsType.MMS_BOOLEAN));
				lol.Add(new ClientConnect.PathDA("ESSrvLD0/8DO_GGIO1.SPCSO4.Oper.ctlVal", FunctionalConstraint.CO, MmsType.MMS_BOOLEAN));


				Loop(lol, "localhost:102");
				//	Loop(lol, "192.168.48.129:102");
				foreach (var x in Client.GetPathDA(Client.GetClientConnect("localhost:102")))
				{
					Console.WriteLine($"{x.Path} : {x.FC} : {x.TypeMms}");
				}
				var status = false;
				while (true)
				{
					status = !status;
					//Client.SetValue("localhost:102", status, null, 0, lol[4], false, false, false, null, OrCat.PROCESS); //Direct_WithNormalSecurity
					//Client.SetValue("localhost:102", status, null, 0, lol[5], false, false, false, null, OrCat.PROCESS); //Sbo_WithNormalSecurity
					//Client.SetValue("localhost:102", status, null, 0, lol[6], false, false, false, null, OrCat.PROCESS); //Direct_EnhanceadSecurity
					Client.SetValue("localhost:102", status, !status, 0, lol[7], false, false, false, "test", OrCat.PROCESS); //Sbo_EnhanceadSecurity

					Thread.Sleep(2500);
				}
			}
			catch (Exception ex)
			{
				//ex.Message = "Test1";

			}
		}
	}
}
