using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Org.GS.Configuration;

namespace Org.GS.Logging
{
  public class LogContext
  {
    private static bool _isInitialized = Initialize();
    public static LogContextState LogContextState {
      get;
      set;
    }
    public static bool DiagnosticLogging {
      get;
      set;
    }
    public static ConfigDbSpec LogConfigDbSpec {
      get;
      set;
    }
    public static ConfigLogSpec ConfigLogSpec {
      get;
      set;
    }

    public static void EstablishContext()
    {
      if (g.AppConfig == null || !g.AppConfig.IsLoaded)
        return;

      string logSpecPrefix = g.CI("ConfigLogSpecPrefix").OrDefault("Default");
      ConfigLogSpec = g.GetLogSpec(logSpecPrefix);

      if (g.AppConfig.ContainsKey("DiagnosticLogging"))
        DiagnosticLogging = g.AppConfig.GetBoolean("DiagnosticLogging");

      string logPath = ConfigLogSpec.LogPath;
      SetLogPath(logPath);

      LogContextState = Logging.LogContextState.Normal;

      new Logger().Flush();
    }

    private static bool Initialize()
    {
      LogContextState = Logging.LogContextState.Initial;
      return true;
    }


    private static void SetLogPath(string appConfigLogPath)
    {
      if (appConfigLogPath.IsNotBlank())
      {
        if (Directory.Exists(appConfigLogPath))
        {
          g.LogPath = appConfigLogPath;
          return;
        }
        else
        {
          try
          {
            Directory.CreateDirectory(appConfigLogPath);
          }
          catch { } // swallow any exceptions

          // if successfully created, then use it
          if (Directory.Exists(appConfigLogPath))
          {
            g.LogPath = appConfigLogPath;
          }
        }
      }

      if (g.LogPath.IsNotBlank())
      {
        if (Directory.Exists(g.LogPath))
        {
          return;
        }
        else
        {
          try
          {
            Directory.CreateDirectory(g.LogPath);
          }
          catch { }

          if (Directory.Exists(g.LogPath))
            return;
        }
      }


      string appDataPath = g.AppDataPath;
      string appDataLogPath = appDataPath + @"\Log";

      if (Directory.Exists(appDataLogPath))
      {
        g.LogPath = appDataLogPath;
        return;
      }
      else
      {
        try
        {
          Directory.CreateDirectory(appDataLogPath);
        }
        catch { } // swallow any exceptions

        // if successfully created, then use it
        if (Directory.Exists(appDataLogPath))
        {
          g.LogPath = appDataLogPath;
          return;
        }

      }

      // if there is still not a log path determined, try to write to "C:\Org\DefaultLogs"

      string lastResortPath = @"C:\Org\DefaultLogs";

      try
      {
        if (!Directory.Exists(lastResortPath))
        {
          Directory.CreateDirectory(lastResortPath);
        }
      }
      catch { } // swallow any exceptions

      if (Directory.Exists(lastResortPath))
      {
        g.LogPath = lastResortPath;
      }
    }
  }

}
