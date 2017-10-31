using System;
using System.Threading;
using Logger;
using IEC_61850;

namespace ConsoleApp
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				Log.WorkLogging(true);

				Log.Write(new Exception("Test1"), Log.Code.WARNING);

				var test = new ClientConnect();

				test.DefineConnection("localhost",102);
				test.StartConnection();
				test.FillPathDA();

				Console.ReadLine();
			}
			catch (Exception ex)
			{
				//ex.Message = "Test1";

			}
		}
	}
}
