using System;
using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;
using IEC_61850;
using Logger;
using IEC61850.Common;

namespace Test
{
	[TestFixture]
	public class Tests
	{
		[Test]
		public void Test1()
		{
			Log.WorkLogging(true);
			
			ClientAPI.NewConnection("localhost", 102);
			ClientAPI.StartConnection(ClientAPI.GetClientConnect("localhost:102"));

			List<ClientConnect.PathDA> lol = new List<ClientConnect.PathDA>
			{
				new ClientConnect.PathDA("ESSrvLD0/8DO_GGIO1.SPCSO1.Oper.ctlVal", FunctionalConstraint.CO, MmsType.MMS_BOOLEAN),
				new ClientConnect.PathDA("ESSrvLD0/8DO_GGIO1.SPCSO2.Oper.ctlVal", FunctionalConstraint.CO, MmsType.MMS_BOOLEAN),
				new ClientConnect.PathDA("ESSrvLD0/8DO_GGIO1.SPCSO3.Oper.ctlVal", FunctionalConstraint.CO, MmsType.MMS_BOOLEAN),
				new ClientConnect.PathDA("ESSrvLD0/8DO_GGIO1.SPCSO4.Oper.ctlVal", FunctionalConstraint.CO, MmsType.MMS_BOOLEAN)
			};

			Loop(lol, "localhost:102");
			
			Thread.Sleep(10000);
		}
		
		private static async void Loop(List<ClientConnect.PathDA> lol,  string str)
		{
			var status = false;

			status = !status;
			ClientAPI.SetValue("localhost:102", status, null, 0, lol[0], false, false, false, null, OrCat.PROCESS); //Direct_WithNormalSecurity
			ClientAPI.SetValue("localhost:102", status, null, 0, lol[1], false, false, false, null, OrCat.PROCESS); //Sbo_WithNormalSecurity
			ClientAPI.SetValue("localhost:102", status, null, 0, lol[2], false, false, false, null, OrCat.PROCESS); //Direct_EnhanceadSecurity
			Thread.Sleep(100);	
			var test1 = await ClientAPI.GetValue(str, lol[0]);
			Assert.AreEqual(status, test1);
			dynamic test2 = await ClientAPI.GetValue(str, lol[1]);
			Assert.AreEqual(status, test2);
			dynamic test3 = await ClientAPI.GetValue(str, lol[2]);
			Assert.AreEqual(status, test3);
			
			status = !status;
			ClientAPI.SetValue("localhost:102", status, null, 0, lol[0], false, false, false, null, OrCat.PROCESS); //Direct_WithNormalSecurity
			ClientAPI.SetValue("localhost:102", status, null, 0, lol[1], false, false, false, null, OrCat.PROCESS); //Sbo_WithNormalSecurity
			ClientAPI.SetValue("localhost:102", status, null, 0, lol[2], false, false, false, null, OrCat.PROCESS); //Direct_EnhanceadSecurity
			Thread.Sleep(100);
			test1 = await ClientAPI.GetValue(str, lol[0]);
			Assert.AreEqual(!status, test1);
			test2 = await ClientAPI.GetValue(str, lol[1]);
			Assert.AreEqual(!status, test2);
			test3 = await ClientAPI.GetValue(str, lol[2]);
			Assert.AreEqual(!status, test3);

			Console.WriteLine("end");
			Thread.Sleep(0);
		}
	}
}