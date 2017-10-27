using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Logger
{
    public static class Log
    {
	    private static bool _workLogging;
	    private static readonly EventWaitHandle EventWaitHandle = new AutoResetEvent(false);
	    private static readonly object Locker = new object();
		private static Thread _loggingThread;
		private static readonly Queue<LogMes> QueueLogMes = new Queue<LogMes>();

	    public static void WorkLogging(bool status)
	    {
		    _workLogging = status;
	    }

	    public static void Write(Exception exaption, Code codeLog)
	    {
			if (_workLogging)
			{
				if (_loggingThread == null)
				{
					_loggingThread = new Thread(ProcessQueue)
					{
						Name = "Logger",
						IsBackground = true
					};
					_loggingThread.Start();
				}

				lock (Locker)
				{
					QueueLogMes.Enqueue(new LogMes(exaption, codeLog));
					EventWaitHandle.Set();
				}
			}
	    }

		private static void ProcessQueue()
	    {
			while (_workLogging)
			{
				LogMes temp = null;

				lock (Locker)
				{
					if (QueueLogMes.Count != 0)
						temp = QueueLogMes.Dequeue();

				}

				if (temp != null)
					Message(temp);
				else
					EventWaitHandle.WaitOne();

			}
		}

	    private static void Message(LogMes logMes)
	    {
		    using (StreamWriter w = File.AppendText("log.txt"))
		    {
			    w.Write(logMes.CodeLog == Code.STOP
				    ? "---------------------------------------------------------------------------------------------\n"
				    : $"#{logMes.DateTime}{logMes.DateTime.Millisecond}: {logMes.Exaption.Message} - Code Exaption: {logMes.CodeLog} \n");
			    w.Close();
		    }
	    }

		private class LogMes
		{
			public DateTime DateTime { get; }
			public Exception Exaption { get; }
			public Code CodeLog { get; }

			public LogMes(Exception exaption, Code codeLog)
			{
				DateTime = DateTime.Now;
				Exaption = exaption;
				CodeLog = codeLog;
			}
		}

	    public enum Code
	    {
			STOP = 0,
			SUCCSES = 1,
			WARNING = 2,
			ERROR = 3
	    }
	}
}
