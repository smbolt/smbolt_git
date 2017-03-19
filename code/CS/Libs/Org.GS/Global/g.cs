using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;
using System.Reflection;
using System.Resources;
using System.Windows;
using System.Windows.Forms;
using Org.GS.Notifications;
using Org.GS.Configuration;
using Org.GS.Logging;
using Org.GS.Dynamic;
using Org.GS;

namespace Org.GS
{
  public static class g
  {
    private static bool IsInitialized = Initialize();
    public static string Ruler;
    private static Cache _cache;
    public static Cache Cache { get { return _cache; } }
    public static SystemInfo SystemInfo; 
    public static object UtilityLockObject = new object();
    public static Vault Vault;
    public static bool IsVaultLoadedFromFile = false;
    public static DiagnosticsManager DiagnosticsManager = new DiagnosticsManager();
    public static AppInfo AppInfo;
    public static string ConnectionStringName = String.Empty;
    public static LogRecordSet LogRecordSet;
    public static bool UseLocalAppData { get; set; }
    public static bool IsXmlMapperInitialized { get; set; }
    public static ConfigSmtpSpec DefaultConfigSmtpSpec { get; set; }
    public static ObjectMap ObjectMap;
    public static AppConfig AppConfig;
    public static AppConfig ModuleConfig;
    public static bool IsModuleConfigLoaded = false;
    public static Org.GS.Dynamic.Module Module = null;
    public static Assembly RootAssembly = null;
    public static Assembly ModuleAssembly = null;

    public static bool SuppressLogging = false;
    public static bool InOrgAdminMode = false;
    public static bool InClientAdminMode = false;
    public static bool IsDomainOrg = false;
    public static bool IsInVisualStudioDesigner;
    public static bool UseStartupLogging;
    public static bool IsModuleHost;
    public static bool UseInitialConfig = false;
    public static bool AttemptConfigImport = false;
    public static string DbServerName = String.Empty;
    public static string DomainName = String.Empty;
    public static string InitConfig = String.Empty;
    public static string IpAddress = String.Empty;
    public static int TimeoutMinutes = 30;

    public static string ExecutablePath { get; set; }
    public static string AbsoluteExecutablePath { get; set; }
    public static string ProgramDataPath { get; set; }
    public static string AppConfigPath { get; set; }
    public static string CentralConfigPath { get; set; }
    public static string LocalConfigPath { get; set; }
    public static string LogPath { get; set; }
    public static string ResourcePath { get; set; }
    public static string ReportsPath { get; set; }
    public static string ImportsPath { get; set; }
    public static string ExportsPath { get; set; }
    public static string ErrorsPath { get; set; }
    public static string MEFCatalog { get; set; }
    public static string DocPath { get; set; }
    public static string HelpPath { get; set; }
    public static string PerformancePath { get; set; }
    public static string CS1;
    public static string crlf = Environment.NewLine;
    public static string nl = g.crlf;
    public static string nl2 = g.crlf + g.crlf;
    public static string crlf2 = g.crlf + g.crlf;
    public static string br = @"<br/>";
    public static string br2 = @"<br/><br/>";
    public static string PadWork = null;
    public static string PadZero = null;
    public static string Trace = String.Empty;
    public static string Report = String.Empty;
    public static FxVersionSet FxVersionSet; 
    public static int MemoryLogLimit = 250000;
    public static int MemoryLogTruncateSize = 200000;
    public static int MemoryLogCount = 0; 
    public static Color ThemeExtraLightColor = Color.White;
    public static Color ThemeLightColor = Color.White;
    public static Color ThemeMidLightColor = Color.White;
    public static Color ThemeBaseColor = Color.White;
    public static Color ThemeMidDarkColor = Color.White;
    public static Color ThemeDarkColor = Color.White;
    public static Color ThemeExtraDarkColor = Color.White;
    public static Color ThemePanelBackgroundColor = Color.White;
    public static Color ThemePanelHeaderColor = Color.White;

    private static StringBuilder _memoryLog = new StringBuilder();
    public static string MemoryLog
    {
      get { return _memoryLog.ToString(); }
    }
  
    public static bool DriveInitialization()
    {
      return true;
    }

    public static DiagnosticsMode DiagnosticsMode
    {
      get { return DiagnosticsManager.DiagnosticsMode; } 
    }

    public static bool DebugOrVerbose
    {
      get { return DiagnosticsManager.DiagnosticsMode > 0; }
    }

    public static bool Verbose
    {
      get { return DiagnosticsManager.DiagnosticsMode == DiagnosticsMode.Verbose; }
    }

    public static string AppDataPath
    {
      get 
      {
        string appDataPath = Get_AppDataPath();
        if (appDataPath.IsBlank())
        {
          if (g.IsWebBasedApplication)
            throw new Exception("Cannot determine AppDataPath for web based application.");
          else
            throw new Exception("Insufficient data in LocalConfig.xmlx to determine AppDataPath.  Vendor, AppType or AppName are missing or blank.");
        }
        return appDataPath;
      }
    }

    private static LocalConfig _localConfig;
    public static LocalConfig LocalConfig
    {
      get
      {
        if (_localConfig == null)
        {
          _localConfig = new LocalConfig();
        }
        return _localConfig;
      }
    }

    public static void ClearMemoryLog()
    {
      _memoryLog = new StringBuilder();
      MemoryLogCount = 0;
    }

    public static bool IsWebBasedApplication
    {
      get { return Get_IsWebBasedApplication(); }
    }

    public static bool Initialize()
    {
      try
      {
        _cache = new Cache();
        SystemInfo = new SystemInfo();
        FxVersionSet = new FxVersionSet();

        IsInVisualStudioDesigner = AssemblyHelper.IsInVisualStudioDesigner;
        AppInfo = new AppInfo();
        if (LogRecordSet == null)
            LogRecordSet = new LogRecordSet();
        IsXmlMapperInitialized = InitializeXmlMapper();
        Vault = GetVault();

        _localConfig = new LocalConfig();

        if (_localConfig.ContainsKey("StartupLogKeepFiles"))
          StartupLogging.StartupLogKeepFiles = _localConfig.GetInteger("StartupLogKeepFiles");

        ProgramDataPath = String.Empty;
        AppConfigPath = String.Empty;
        CentralConfigPath = String.Empty;
        LogPath = String.Empty;
        ResourcePath = String.Empty;
        ReportsPath = String.Empty;
        ImportsPath = String.Empty;
        ExportsPath = String.Empty;
        ErrorsPath = String.Empty;
        MEFCatalog = String.Empty;
        DocPath = String.Empty;
        HelpPath = String.Empty;
        PerformancePath = String.Empty;

        Ruler = GetRuler();

        return true;
      }
      catch(Exception ex) 
      {
        throw new Exception("An exception occurred initializing the 'a' (application) object.", ex); 
      }
    }

    public static string GetRuler()
    {
      return "0----+----1----+----2----+----3----+----4----+----5----+----6----+----7----+----8----+----9----+----" +
             "0----+----1----+----2----+----3----+----4----+----5----+----6----+----7----+----8----+----9----+----" + 
             "0----+----1----+----2----+----3----+----4----+----5----+----6----+----7----+----8----+----9----+----";
    }

    private static object LogToMemory_LockObject = new object();
    public static void LogToMemory(string text)
    {
      if (Monitor.TryEnter(LogToMemory_LockObject, 5000))
      {
        try
        {
          if (_memoryLog == null)
            _memoryLog = new StringBuilder();

          string leadingNewLine = String.Empty;
          if (text.StartsWith("^"))
          {
            leadingNewLine = g.crlf;
            text = text.Substring(1); 
          }

          MemoryLogCount++; 

          string logRecord = leadingNewLine + DateTime.Now.ToString("yyyyMMdd-HHmmss.fff") + " " +
                             "TH:" + System.Threading.Thread.CurrentThread.ManagedThreadId.ToString("00000") + " " + MemoryLogCount.ToString("00000") + " "
                             + text + g.crlf; 

          _memoryLog.Append(logRecord);

          // if over 250K (default, but overridable) bytes, trim (keeping newest) to ~200K (default, but overridable)
          if (_memoryLog.Length > MemoryLogLimit)
          {
            if (MemoryLogTruncateSize > MemoryLogLimit * 0.8)
              MemoryLogTruncateSize = Convert.ToInt32(MemoryLogLimit * 0.8);

            int charsToTrim = MemoryLogLimit - MemoryLogTruncateSize;

            string truncatedLog = _memoryLog.ToString();
            _memoryLog = new StringBuilder();
            int truncatePoint = truncatedLog.IndexOf(g.crlf, charsToTrim);
            if (truncatePoint != -1)
              _memoryLog.Append(truncatedLog.Substring(truncatePoint + 2));
            else
              _memoryLog.Append(truncatedLog.Substring(charsToTrim));
          }
        }
        catch{}
        finally
        {
          Monitor.Exit(LogToMemory_LockObject);
        }
      }
    }

    private static string Get_AppDataPath()
    {
      if (ProgramDataPath == null)
        return String.Empty;

      if (g.AppInfo.Vendor == null)
        return String.Empty;

      if (g.AppInfo.AppName == null)
        return String.Empty;

      if (ProgramDataPath.IsBlank() || g.AppInfo.Vendor.IsBlank() || g.AppInfo.AppName.IsBlank())
        return String.Empty;

      string programTypeFolder = String.Empty;

      switch (g.AppInfo.OrgApplicationType)
      {
        case ApplicationType.WinApp:
        case ApplicationType.WpfApp:
        case ApplicationType.WinConsole:
        case ApplicationType.WinAppModule:
        case ApplicationType.WpfAppModule:
          programTypeFolder = "Apps";
          break;

        case ApplicationType.WinService:
          programTypeFolder = "WinServices";
          break;

        case ApplicationType.WebSite:
          programTypeFolder = "WebSites";
          break;

        case ApplicationType.WebApi:
          programTypeFolder = "WebApi";
          break;

        case ApplicationType.WcfService:
          programTypeFolder = "WcfServices";
          break;
      }

        if (programTypeFolder.IsBlank())
          return String.Empty;

        string appDataPath = String.Empty;

        switch (g.AppInfo.OrgApplicationType)
        {
          case ApplicationType.WebSite:
          case ApplicationType.WcfService:
            appDataPath = ProgramDataPath;
            break;

          case ApplicationType.WinApp:
          case ApplicationType.WpfApp:
          case ApplicationType.WinService:
          case ApplicationType.WebApi:
          case ApplicationType.WinConsole:
          case ApplicationType.WinAppModule:
          case ApplicationType.WpfAppModule:
            if (g.UseLocalAppData)
              appDataPath = ProgramDataPath + @"\AppData";
            else
              appDataPath = ProgramDataPath + @"\" + g.AppInfo.Vendor + @"\" + programTypeFolder + @"\" + g.AppInfo.AppName + @"\AppData";
            break;

          default:
            throw new Exception("Cannot determine g.AppDataPath because the application type '" + g.AppInfo.OrgApplicationType.ToString() + "' is invalid."); 
        }


        return appDataPath; 
    }

    public static string EnvPrefix
    {
      get { return GetEnvPrefix(); }
    }

    [DebuggerStepThrough]
    public static Vault GetVault()
    {
      Vault v = new Vault();
      Assembly assembly = Assembly.GetExecutingAssembly();
      ResourceManager resourceManager = new ResourceManager("Org.GS.Resource1", assembly);
      object o = resourceManager.GetObject("config");
      byte[] vaultBytes = (byte[])o;
      string s = System.Text.Encoding.UTF8.GetString(vaultBytes);
      v.LoadFromString(s);
      return v;
    }

    [DebuggerStepThrough]
    public static bool InitializeXmlMapper()
    {
      XmlMapper.Load();
      return true;
    }

    [DebuggerStepThrough]
    public static string GetCI(string ciName)
    {
      if (AppConfig == null)
        return String.Empty;

      if (AppConfig.ContainsKey(ciName))
        return AppConfig.GetCI(ciName);

      return String.Empty;
    }

    [DebuggerStepThrough]
    public static List<string> GetList(string listName)
    {
      if (AppConfig == null)
        return new List<string>();

      if (AppConfig.ContainsList(listName))
        return AppConfig.GetList(listName);

      return new List<string>();
    }

    [DebuggerStepThrough]
    public static Dictionary<string, string> GetDictionary(string dictName)
    {
      if (AppConfig == null)
        return new Dictionary<string, string>();

      if (AppConfig.ContainsDictionary(dictName))
        return AppConfig.GetDictionary(dictName);

      return new Dictionary<string, string>();
    }

    [DebuggerStepThrough]
    public static List<int> GetIntList(string listName)
    {
      if (AppConfig == null)
        return new List<int>();

      if (AppConfig.ContainsList(listName))
        return AppConfig.GetIntList(listName);

      return new List<int>();
    }

    [DebuggerStepThrough]
    public static string GetCI(string ciName, string defaultValue)
    {
      if (AppConfig == null)
        return defaultValue;

      if (AppConfig.ContainsKey(ciName))
        return AppConfig.GetCI(ciName);

      return defaultValue;
    }

    [DebuggerStepThrough]
    public static string BlankString(int length)
    {
      StringBuilder sb = new StringBuilder();
      for (int i = 0; i < length; i++)
        sb.Append(" ");

      return sb.ToString();
    }

    [DebuggerStepThrough]
    public static bool? GetNullableBooleanValue(object o)
    {
      if (o == null)
        return (bool?)null;

      string stringValue = o.ToString().Trim();

      if (stringValue.Length == 0)
        return (bool?)null;

      return GetBooleanValue(o); 
    }

    [DebuggerStepThrough]
    public static bool GetBooleanValue(object o)
    {
      if (o == null)
        return false;
            
      string stringValue = o.ToString();
      bool returnValue = false;

      if (Boolean.TryParse(stringValue, out returnValue))
        return returnValue;

      return false;
    }

    [DebuggerStepThrough]
    public static int? GetNullableInt32Value(object o)
    {
      if (o == null)
        return (int?)null;

      string stringValue = o.ToString().Trim();

      if (stringValue.Length == 0)
        return (int?)null;

      return GetInt32Value(o);
    }

    [DebuggerStepThrough]
    public static int GetInt32Value(object o)
    {
      if (o == null)
        return 0;
            
      string stringValue = o.ToString();
      int returnValue = 0;

      if (Int32.TryParse(stringValue, out returnValue))
        return returnValue;

      return 0;
    }

    [DebuggerStepThrough]
    public static Int64? GetNullableInt64Value(object o)
    {
      if (o == null)
        return (Int64?) null;

      string stringValue = o.ToString().Trim();
      if (stringValue.Length == 0)
        return (Int64?)null;

      return GetInt64Value(o);
    }

    [DebuggerStepThrough]
    public static Int64 GetInt64Value(object o)
    {
      if (o == null)
        return 0;
            
      string stringValue = o.ToString();
      Int64 returnValue = 0;

      if (Int64.TryParse(stringValue, out returnValue))
        return returnValue;

      return 0;
    }

    [DebuggerStepThrough]
    public static float? GetNullableFloatValue(object o)
    {
      if (o == null)
        return (float?)null;

      string stringValue = o.ToString();
      if (stringValue.Length == 0)
        return (float?)null;

      return GetFloatValue(o); 
    }

    [DebuggerStepThrough]
    public static float GetFloatValue(object o)
    {
      if (o == null)
        return 0F;

      try
      {
        return (float) Convert.ToDecimal(o);
      }
      catch
      {
        return 0F;
      }
    }

    [DebuggerStepThrough]
    public static decimal? GetNullableDecimalValue(object o)
    {
      if (o == null)
        return (decimal?)null;

      string stringValue = o.ToString();
      if (stringValue.Length == 0)
        return (decimal?)null;

      return GetDecimalValue(o); 
    }

    [DebuggerStepThrough]
    public static decimal GetDecimalValue(object o)
    {
      if (o == null)
        return 0M;

      try
      {
        return Convert.ToDecimal(o);
      }
      catch
      {
        return 0M;
      }
    }

    [DebuggerStepThrough]
    public static Single? GetNullableSingleValue(object o)
    {
      if (o == null)
        return (Single?)null;

      string stringValue = o.ToString().Trim();
      if (stringValue.Length == 0)
        return (Single?)null;

      return GetSingleValue(o); 
    }

    [DebuggerStepThrough]
    public static Single GetSingleValue(object o)
    {
      if (o == null)
        return 0;
            
      string stringValue = o.ToString();
      Single returnValue = 0;

      if (Single.TryParse(stringValue, out returnValue))
        return returnValue;

      return 0;
    }

    [DebuggerStepThrough]
    public static Point GetPointValue(object o)
    {
      if (o == null)
        return new Point(0, 0); 
            
      string stringValue = o.ToString();
      Point returnValue = new Point(0, 0);

      if (o.GetType().Name == "String")
      {
        string inValue = o.ToString().Trim();
        if (inValue.CountOfChar(',') == 1)
        {
          string[] tokens = inValue.Split(',');
          if (tokens.Length == 2)
          {
            if (tokens[0].Replace("-", String.Empty).IsNumeric() && tokens[1].Replace("-", String.Empty).IsNumeric())
            {
              returnValue = new Point(tokens[0].ToInt32(), tokens[1].ToInt32());
            }
          }
        }
      }

      return returnValue;
    }

    [DebuggerStepThrough]
    public static PointF GetPointFValue(object o)
    {
      if (o == null)
        return new PointF(0.0F, 0.0F); 
            
      string stringValue = o.ToString();
      PointF returnValue = new PointF(0.0F, 0.0F); 

      if (o.GetType().Name == "String")
      {
        string inValue = o.ToString().Trim();
        if (inValue.CountOfChar(',') == 1)
        {
          string[] tokens = inValue.Split(',');
          if (tokens.Length == 2)
          {
            if (tokens[0].IsFloat() && tokens[1].IsFloat())
            {
              returnValue = new PointF((float)Decimal.Parse(tokens[0]), (float)Decimal.Parse(tokens[1]));
            }
          }
        }
      }

      return returnValue;
    }

    [DebuggerStepThrough]
    public static Size GetSizeValue(object o)
    {
      if (o == null)
        return new Size(0, 0); 
            
      string stringValue = o.ToString();
      Size returnValue = new Size(0, 0);

      if (o.GetType().Name == "String")
      {
        string inValue = o.ToString().Trim();
        if (inValue.CountOfChar(',') == 1)
        {
          string[] tokens = inValue.Split(',');
          if (tokens.Length == 2)
          {
            if (tokens[0].IsNumeric() && tokens[1].IsNumeric())
            {
              returnValue = new Size(Int32.Parse(tokens[0]), Int32.Parse(tokens[1]));
            }
          }
        }
      }

      return returnValue;
    }

    [DebuggerStepThrough]
    public static SizeF GetSizeFValue(object o)
    {
      if (o == null)
        return new SizeF(0.0F, 0.0F); 
            
      string stringValue = o.ToString();
      SizeF returnValue = new SizeF(0.0F, 0.0F); 

      if (o.GetType().Name == "String")
      {
        string inValue = o.ToString().Trim();
        if (inValue.CountOfChar(',') == 1)
        {
          string[] tokens = inValue.Split(',');
          if (tokens.Length == 2)
          {
            if (tokens[0].IsFloat() && tokens[1].IsFloat())
            {
              returnValue = new SizeF((float) Decimal.Parse(tokens[0]), (float) Decimal.Parse(tokens[1]));
            }
          }
        }
      }

      return returnValue;
    }

    //[DebuggerStepThrough]
    public static Color GetColorValue(object o)
    {
      if (o == null)
        return Color.White;
            
      string stringValue = o.ToString();

      Color color = Color.FromName(stringValue); 

      if (stringValue.StartsWith("SystemColors."))
        color = GetSystemColorFromName(stringValue);
      else
        color = Color.FromName(stringValue); 

      return color;
    }

    public static Color GetSystemColorFromName(string name)
    {
      switch(name)
      {
        case "SystemColors.ActiveBorder" : return SystemColors.ActiveBorder;
        case "SystemColors.ActiveCaption" : return SystemColors.ActiveCaption;
        case "SystemColors.ActiveCaptionText" : return SystemColors.ActiveCaptionText;
        case "SystemColors.AppWorkspace" : return SystemColors.AppWorkspace;
        case "SystemColors.ButtonFace" : return SystemColors.ButtonFace;
        case "SystemColors.ButtonHighlight" : return SystemColors.ButtonHighlight;
        case "SystemColors.ButtonShadow" : return SystemColors.ButtonShadow;
        case "SystemColors.Control" : return SystemColors.Control;
        case "SystemColors.ControlDark" : return SystemColors.ControlDark;
        case "SystemColors.ControlDarkDark" : return SystemColors.ControlDarkDark;
        case "SystemColors.ControlLight" : return SystemColors.ControlLight;
        case "SystemColors.ControlLightLight" : return SystemColors.ControlLightLight;
        case "SystemColors.ControlText" : return SystemColors.ControlText;
        case "SystemColors.Desktop" : return SystemColors.Desktop;
        case "SystemColors.GradientActiveCaption" : return SystemColors.GradientActiveCaption;
        case "SystemColors.GradientInactiveCaption" : return SystemColors.GradientInactiveCaption;
        case "SystemColors.GrayText" : return SystemColors.GrayText;
        case "SystemColors.Highlight" : return SystemColors.Highlight;
        case "SystemColors.HighlightText" : return SystemColors.HighlightText;
        case "SystemColors.HotTrack" : return SystemColors.HotTrack;
        case "SystemColors.InactiveBorder" : return SystemColors.InactiveBorder;
        case "SystemColors.InactiveCaption" : return SystemColors.InactiveCaption;
        case "SystemColors.InactiveCaptionText" : return SystemColors.InactiveCaptionText;
        case "SystemColors.Info" : return SystemColors.Info;
        case "SystemColors.InfoText" : return SystemColors.InfoText;
        case "SystemColors.Menu" : return SystemColors.Menu;
        case "SystemColors.MenuBar" : return SystemColors.MenuBar;
        case "SystemColors.MenuHighlight" : return SystemColors.MenuHighlight;
        case "SystemColors.MenuText" : return SystemColors.MenuText;
        case "SystemColors.ScrollBar" : return SystemColors.ScrollBar;
        case "SystemColors.Window" : return SystemColors.Window;
        case "SystemColors.WindowFrame" : return SystemColors.WindowFrame;
        case "SystemColors.WindowText" : return SystemColors.WindowText;
      }

      return Color.White;
    }

    [DebuggerStepThrough]
    public static DateTime? GetNullableDateTimeValue(object o, string format)
    {
      if (o == null)
        return (DateTime?)null;

      if (o.ToString().IsBlank())
        return (DateTime?)null;

      return GetDateTimeValue(o, format);
    }

    //[DebuggerStepThrough]
    public static DateTime GetDateTimeValue(object o, string format)
    {
      if (o == null)
        return DateTime.MinValue;
            
      string stringValue = o.ToString();
      DateTime returnValue = DateTime.MinValue;

      if (format.IsBlank())
      {
        if (DateTime.TryParse(stringValue, out returnValue))
          return returnValue; 
      }
      else
      {
        if (format.StartsWith("["))
        {
          if (format.CountOfChar('*') != 1)
            throw new Exception("Illegal date format specified (3) '" + format + "' must contain exactly one asterisk.");
          int endPos = format.IndexOf(']');
          if (endPos == -1)
            throw new Exception("Illegal date format specified (1) '" + format + "'.");
          string literalSuppliedValue = format.Substring(1, endPos - 1);
          if (format.Length < endPos + 1)
            throw new Exception("Illegal date format specified (2) '" + format + "'.");
          format = format.Substring(endPos + 1);
          if (literalSuppliedValue.CountOfChar('*') != 1)
            throw new Exception("Illegal date format specified (3) '" + format + "' must contain exactly one asterisk.");
          stringValue = literalSuppliedValue.Replace("*", stringValue);
        }

        if (DateTime.TryParseExact(stringValue, format, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out returnValue))
          return returnValue;
      }

      return returnValue;
    }

    [DebuggerStepThrough]
    public static DateTime GetDateTimeValue(object o)
    {
      if (o == null)
        return DateTime.MinValue;
            
      string stringValue = o.ToString();
      DateTime returnValue = DateTime.MinValue;

      if (DateTime.TryParse(stringValue, out returnValue))
        return returnValue; 
     
      return returnValue;
    }

    [DebuggerStepThrough]
    public static string ApplyDateTimeFormat(DateTime dt, string format)
    {
      if (format.IsBlank())
      {
        return dt.ToString(); 
      }
      else
      {
        if (format.StartsWith("["))
        {
          if (format.CountOfChar('*') != 1)
            throw new Exception("Illegal date format specified (3) '" + format + "' must contain exactly one asterisk."); 
          int endPos = format.IndexOf(']');
          if (endPos == -1)
            throw new Exception("Illegal date format specified (1) '" + format + "'."); 
          string literalSuppliedValue = format.Substring(1, endPos - 1);
          if (format.Length < endPos + 1)
            throw new Exception("Illegal date format specified (2) '" + format + "'."); 
          format = format.Substring(endPos + 1);
          if (literalSuppliedValue.CountOfChar('*') != 1)
            throw new Exception("Illegal date format specified (3) '" + format + "' must contain exactly one asterisk.");

          string rawFormattedDate = dt.ToString(format);
          string literalSuppliedNoAsterisk = literalSuppliedValue.Replace("*", String.Empty).Trim();

          if (rawFormattedDate.Contains(literalSuppliedNoAsterisk))
          {
            string returnValue = rawFormattedDate.Replace(literalSuppliedNoAsterisk, String.Empty).Trim();
            return returnValue;
          }
        }
      }

      return dt.ToString();
    }

    //[DebuggerStepThrough]
    public static TimeSpan? GetNullableTimeSpanValue(object o)
    {
      if (o == null)
        return (TimeSpan?)null;

      if (o.ToString().IsBlank())
        return (TimeSpan?)null;

      return GetTimeSpanValue(o);
    }

    //[DebuggerStepThrough]
    public static TimeSpan? GetTimeSpanValue(object o)
    {
      TimeSpan ts = new TimeSpan();

      // only time span values in format "00:00" with max value of "23:59" will be acceptable, all else will return default "zero" time span

      if (o == null)
        return ts;

      string tsString = o.ToString().Trim();
      if (tsString.Length != 5)
        return ts;

      string tsHour = tsString.Substring(0, 2);
      string colon = tsString.Substring(2, 1);
      string tsMinute = tsString.Substring(3, 2);

      if (colon != ":" || tsHour.IsNotNumeric() || tsMinute.IsNotNumeric())
        return ts;

      int hours = Int32.Parse(tsHour);
      int minutes = Int32.Parse(tsMinute);

      return new TimeSpan(hours, minutes, 0);
    }

    [DebuggerStepThrough]
    public static bool AllStringsNonBlank(string[] strings)
    {
      if (strings == null)
        return false;

      if (strings.Length == 0)
        return false;

      foreach (string s in strings)
      {
        if (s == null)
          return false;

        if (s.Trim().Length == 0)
          return false;
      }

      return true;
    }

    //[DebuggerStepThrough]
    public static ConfigDbSpec GetDbSpec(string prefix)
    {
      string specName = g.EnvPrefix + prefix;
      ConfigDbSpec spec = (ConfigDbSpec)g.AppConfig.GetCO<ConfigDbSpec>(specName);
      return spec;
    }

    //[DebuggerStepThrough]
    public static ConfigNotifySpec GetNotifySpec(string prefix)
    {
      string specName = g.EnvPrefix + prefix;
      ConfigNotifySpec spec = (ConfigNotifySpec)g.AppConfig.GetCO<ConfigNotifySpec>(specName);
      return spec;
    }

    //[DebuggerStepThrough]
    public static ConfigLogSpec GetLogSpec(string prefix)
    {
      string specName = g.EnvPrefix + prefix;
      ConfigLogSpec spec = (ConfigLogSpec)g.AppConfig.GetCO<ConfigLogSpec>(specName);

      return spec;
    }

    [DebuggerStepThrough]
    public static ConfigSmtpSpec GetSmtpSpec(string prefix)
    {
      string specName = g.EnvPrefix + prefix;
      ConfigSmtpSpec spec = (ConfigSmtpSpec)g.AppConfig.GetCO<ConfigSmtpSpec>(specName);
      return spec;
    }

    [DebuggerStepThrough]
    public static int GetIntegerFromString(this string value)
    {
      int returnValue = 0;

      string numericCharacters = String.Empty;
      foreach (Char c in value)
        if (Char.IsNumber(c))
            numericCharacters += c;

      if (numericCharacters.Length > 0)
        returnValue = Int32.Parse(numericCharacters);

      return returnValue;
    }

    [DebuggerStepThrough]
    public static string GetAssemblyVersion(Assembly assembly)
    {
      Version v = assembly.GetName().Version;
      return v.Major.ToString().Trim() + "." + v.Minor.ToString().Trim() + "." + v.Build.ToString().Trim() + "." + v.Revision.ToString();
    }

    [DebuggerStepThrough]
    public static Bitmap GetBitmapFromFile(string imagePath)
    {
      System.Drawing.Image i = System.Drawing.Image.FromStream(new MemoryStream(File.ReadAllBytes(imagePath)));
      return new Bitmap(i);
    }

    [DebuggerStepThrough]
    public static string CI(string ciName)
    {
      return AppConfig.GetCI(ciName);
    }

    [DebuggerStepThrough]
    public static string CI(string ciName, string defaultValue)
    {
      string configValue = AppConfig.GetCI(ciName);
      if (configValue.IsBlank())
        configValue = defaultValue;
      return configValue;
    }

    [DebuggerStepThrough]
    public static bool CIExists(string ciName)
    {
      return AppConfig.ContainsKey(ciName); 
    }

    [DebuggerStepThrough]
    public static DateTime GetTimeOfDay(string time)
    {
      DateTime dt = new DateTime(1, 1, 1, 0, 0, 0);

      time = time.Trim();
      if (time.Length == 0)
        return dt;

      string[] s = time.Split(':');

      if (s.Length != 2)
        return dt;

      if (!s[0].IsNumeric())
        return dt;

      if (!s[1].IsNumeric())
        return dt;

      int hour = Int32.Parse(s[0]);
      int minute = Int32.Parse(s[1]);

      if (hour < 0 || hour > 23)
        return dt;

      if (minute < 0 || minute > 59)
        return dt;

      dt = new DateTime(1, 1, 1, hour, minute, 0);

      return dt;
    }


    [DebuggerStepThrough]
    public static string ValidateActiveOn(string activeOn)
    {
      activeOn = activeOn.Trim();

      if (activeOn.Length != 7)
        return "ActiveOn value must be 7 characters long, e.g. '-MTWTF-'.";

      string a = "SMTWTFS";

      for (int i = 0; i < 7; i++)
      {
        if (activeOn[i] != '-' && activeOn[i] != a[i])
          return "ActiveOn contains an illegal character in (1-based) position " + (i + 1).ToString() +
                 ". Legal values are '-' or '" + a[i].ToString() + "' in this position.";
      }

      return String.Empty;
    }

    [DebuggerStepThrough]
    public static bool IsValidTime(string time)
    {
      string[] s = time.Split(':');

      if (s.Length != 2)
        return false;

      if (!s[0].IsNumeric())
        return false;

      if (!s[1].IsNumeric())
        return false;

      int hour = Int32.Parse(s[0]);
      int minute = Int32.Parse(s[1]);

      if (hour < 0 || hour > 23)
        return false;

      if (minute < 0 || minute > 59)
        return false;

      return true;
    }

    [DebuggerStepThrough]
    public static string GetActionFromEvent(object sender)
    {
      var pi = sender.GetType().GetProperty("Tag");
      if (pi == null)
        return String.Empty;

      object value = pi.GetValue(sender);
      if (value == null)
        return String.Empty;

      return value.ToString().Trim();
    }

    [DebuggerStepThrough]
    public static string PadTo(this string value, int totalLength)
    {
      if (value == null)
        return "NULL".PadTo(totalLength);

      // establish PadWork if it has not yet been established
      if (PadWork == null)
      {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < 200; i++)
        {
          sb.Append(" ");
        }
        PadWork = sb.ToString();
      }

      if (totalLength > 200)
        totalLength = 200;

      string returnValue = (value.Trim() + PadWork).Substring(0, totalLength);

      return returnValue;
    }

    [DebuggerStepThrough]
    public static string FillTo(this string value, int totalLength, char fillChar)
    {
      if (value == null)
        return String.Empty.FillTo(totalLength, fillChar); 

      string fill = new String(fillChar, totalLength); 

      string returnValue = (value.Trim() + fill).Substring(0, totalLength);

      return returnValue;
    }

    [DebuggerStepThrough]
    public static string PadToLength(this string value, int totalLength)
    {
      if (value == null)
        return g.BlankString(totalLength);

      value = value.Replace(g.crlf, String.Empty); 

      if (value.Length > totalLength)
        return value.Substring(0, totalLength); 

      // establish PadWork if it has not yet been established
      if (PadWork == null)
      {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < 200; i++)
        {
          sb.Append(" ");
        }
        PadWork = sb.ToString();
      }

      if (totalLength > 200)
        totalLength = 200;

      string returnValue = (value.Trim() + PadWork).Substring(0, totalLength);

      return returnValue;
    }

    //[DebuggerStepThrough]
    public static string PadToLength(this string value, int totalLength, char padCharacter)
    {
      if (totalLength == 0)
        return String.Empty;

      string workValue = String.Empty;

      if (value != null)
        workValue = value.Replace(g.crlf, String.Empty).Trim();
      
      int padLength = totalLength - workValue.Length;

      if (padLength < 1)
        return value;

      string padString = new String(padCharacter, padLength);

      string paddedString = workValue + padString;
      return paddedString;
    }

    [DebuggerStepThrough]
    public static string PadLeftAndTo(this string value, int leftPad, int totalLength)
    {
      if (value == null)
        return "NULL".PadTo(totalLength);

      // establish PadWork if it has not yet been established
      if (PadWork == null)
      {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < 200; i++)
        {
          sb.Append(" ");
        }
        PadWork = sb.ToString();
      }

      if (totalLength > 200)
        totalLength = 200;

      string leftPadded = value.PadLeft(leftPad); 
      string returnValue = (leftPadded + PadWork).Substring(0, totalLength); 

      return returnValue;
    }

    [DebuggerStepThrough]
    public static string[] ToTokenArray(this string value, char[] delimiter)
    {
      if (value == null)
        return new string[0];

      return value.Trim().Split(delimiter, StringSplitOptions.RemoveEmptyEntries); 
    }

    [DebuggerStepThrough]
    public static int[] ToTokenArrayInt32(this string value, char[] delimiter)
    {
      if (value == null)
        return new int[0];

      string[] stringArray = value.Trim().Split(delimiter, StringSplitOptions.RemoveEmptyEntries); 
      int[] intArray = new int[stringArray.Length];

      for (int i = 0; i < stringArray.Length; i++)
        intArray[i] = stringArray[i].ToInt32();

      return intArray; 
    }

    [DebuggerStepThrough]
    public static string IntToFormattedString(this int? value, string formatSpecifier)
    {
      if (!value.HasValue)
        return String.Empty;

      int numericValue = value.Value;

      if (formatSpecifier.IsBlank())
        return numericValue.ToString();

      return numericValue.ToString(formatSpecifier);
    }

    [DebuggerStepThrough]
    public static string PadToJustifyRight(this string value, int totalLength)
    {
      string newValue = value.PadTo(totalLength);
      return newValue.JustifyRight();     
    }

    [DebuggerStepThrough]
    public static string JustifyRight(this string value)
    {
      int originalLength = value.Length;
      int trimmedLength = value.Trim().Length;
      int leadingBlanks = originalLength - trimmedLength;
      return g.BlankString(leadingBlanks) + value.Trim();            
    }

    [DebuggerStepThrough]
    public static string ReplaceAtPosition(this string value, string replacementString, int position)
    {
      if (value == null)
        return String.Empty;

      int originalLength = value.Length;
      int replacementStringLength = replacementString.Length;

      if (position + replacementStringLength > originalLength)
        throw new Exception("String extension method 'ReplaceAtPosition' failed due to the value of position (" + position.ToString() + ") plus " +
                            "the length of the replacementString (" + replacementStringLength.ToString() + ") being greater than the total length of the " +
                            "original string (" + originalLength.ToString() + ").");

      string frontPart = value.Substring(0, position);

      int backBeg = position + replacementStringLength;
      if (backBeg > originalLength - 1)
        backBeg = originalLength - 1;
      int backLth = originalLength - (position + replacementStringLength);
      string backPart = value.Substring(backBeg, backLth);

      string newString = frontPart + replacementString + backPart;

      return newString;
    }

    [DebuggerStepThrough]
    public static string ReplaceFirstOccurrence(this string value, string findString, string replacementString)
    {
      if (value.IsBlank() || findString.IsBlank())
        return String.Empty;

			int findStrPos = value.IndexOf(findString);
			if (findStrPos == -1)
				return value; 

      string frontPart = value.Substring(0, findStrPos);

			int backBeg = findStrPos + findString.Length;
      string backPart = value.Substring(backBeg);

      string newString = frontPart + replacementString + backPart;

      return newString;
    }

    [DebuggerStepThrough]
    public static string RemoveFirstOccurrence(this string value, string findString)
    {
      if (value.IsBlank() || findString.IsBlank())
        return String.Empty;

			int findStrPos = value.IndexOf(findString);
			if (findStrPos == -1)
				return value; 

      string frontPart = value.Substring(0, findStrPos);

			int backBeg = findStrPos + findString.Length;
      string backPart = value.Substring(backBeg);

      string newString = frontPart + backPart;

      return newString;
    }

		public static string CompressBlanksTo(this string s, int spaceCount)
		{
			if (s.IsBlank())
				return String.Empty;

			string[] tokens = s.Split(Constants.SpaceDelimiter, StringSplitOptions.RemoveEmptyEntries);
			if (tokens.Length == 0)
				return String.Empty;

			string intervalBlanks = g.BlankString(spaceCount);

			var sb = new StringBuilder();

			for (int i = 0; i < tokens.Length; i++)
			{
				sb.Append(tokens[i]);
				if (i < tokens.Length - 1)
					sb.Append(intervalBlanks);
			}

			return sb.ToString();
		}

    [DebuggerStepThrough]
    public static string TrimToMax(this string value, int maxLength)
    {
      if (value == null)
          return "NULL STRING";

      value = value.Trim();

      if (value.Length <= maxLength)
          return value;

      return value.Substring(0, maxLength);
    }

    // IMPORTANT - this method will truncate digits if the value passed in is greather than the totalLength specification.
    // Example if totalLength = 4, "1" will be come "0001", but "12345" will become "2345".
    [DebuggerStepThrough]
    public static string PadWithLeadingZeros(this string value, int totalLength)
    {
      value = value.Trim();

      if (value.Length == totalLength)
        return value;

      if (value.Length > totalLength)
      {
        string truncated = value.Substring((value.Length - totalLength), totalLength);
        return truncated;
      }

      // establish PadWork if it has not yet been established
      if (PadZero == null)
      {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < 200; i++)
        {
          sb.Append("0");
        }
        PadZero = sb.ToString();
      }

      if (totalLength > 200)
        totalLength = 200;

      int padLength = totalLength - value.Length;
      string returnValue = PadZero.Substring(0, padLength) + value;

      return returnValue;
    }

    [DebuggerStepThrough]
    public static bool IncludesLowerCaseLetter(string s)
    {
      foreach (Char c in s.Trim())
      {
        if (Char.IsLower(c))
            return true;
      }

      return false;
    }

    [DebuggerStepThrough]
    public static bool IncludesUpperCaseLetter(string s)
    {
      foreach (Char c in s.Trim())
      {
        if (Char.IsUpper(c))
            return true;
      }

      return false;
    }

    [DebuggerStepThrough]
    public static bool IncludesNumber(string s)
    {
      foreach (Char c in s.Trim())
      {
        if (Char.IsDigit(c))
            return true;
      }

      return false;
    }

    [DebuggerStepThrough]
    public static bool IsValidPath(string pathSpec)
    {
      int pos = -1;

      string path = pathSpec.Trim().Replace('/', '\\');

      if (path.Length < 3)
        return false;

      switch (path[0])
      {
        // unc path validation
        case '\\':
          // the third character cannot be a slash
          if (path[2] == '\\')
            return false;

          // need to have another slash after the first two
          pos = path.IndexOf('\\', 3);
          if (pos == -1)
            return false;

          // the third slash cannot be the end of the string
          if (pos == path.Length - 1)
            return false;

          // else return false;
          return true;


        // drive letter specification
        default:
          // must start with a letter
          if (!Char.IsLetter(path[0]))
            return false;

          // next must be a colon
          if (path.Substring(1, 1) != ":")
            return false;

          // next must be a back slash (forward converted to back previously)
          if (path.Substring(2, 1) != @"\")
            return false;

          return true;
      }
    }

    [DebuggerStepThrough]
    public static Color GetColorFromString(string value)
    {
      if (value.Length != 6)
        return Color.Black;

      int r = value.Substring(0, 2).ToLower().GetHexValue();
      int g = value.Substring(2, 2).ToLower().GetHexValue();
      int b = value.Substring(4, 2).ToLower().GetHexValue();

      return Color.FromArgb(r, g, b);
    }

    public static int GetHexValue(this string value)
    {
      int h1 = 0;
      int h0 = 0;

      if (value.Length == 2)
      {
        switch (value[0])
        {
          case 'a': h1 = 10; break;
          case 'b': h1 = 11; break;
          case 'c': h1 = 12; break;
          case 'd': h1 = 13; break;
          case 'e': h1 = 14; break;
          case 'f': h1 = 15; break;
          default:
            if (value[0].ToString().IsNumeric())
                h1 = Int32.Parse(value[0].ToString());
            break;

        }

        switch (value[1])
        {
          case 'a': h0 = 10; break;
          case 'b': h0 = 11; break;
          case 'c': h0 = 12; break;
          case 'd': h0 = 13; break;
          case 'e': h0 = 14; break;
          case 'f': h0 = 15; break;
          default:
            if (value[1].ToString().IsNumeric())
                h0 = Int32.Parse(value[0].ToString());
            break;

        }

        int hexValue = h1 * 16 + h0;
        return hexValue;
      }

      return 0;
    }

    [DebuggerStepThrough]
    public static Color GetColor(string value)
    {
      string[] s = value.Split(',');
      int r = Int32.Parse(s[0]);
      int g = Int32.Parse(s[1]);
      int b = Int32.Parse(s[2]);

      return Color.FromArgb(r, g, b);
    }

    [DebuggerStepThrough]
    public static DateTime GetDateFromLongNumeric(string date)
    {
      bool includesTime = false;
      bool includesMilliseconds = false;

      string d = date.Trim();

      if (d.Length == 19)
      {
        if (d[8] == '-' && d[15] == '.')
        {
          d = d.Replace("-", String.Empty).Replace(".", String.Empty); 
        }
        else
          return DateTime.MinValue;
      }

      if (!d.IsNumeric())
        return DateTime.MinValue;

      if (d.Length == 17)
      {
        includesTime = true;
        includesMilliseconds = true;
      }
      else
        if (d.Length == 14)
          includesTime = true;
        else
          if (d.Length != 8)
            return DateTime.MinValue;

      int year = Int32.Parse(d.Substring(0, 4));
      int month = Int32.Parse(d.Substring(4, 2));
      int day = Int32.Parse(d.Substring(6, 2));
      int hour = 0;
      int minute = 0;
      int second = 0;
      int milliseconds = 0;

      if (includesTime)
      {
        hour = Int32.Parse(d.Substring(8, 2));
        minute = Int32.Parse(d.Substring(10, 2));
        second = Int32.Parse(d.Substring(12, 2));
      }

      if (includesMilliseconds)
        milliseconds = Int32.Parse(d.Substring(14, 3));

      if (year < 1850 || year > 2100)
        return DateTime.MinValue;

      if (month < 1 || month > 12)
        return DateTime.MinValue;

      if (day < 1 || day > 31)
        return DateTime.MinValue;

      if (hour > 24)
        return DateTime.MinValue;

      if (minute > 59)
        return DateTime.MinValue;

      if (second > 59)
        return DateTime.MinValue;

      return new DateTime(year, month, day, hour, minute, second, milliseconds);
    }

    [DebuggerStepThrough]
    public static DateTime GetDateFromMDY(string m, string d, string y)
    {
      switch (m.ToLower().Trim())
      {
        case "january":
        case "jan":
          m = "1";
          break;

        case "february":
        case "feb":
          m = "2";
          break;

        case "march":
        case "mar":
          m = "3";
          break;

        case "april":
        case "apr":
          m = "4";
          break;

        case "may":
          m = "5";
          break;

        case "june":
        case "jun":
          m = "6";
          break;

        case "july":
        case "jul":
          m = "7";
          break;

        case "august":
        case "aug":
          m = "8";
          break;

        case "september":
        case "sep":
          m = "9";
          break;

        case "october":
        case "oct":
          m = "10";
          break;

        case "november":
        case "nov":
          m = "11";
          break;

        case "december":
        case "dec":
          m = "12";
          break;
      }

      if (!m.IsNumeric())
        return DateTime.MinValue;

      if (!d.IsNumeric())
        return DateTime.MinValue;

      if (!y.IsNumeric())
        return DateTime.MinValue;

      int month = Int32.Parse(m);
      int day = Int32.Parse(d);
      int year = Int32.Parse(y);

      if (year < 1850 || year > 2100)
        return DateTime.MinValue;

      if (month < 1 || month > 12)
        return DateTime.MinValue;

      if (day < 1 || day > 31)
        return DateTime.MinValue;

      string dateString = month.ToString() + "/" + day.ToString() + "/" + year.ToString();
      DateTime dt = DateTime.MinValue;
      DateTime.TryParse(dateString, out dt);
      return dt;
    }

    [DebuggerStepThrough]
    public static string GetLongNumericDateString(DateTime dt)
    {
      if (dt == DateTime.MinValue)
        return String.Empty;

      return dt.ToString("yyyyMMddHHmmss");
    }

    [DebuggerStepThrough]
    public static string RemoveTrailingSlash(string path)
    {
      path = path.Trim();

      if (path.Length == 0)
        return String.Empty;

      int lastCharPos = path.Length - 1;

      if (lastCharPos == 0)
        return String.Empty;

      string lastChar = path[lastCharPos].ToString();

      if (lastChar == @"\" || lastChar == @"/")
        return path.Substring(0, path.Length - 1);

      return path;
    }

    [DebuggerStepThrough]
    public static DateTime BuildIISLogDateTime(string dateValue, string timeValue)
    {
      DateTime dt = DateTime.MinValue;
      DateTime.TryParse(dateValue + " " + timeValue, out dt);

      return dt;
    }

    [DebuggerStepThrough]
    public static string GetAppPath()
    {
      string exePath = Process.GetCurrentProcess().MainModule.FileName;
      string startupDirectory = Path.GetDirectoryName(exePath);

      List<string> pathParts = new List<string>();

      if (startupDirectory.EndsWith(@"bin\Debug") || startupDirectory.EndsWith(@"bin\Release"))
      {
        pathParts = startupDirectory.Split(@"\".ToCharArray()).ToList();
        pathParts.RemoveRange(pathParts.Count - 2, 2);
        return string.Join(@"\", pathParts.ToArray());
      }

      if (startupDirectory.EndsWith(@"bin\x86\Debug") || startupDirectory.EndsWith(@"bin\x86\Release") ||
        startupDirectory.EndsWith(@"bin\x64\Debug") || startupDirectory.EndsWith(@"bin\x64\Release"))
      {
        pathParts = startupDirectory.Split(@"\".ToCharArray()).ToList();
        pathParts.RemoveRange(pathParts.Count - 3, 3);
        return string.Join(@"\", pathParts.ToArray());
      }

      return startupDirectory;
    }

    [DebuggerStepThrough]
    public static string GetAspNetAppPath(string path)
    {
      string startupDirectory = Path.GetDirectoryName(path);

      List<string> pathParts = new List<string>();

      if (startupDirectory.EndsWith(@"bin\Debug") || startupDirectory.EndsWith(@"bin\Release"))
      {
        pathParts = startupDirectory.Split(@"\".ToCharArray()).ToList();
        pathParts.RemoveRange(pathParts.Count - 2, 2);
        return string.Join(@"\", pathParts.ToArray());
      }

      return startupDirectory;
    }

    public static Status GetStatus(object s)
    {
      if (s == null)
        return Status.NotSet;

      int statusID = 0;

      string objectType = s.GetType().Name;

      switch (objectType)
      {
        case "String":
          string statusString = s.ToString();
          if (statusString.IsNotNumeric())
              return Status.NotSet;
          statusID = Convert.ToInt32(statusString.Trim());
          break;

        case "Int32":
          statusID = (int)s;
          break;

        default:
          return Status.NotSet;
      }

      Status status = Status.NotSet;
      Enum.TryParse<Status>(statusID.ToString(), out status);

      return status;
    }
    

    public static ConfigItemPropertySet GetConfigItemPropertySet(Type t)
    {
      // Build a collection of property names and types that have the 
      // custom attribute "[OrgConfigItem] on them.

      ConfigItemPropertySet configItemPropertySet = new ConfigItemPropertySet();

      PropertyInfo[] piSet = t.GetProperties();
      foreach (PropertyInfo pi in piSet)
      {
        if (pi.GetCustomAttributes(typeof(OrgConfigItem), false).Count() > 0)
        {
          ConfigItemProperty cip = new ConfigItemProperty();
          cip.PropertyName = pi.Name;
          cip.PropertyType = pi.PropertyType;

          if (!configItemPropertySet.ContainsKey(cip.PropertyName))
              configItemPropertySet.Add(cip.PropertyName, cip);
        }
      }

      return configItemPropertySet;
    }
    
    public static void SetConfigObjectPropertyValue(ConfigObjectBase co, PropertyInfo pi, string propertyType, string propertyValue)
    {
      if (pi == null)
          return;

      switch (propertyType)
      {
        case "DatabaseType":
          pi.SetValue(co, g.ToEnum<DatabaseType>(propertyValue, DatabaseType.NotSet), null);
          break;

        case "LogMethod":
          pi.SetValue(co, g.ToEnum<LogMethod>(propertyValue, LogMethod.NoLogging), null);
          break;

        case "LogFileFrequency":
          pi.SetValue(co, g.ToEnum<LogFileFrequency>(propertyValue, LogFileFrequency.Continuous), null);
          break;

        case "LogFileSizeManagementMethod":
          pi.SetValue(co, g.ToEnum<LogFileSizeManagementMethod>(propertyValue, LogFileSizeManagementMethod.TotalBytes), null);
          break;

        case "LogFileSizeManagementAgent":
          pi.SetValue(co, g.ToEnum<LogFileSizeManagementAgent>(propertyValue, LogFileSizeManagementAgent.Logger), null);
          break;

        case "NotifyConfigMode":
          pi.SetValue(co, g.ToEnum<NotifyConfigMode>(propertyValue, NotifyConfigMode.None), null);
          break;

        case "WebServiceBinding":
          pi.SetValue(co, g.ToEnum<WebServiceBinding>(propertyValue, WebServiceBinding.NotSet), null);
          break;

        case "Boolean":
          bool boolValue = false;
          Boolean.TryParse(propertyValue, out boolValue);
          pi.SetValue(co, boolValue, null);
          break;

        case "String":
          pi.SetValue(co, propertyValue, null);
          break;
      }
    }

    // This method will convert a string value to an enumeration value if the string value is defined for the enum,
    // otherwise it will return the default value for the enumeration which is passed in via the method call.
    // example: wsSpec.WebServiceBinding = g.ToEnum<WebServiceBinding>(strBinding, WebServiceBinding.NotSet);
    // where WebService binding is the enum, "strBinding" is the string value and ".NotSet" is the default.
    // When we upgrade to the .Net Framework 4.0, there will be an "Enum.TryParse" method built in... 
    [DebuggerStepThrough]
    public static TEnum ToEnum<TEnum>(this string strEnumValue, TEnum defaultValue)
    {
      if (!Enum.IsDefined(typeof(TEnum), strEnumValue))
        return defaultValue;

      return (TEnum)Enum.Parse(typeof(TEnum), strEnumValue);
    }

    private static string GetEnvPrefix()
    {
      switch (Environment.MachineName)
      {
        case "SB-TABLET":
          return "Tab";
      }

      return String.Empty;
    }    
    
    [DebuggerStepThrough]
    public static TEnum ToEnum<TEnum>(this object enumValue, TEnum defaultValue)
    {
      if (enumValue == null)
        return defaultValue;

      string strEnumValue = enumValue.ToString().Trim();

      if (strEnumValue.IsNumeric())
      {
        if (Enum.IsDefined(typeof(TEnum), strEnumValue.ToInt32()))
        {
          int intEnumValue = strEnumValue.ToInt32();
          return (TEnum)Enum.ToObject(typeof(TEnum), intEnumValue);
        }
        else
        {
          return defaultValue;
        }
      }

      if (!Enum.IsDefined(typeof(TEnum), strEnumValue))
        return defaultValue;

      return (TEnum)Enum.Parse(typeof(TEnum), strEnumValue);
    }

    private static object Lock_GetExceptionString;
    public static string GetExceptionString(Exception ex)
    {
      if (Lock_GetExceptionString == null)
          Lock_GetExceptionString = new object();

      lock (Lock_GetExceptionString)
      {
        string exceptionString = "Exception Type: " + ex.GetType().Name + g.nl + "Message: " + ex.Message + g.nl + "Stack Trace: " + ex.StackTrace.Trim() + g.nl;

        if (ex.InnerException != null)
        {
          string innerExceptionString = GetExceptionString(ex.InnerException);
          exceptionString += g.nl + "Inner Exception: " + exceptionString;
        }

        return exceptionString;
      }
    }

    public static string TranslateStatus(TaskResultStatus taskResultStatus)
    {
      switch (taskResultStatus)
      {
        case TaskResultStatus.Success:
        case TaskResultStatus.Complete:
          return "Success";

        case TaskResultStatus.Warning:
        case TaskResultStatus.CompleteWithNonFatalErrors:
          return "Warning";

        case TaskResultStatus.Failed:
        case TaskResultStatus.CompleteWithFatalErrors:
        case TaskResultStatus.Canceled:
        case TaskResultStatus.Aborted:
        case TaskResultStatus.BeingCanceled:
          return "Error";
      }

      return "NotSet";
    }


    public static Location GetLocation(string stackTrace)
    {
      // consider using reflection for all of this...
      // System.Reflection.MethodBase.*

      char[] delim = new char[] { '\n' };

      MethodBase method = MethodBase.GetCurrentMethod();
      string _namespace = method.ReflectedType.Namespace;

      Location loc = new Location();

      loc.Program = g.AppInfo.AppName;

      string[] lines = stackTrace.Split(delim, StringSplitOptions.RemoveEmptyEntries);
      for (int i = 0; i < lines.Length; i++)
        lines[i] = lines[i].Trim();

      string callingMethod = String.Empty;

      foreach (string line in lines)
      {
        if (line.Length > 10)
        {
          if (line.Substring(0, 3).ToLower() == "at ")
          {
            if (line.IndexOf("System.Environment") == -1 && line.IndexOf(_namespace) == -1)
            {
              string[] tokens = line.Split(new string[] { " in " }, StringSplitOptions.RemoveEmptyEntries);
              if (tokens.Length == 0)
                break;

              loc.At = tokens[0].Trim().Replace("at ", String.Empty);

              if (tokens.Length > 1)
                loc.In = tokens[1].Trim().Replace("in ", String.Empty);

              break;
            }
          }
        }
      }

      return loc;
    }

    private static bool Get_IsWebBasedApplication()
    {
      if (g.AppInfo == null)
        throw new Exception("g.AppInfo is null - cannot determine application type.");

      if (g.AppInfo.OrgApplicationType == ApplicationType.WcfService || g.AppInfo.OrgApplicationType == ApplicationType.WebSite ||
        g.AppInfo.OrgApplicationType == ApplicationType.WebApi)
        return true;

      return false;
    }
    
    public static TaskResult ProcessNotifications(SmtpParms smtpParms, string processName, string eventName, NotificationOptions options)
    {
      var notificationTaskResult = new TaskResult();

      try
      {
        if (smtpParms == null)
        {
          notificationTaskResult.TaskResultStatus = TaskResultStatus.NoConfiguration;
          notificationTaskResult.Message = "No SMTP configuration.";
          return notificationTaskResult;
        }

        var NotifyConfigSet = g.AppConfig.GetNotifyConfigSet();
        if (NotifyConfigSet == null || NotifyConfigSet.Count == 0)
        {
          notificationTaskResult.TaskResultStatus = TaskResultStatus.NoConfiguration;
          notificationTaskResult.Message = NotifyConfigSet == null ? "NotifyConfigSet is null." : "NotifyConfigSet count is 0.";
          return notificationTaskResult;
        }

        if (!NotifyConfigSet.ContainsKey(processName))
        {
          notificationTaskResult.TaskResultStatus = TaskResultStatus.NoConfiguration;
          notificationTaskResult.Message = "NotifyConfigSet does not contain an entry for process name '" + processName + "'.";
          return notificationTaskResult;
        }

        var notificationConfig = NotifyConfigSet[processName];
        var notifyEvent = notificationConfig.NotifyEventSet.GetNotifyEvent(eventName);

        if (notifyEvent == null)
        {
          notificationTaskResult.TaskResultStatus = TaskResultStatus.NoConfiguration;
          notificationTaskResult.Message = "No notify event for event name '" + eventName + "' is configured and active in the event set for process '" + processName + "'.";
          return notificationTaskResult;
        }
        
        var notification = new Notification();
        notification.Subject = notifyEvent.DefaultSubject;
        if (options.Subject.IsNotBlank())
          notification.Subject = options.Subject;
        notification.Body = options.Message;
        notification.Code = options.Code;
        notification.EventName = eventName;

        using (var notificationEngine = new NotificationEngine(notificationConfig, smtpParms))
        {
          return notificationEngine.ProcessNotifications(notification);
        }
      }
      catch (Exception ex)
      {
        if (options.ThrowExceptions)
          throw new Exception("An exception occurred while attempting to process notifications.", ex);
      }

      return notificationTaskResult;
    }


		public static string GetAsciiExtended()
		{
			var sb = new StringBuilder();
			sb.Append("ASCII Extended Characters" + g.crlf +
				        "DEC        HEX        CHR" + g.crlf + 
								"-------------------------" + g.crlf);
			sb.Append(@"000         00        \0   " + g.crlf);

			for (int i = 1; i < 256; i++)
			{
				if (i == 10)
				{
					sb.Append(i.ToString("000") + "         " + (i.ToHex()).PadWithLeadingZeros(2) + @"        \r" + g.crlf);
					continue;
				}

				if (i == 13)
				{
					sb.Append(i.ToString("000") + "         " + (i.ToHex()).PadWithLeadingZeros(2) + @"        \n" + g.crlf);
					continue;
				}

				sb.Append(i.ToString("000") + "         " + (i.ToHex()).PadWithLeadingZeros(2) + "         " + ((char)i).ToString() + g.crlf); 
			}

			string asciiExtended = sb.ToString();
			return asciiExtended;
		}

  }
}
