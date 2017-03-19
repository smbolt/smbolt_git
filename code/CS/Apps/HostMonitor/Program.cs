using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Org.GS;
using Org.GS.Logging;

namespace Org.HostMonitor
{
	public class Program
	{
		private static a a;
		private static int _monitorFrequency;
		private static bool _monitorThreadActive;
		private static bool _continueMonitorThread;
		private static CancellationTokenSource _cancellationTokenSource;
		private static Logger _logger;
		private static DateTime _startDateTime;

		static void Main(string[] args)
		{
			try
			{
				if (!InitializeProgram())
				{
					string message = "Program initialization failed. The program will exit.";
					if (_logger == null)
						_logger = new Logger();
					_logger.Log(message);
					Console.WriteLine(message);
					return;
				}


				_continueMonitorThread = true;
				_cancellationTokenSource = new CancellationTokenSource();
				var task = new Task(() => MonitorHost(), _cancellationTokenSource.Token, TaskCreationOptions.LongRunning);
				task.Start();

				System.Threading.Thread.Sleep(2000); 
				Console.ReadLine();

				Console.WriteLine("Program beginning the termination process." + g.crlf + 
					                "Stopping the main monitoring thread." + g.crlf + 
													"Please wait...");

				_continueMonitorThread = false;
				_cancellationTokenSource.Cancel();

				int waitOnThreadLimit = 10;
				while (task.Status != TaskStatus.Canceled && task.Status != TaskStatus.Faulted && task.Status != TaskStatus.RanToCompletion)
				{
					System.Threading.Thread.Sleep(1000);
					waitOnThreadLimit--;
					if (waitOnThreadLimit < 1)
					{
						string message = "Stopped waiting on monitoring thread to complete after 10 seconds - exiting.";
						_logger.Log(message);
						Console.WriteLine(message); 
						break;
					}
				}

				Console.WriteLine(g.crlf + "The monitoring thread was successfully canceled." + g.crlf + "HostMonitor will now terminate.");
				System.Threading.Thread.Sleep(1500); 

			}
			catch (Exception ex)
			{
				if (_logger == null)
					_logger = new Logger();

				string errorMessage = "An exception occurred in the HostMonitor program." + ex.ToReport();

				_logger.Log(errorMessage);
				Console.WriteLine(errorMessage + g.crlf2); 

				int timeRemaining = 60;

				while (timeRemaining > 1)
				{
					Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
					Console.Write("HostMonitor will automatically close in " + timeRemaining.ToString() + " seconds.    ");
					System.Threading.Thread.Sleep(1000);
					timeRemaining--;
				}
			}
		}

		private static void MonitorHost()
		{
			_monitorThreadActive = true;
			_startDateTime = DateTime.Now;

			Console.SetCursorPosition(0, 2);
			Console.WriteLine(g.BlankString(Console.BufferWidth)); 

			try
			{
				while (_continueMonitorThread)
				{
					if (_cancellationTokenSource.IsCancellationRequested)
					{
						string message = "MonitorHost process has been canceled and will terminate.";
						_logger.Log(message);
						Console.WriteLine(message);
						return;
					}

					var ts = DateTime.Now - _startDateTime;
					string durationMessage = "Host monitoring has been active for " + ts.ToDurationString() + "." + g.crlf + 
						                       "Press any key to terminate.";
					
					Console.SetCursorPosition(0, 3);
					Console.WriteLine(durationMessage  + g.BlankString(20));
					System.Threading.Thread.Sleep(_monitorFrequency);
					_logger.Log("Running for " + ts.ToDurationString() + ".");
				}
			}
			catch (Exception ex)
			{
				_monitorThreadActive = false;
				string errorMessage = "An exception has occurred during host monitoring.  The exception message follows." + g.crlf + ex.ToReport();
				Console.WriteLine(errorMessage);
				Console.WriteLine("The main monitoring thread is exiting."); 

				if (_logger == null)
					_logger = new Logger();
				_logger.Log(errorMessage); 
				_logger.Log("The main monitoring thread is exiting."); 
			}
		}

		private static bool InitializeProgram()
		{
			try
			{ 
				_monitorThreadActive = false;

				Console.WriteLine("HostMonitor is starting on " + DateTime.Now.ToLongDateString() + " at " + DateTime.Now.ToString("HH:mm:ss"));

				_logger = new Logger();
				a = new a();

				_monitorFrequency = g.CI("MonitorFrequency").ToInt32();
				if (_monitorFrequency < 5000)
					_monitorFrequency = 5000; 

				_logger.Log("Program initialization complete.");
				_logger.Log("Monitoring frequency is set to " + _monitorFrequency.ToString("###,##0") + " milliseconds.");

				Console.WriteLine("Monitoring frequency is set to " + _monitorFrequency.ToString("###,##0") + " milliseconds.");
				Console.WriteLine("Press any key to terminate.");

				return true;
			}
			catch (Exception ex)
			{
				string errorMessage = "An exception has occurred during program initialization.  The exception message follows." + g.crlf + ex.ToReport();
				Console.WriteLine(errorMessage);

				if (_logger == null)
					_logger = new Logger();
				_logger.Log(errorMessage); 

				return false;
			}
		}
	}
}
