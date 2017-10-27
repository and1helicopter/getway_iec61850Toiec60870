using System;
using System.Threading;
using Logger;

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
				Log.Write(new Exception("Test2"), Log.Code.ERROR);
				Log.Write(new Exception("Test3"), Log.Code.SUCCSES);
				Log.Write(new Exception("Test4"), Log.Code.STOP);
				Console.WriteLine("Press any key....");
				Console.ReadLine();


				Log.Write(new Exception("Test5"), Log.Code.WARNING);
				Log.Write(new Exception("Test6"), Log.Code.ERROR);
				Log.Write(new Exception("Test7"), Log.Code.SUCCSES);
				Log.Write(new Exception("Test8"), Log.Code.STOP);
				Console.WriteLine("Press any key....");
				Console.ReadLine();

				Log.Write(new Exception("Test1"), Log.Code.WARNING);
				Log.Write(new Exception("Test2"), Log.Code.ERROR);
				Log.Write(new Exception("Test3"), Log.Code.SUCCSES);
				Log.Write(new Exception("Test4"), Log.Code.ERROR);
				Log.Write(new Exception("Test5"), Log.Code.WARNING);
				Log.Write(new Exception("Test6"), Log.Code.ERROR);
				Log.Write(new Exception("Test7"), Log.Code.SUCCSES);
				Log.Write(new Exception("Test8"), Log.Code.SUCCSES);
				Log.Write(new Exception("Test1"), Log.Code.WARNING);
				Log.Write(new Exception("Test2"), Log.Code.ERROR);
				Log.Write(new Exception("Test3"), Log.Code.SUCCSES);
				Log.Write(new Exception("Test4"), Log.Code.ERROR);
				Log.Write(new Exception("Test5"), Log.Code.WARNING);
				Log.Write(new Exception("Test6"), Log.Code.ERROR);
				Log.Write(new Exception("Test7"), Log.Code.SUCCSES);
				Log.Write(new Exception("Test8"), Log.Code.SUCCSES);
				Log.Write(new Exception("Test1"), Log.Code.WARNING);
				Log.Write(new Exception("Test2"), Log.Code.ERROR);
				Log.Write(new Exception("Test3"), Log.Code.SUCCSES);
				Log.Write(new Exception("Test4"), Log.Code.ERROR);
				Log.Write(new Exception("Test5"), Log.Code.WARNING);
				Log.Write(new Exception("Test6"), Log.Code.ERROR);
				Log.Write(new Exception("Test7"), Log.Code.SUCCSES);
				Log.Write(new Exception("Test8"), Log.Code.SUCCSES);
				Log.Write(new Exception("Test1"), Log.Code.WARNING);
				Log.Write(new Exception("Test2"), Log.Code.ERROR);
				Log.Write(new Exception("Test3"), Log.Code.SUCCSES);
				Log.Write(new Exception("Test4"), Log.Code.ERROR);
				Log.Write(new Exception("Test5"), Log.Code.WARNING);
				Log.Write(new Exception("Test6"), Log.Code.ERROR);
				Log.Write(new Exception("Test7"), Log.Code.SUCCSES);
				Log.Write(new Exception("Test8"), Log.Code.SUCCSES);
				Log.Write(new Exception("Test1"), Log.Code.WARNING);
				Log.Write(new Exception("Test2"), Log.Code.ERROR);
				Log.Write(new Exception("Test3"), Log.Code.SUCCSES);
				Log.Write(new Exception("Test4"), Log.Code.ERROR);
				Log.Write(new Exception("Test5"), Log.Code.WARNING);
				Log.Write(new Exception("Test6"), Log.Code.ERROR);
				Log.Write(new Exception("Test7"), Log.Code.SUCCSES);
				Log.Write(new Exception("Test8"), Log.Code.SUCCSES);
				Log.Write(new Exception("Test1"), Log.Code.WARNING);
				Log.Write(new Exception("Test2"), Log.Code.ERROR);
				Log.Write(new Exception("Test3"), Log.Code.SUCCSES);
				Log.Write(new Exception("Test4"), Log.Code.ERROR);
				Log.Write(new Exception("Test5"), Log.Code.WARNING);
				Log.Write(new Exception("Test6"), Log.Code.ERROR);
				Log.Write(new Exception("Test7"), Log.Code.SUCCSES);
				Log.Write(new Exception("Test8"), Log.Code.SUCCSES);
				Log.Write(new Exception("Test1"), Log.Code.WARNING);
				Log.Write(new Exception("Test2"), Log.Code.ERROR);
				Log.Write(new Exception("Test3"), Log.Code.SUCCSES);
				Log.Write(new Exception("Test4"), Log.Code.ERROR);
				Log.Write(new Exception("Test5"), Log.Code.WARNING);
				Log.Write(new Exception("Test6"), Log.Code.ERROR);
				Log.Write(new Exception("Test7"), Log.Code.SUCCSES);
				Log.Write(new Exception("Test8"), Log.Code.SUCCSES);
				Log.Write(new Exception("Test1"), Log.Code.WARNING);
				Log.Write(new Exception("Test2"), Log.Code.ERROR);
				Log.Write(new Exception("Test3"), Log.Code.SUCCSES);
				Log.Write(new Exception("Test4"), Log.Code.ERROR);
				Log.Write(new Exception("Test5"), Log.Code.WARNING);
				Log.Write(new Exception("Test6"), Log.Code.ERROR);
				Log.Write(new Exception("Test7"), Log.Code.SUCCSES);
				Log.Write(new Exception("Test8"), Log.Code.SUCCSES);
				Log.Write(new Exception("Test1"), Log.Code.WARNING);
				Log.Write(new Exception("Test2"), Log.Code.ERROR);
				Log.Write(new Exception("Test3"), Log.Code.SUCCSES);
				Log.Write(new Exception("Test4"), Log.Code.ERROR);
				Log.Write(new Exception("Test5"), Log.Code.WARNING);
				Log.Write(new Exception("Test6"), Log.Code.ERROR);
				Log.Write(new Exception("Test7"), Log.Code.SUCCSES);
				Log.Write(new Exception("Test8"), Log.Code.SUCCSES);
				Log.Write(new Exception("Test1"), Log.Code.WARNING);
				Log.Write(new Exception("Test2"), Log.Code.ERROR);
				Log.Write(new Exception("Test3"), Log.Code.SUCCSES);
				Log.Write(new Exception("Test4"), Log.Code.ERROR);
				Log.Write(new Exception("Test5"), Log.Code.WARNING);
				Log.Write(new Exception("Test6"), Log.Code.ERROR);
				Log.Write(new Exception("Test7"), Log.Code.SUCCSES);
				Log.Write(new Exception("Test8"), Log.Code.SUCCSES);
				Log.Write(new Exception("Test1"), Log.Code.WARNING);
				Log.Write(new Exception("Test2"), Log.Code.ERROR);
				Log.Write(new Exception("Test3"), Log.Code.SUCCSES);
				Log.Write(new Exception("Test4"), Log.Code.ERROR);
				Log.Write(new Exception("Test5"), Log.Code.WARNING);
				Log.Write(new Exception("Test6"), Log.Code.ERROR);
				Log.Write(new Exception("Test7"), Log.Code.SUCCSES);
				Log.Write(new Exception("Test8"), Log.Code.SUCCSES);
				Log.Write(new Exception("Test1"), Log.Code.WARNING);
				Log.Write(new Exception("Test2"), Log.Code.ERROR);
				Log.Write(new Exception("Test3"), Log.Code.SUCCSES);
				Log.Write(new Exception("Test4"), Log.Code.ERROR);
				Log.Write(new Exception("Test5"), Log.Code.WARNING);
				Log.Write(new Exception("Test6"), Log.Code.ERROR);
				Log.Write(new Exception("Test7"), Log.Code.SUCCSES);
				Log.Write(new Exception("Test8"), Log.Code.SUCCSES);
				Log.Write(new Exception("Test1"), Log.Code.WARNING);
				Log.Write(new Exception("Test2"), Log.Code.ERROR);
				Log.Write(new Exception("Test3"), Log.Code.SUCCSES);
				Log.Write(new Exception("Test4"), Log.Code.ERROR);
				Log.Write(new Exception("Test5"), Log.Code.WARNING);
				Log.Write(new Exception("Test6"), Log.Code.ERROR);
				Log.Write(new Exception("Test1"), Log.Code.WARNING);
				Log.Write(new Exception("Test2"), Log.Code.ERROR);
				Log.Write(new Exception("Test3"), Log.Code.SUCCSES);
				Log.Write(new Exception("Test4"), Log.Code.ERROR);
				Log.Write(new Exception("Test5"), Log.Code.WARNING);
				Log.Write(new Exception("Test6"), Log.Code.ERROR);
				Log.Write(new Exception("Test7"), Log.Code.SUCCSES);
				Log.Write(new Exception("Test8"), Log.Code.SUCCSES);
				Log.Write(new Exception("Test1"), Log.Code.WARNING);
				Log.Write(new Exception("Test2"), Log.Code.ERROR);
				Log.Write(new Exception("Test3"), Log.Code.SUCCSES);
				Log.Write(new Exception("Test4"), Log.Code.ERROR);
				Log.Write(new Exception("Test5"), Log.Code.WARNING);
				Log.Write(new Exception("Test6"), Log.Code.ERROR);
				Log.Write(new Exception("Test7"), Log.Code.SUCCSES);
				Log.Write(new Exception("Test8"), Log.Code.SUCCSES);
				Log.Write(new Exception("Test1"), Log.Code.WARNING);
				Log.Write(new Exception("Test2"), Log.Code.ERROR);
				Log.Write(new Exception("Test3"), Log.Code.SUCCSES);
				Log.Write(new Exception("Test4"), Log.Code.ERROR);
				Log.Write(new Exception("Test5"), Log.Code.WARNING);
				Log.Write(new Exception("Test6"), Log.Code.ERROR);
				Log.Write(new Exception("Testsssssssssssss"), Log.Code.SUCCSES);
				Log.Write(new Exception("Testsssssssssssss"), Log.Code.SUCCSES);
				Log.Write(new Exception("Test8"), Log.Code.STOP);
				Console.WriteLine("Press any key....");
				Console.ReadLine();

				Thread.Sleep(TimeSpan.FromSeconds(3));

			}
			catch (Exception ex)
			{
				//ex.Message = "Test1";

			}
		}
	}
}
