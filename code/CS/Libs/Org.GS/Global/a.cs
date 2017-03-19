﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Reflection;
using Org.GS.Logging;
using Org.GS.Configuration;

namespace Org.GS
{
  public class a : OrgBase
  {
    private bool _runFsActions = true;
    private Logger Logger;
    private bool _deferEstablishLogContext = false;
    private bool _initializationBegun = false;
    private string[] _args = new string[] { }; 
    private string _memoryLog = String.Empty;
    public string MemoryLog { get { return _memoryLog; } }

    public string _appConfigSuffix;

    public string LogFilePath
    {
      get
      {
        if (this.Logger == null)
            return String.Empty;
        return this.Logger.LogFilePath;
      }
    }

    public a()
    {
      Initialize();
    }
    
    public a(bool deferEstablishLogContext, bool runFsActions)
    {
      _deferEstablishLogContext = deferEstablishLogContext;
      _runFsActions = runFsActions;
      Initialize();
    }

    private void Initialize()
    {
      try
      {
        _appConfigSuffix = String.Empty;

        _args = Environment.GetCommandLineArgs();
        ProcessArgs();

        if (_appConfigSuffix.IsNotBlank())
          g.AppInfo.AppConfigSuffix = _appConfigSuffix;

        if (g.AppInfo.MissingOrgAttributes)
          throw new Exception("The AssemblyInfo.cs file is missing some or all of the expected 'Org' attributes."); 

        if (_initializationBegun)
          return;

        g.LogToMemory("Entered the Initialize method of class 'a'");

        _initializationBegun = true;

        if (!g.LocalConfig.ContainsKey("Vendor"))
          g.LocalConfig.AddCI("Vendor", g.AppInfo.Vendor);

        if (!g.LocalConfig.ContainsKey("AppType"))
          g.LocalConfig.AddCI("AppType", g.AppInfo.OrgApplicationType.ToString());

        if (!g.LocalConfig.ContainsKey("AppName"))
          g.LocalConfig.AddCI("AppName", g.AppInfo.AppName);

        if (!g.LocalConfig.ContainsKey("UseLocalAppData"))
          g.LocalConfig.AddCI("UseLocalAppData", "True");

        if (!g.LocalConfig.ContainsKey("UseInitialConfig"))
          g.LocalConfig.AddCI("UseInitialConfig", "False");

        g.UseInitialConfig = g.LocalConfig.GetBoolean("UseInitialConfig");             

        if (!g.LocalConfig.ContainsKey("AttemptConfigImport"))
          g.LocalConfig.AddCI("AttemptConfigImport", "False");

        g.AttemptConfigImport = g.LocalConfig.GetBoolean("AttemptConfigImport"); 

        g.AppInfo.Vendor = g.LocalConfig.GetCI("Vendor");

        if (g.LocalConfig.ContainsKey("AppType"))
          g.AppInfo.OrgApplicationType = (ApplicationType)Enum.Parse(typeof(ApplicationType), g.LocalConfig.GetCI("AppType"));

        g.AppInfo.AppName = g.LocalConfig.GetCI("AppName");
        g.ProgramDataPath = GetProgramDataPath();

        AssertAppDataStructure(g.AppDataPath);

        StartupLogging.WriteStartupLog(g.AppInfo.OrgApplicationType.ToString() + " '" + g.AppInfo.AppName + "' is starting up.  In a.Initialize."); 

        string vaultPath = g.ResourcePath + @"\config.bin";
        if (File.Exists(vaultPath) && !g.IsVaultLoadedFromFile)
        {
          g.Vault = new Vault();
          g.Vault.LoadFromFile(vaultPath);
          g.IsVaultLoadedFromFile = true;
        }

        StartupLogging.WriteStartupLog(g.AppInfo.OrgApplicationType.ToString() + " '" + g.AppInfo.AppName + "' before AppConfig load.  In a.Initialize."); 
        g.AppConfig = new AppConfig(g.AppInfo.ConfigName);
        g.AppConfig.Initialize();
        this.LoadConfig();

        if (g.CIExists("ConfigLogSpecPrefix"))
        {
          string logSpecPrefix = g.CI("ConfigLogSpecPrefix");
          var logSpec = g.GetLogSpec(logSpecPrefix);
          LogContext.ConfigLogSpec = logSpec;
          if (logSpec.LogDbSpecPrefix.IsNotBlank())
          {
            var logDbSpec = g.GetDbSpec(logSpec.LogDbSpecPrefix);
            LogContext.LogConfigDbSpec = logDbSpec; 
          }
        }

        StartupLogging.WriteStartupLog(g.AppInfo.OrgApplicationType.ToString() + " '" + g.AppInfo.AppName + "' after AppConfig load.  In a.Initialize.");
        
        if (g.CI("RunActionSetAtStartup").ToBoolean() && _runFsActions)
        {
          var fsActionSet = g.AppConfig.ProgramConfigSet[g.AppConfig.ConfigName].FSActionSet;

          using (var fsEngine = new FSEngine())
          {
            var taskResult = fsEngine.Run(fsActionSet);
            if (taskResult.TaskResultStatus != TaskResultStatus.Success)
            {
              throw new Exception("FSActionSet execution failed. Error message is '" + taskResult.Message + "'."); 
            }
          }
        }

        this.UpdateLicenseStatus(); 

        g.DiagnosticsManager.SetRunUnitDiagnosticsMode(g.ToEnum<DiagnosticsMode>(g.AppConfig.GetCI("RunUnitDiagnosticsMode"), DiagnosticsMode.None)); 

        if (g.IsModuleHost && !g.IsModuleConfigLoaded)
        {
          g.ModuleConfig = new AppConfig(g.AppInfo.AppName);
          g.ModuleConfig.Initialize();
          this.LoadModuleConfig();
          g.IsModuleConfigLoaded = true;
        }

        StartupLogging.WriteStartupLog(g.AppInfo.OrgApplicationType.ToString() + " '" + g.AppInfo.AppName + "' POINT 1.  In a.Initialize.");

        g.DefaultConfigSmtpSpec = g.GetSmtpSpec("Default");

        bool skipInitLogging = g.CI("SkipInitLogging").ToBoolean();

        this.Logger = new Logger();
        this.Logger.ModuleId = g.AppInfo.ModuleCode;

        if (!skipInitLogging)
          this.Logger.Log("Application '" + g.AppInfo.AppName + "' " + "Version " + g.AppInfo.OrgVersion + " is initializing the application object (a).", 1001);

        if (!_deferEstablishLogContext && LogContext.LogContextState != LogContextState.Normal)
          LogContext.EstablishContext();

        _memoryLog = g.MemoryLog;
        g.ClearMemoryLog();
      }
      catch (Exception ex)
      {
        g.LogToMemory("Exception occurred in Initialize method of 'a' object." + g.crlf + "Exception Report:" + g.crlf + ex.ToReport());
        _memoryLog = g.MemoryLog;
        throw new Exception("An error occurred during the Initialize method of the 'a' (application object).", ex); 
      }
    }

    private void ProcessArgs()
    {
      if (_args.Length < 2)
        return;

      if (_args.Contains("-appConfigSuffix"))
      {
        int appConfigSuffixIndex = GetIndexOf("-appConfigSuffix");
        if (_args.Length > appConfigSuffixIndex + 1)
          _appConfigSuffix = _args[appConfigSuffixIndex + 1].Trim();
      }
    }


    private int GetIndexOf(string item)
    {
      if (_args == null || _args.Length == 0)
        return -1;

      for (int i = 0; i < _args.Length; i++)
      {
        if (_args[i] == item)
          return i;
      }

      return -1;
    }

    private string GetProgramDataPath()
    {
      string programDataPath = String.Empty;

      if (g.UseLocalAppData)
      {
        if (g.IsWebBasedApplication)
        {
          string webRoot = System.Web.Hosting.HostingEnvironment.MapPath("~");
          programDataPath = g.RemoveTrailingSlash(webRoot) + @"\ProgramData";
        }
        else
        {
          programDataPath = g.GetAppPath() + @"\ProgramData";
        }
      }
      else
      {
        if (g.IsWebBasedApplication)
        {
          string webRoot = System.Web.Hosting.HostingEnvironment.MapPath("~");
          programDataPath = g.RemoveTrailingSlash(webRoot) + @"\ProgramData";
        }
        else
        {
          programDataPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
        }
      }

      return programDataPath;
    }

    public void LoadConfig()
    {
      string configPath = g.AppConfigPath + @"\AppConfig.xmlx";
      g.AppConfig.Clear();

      if (_args.Contains("-objectFactoryDebugDeserialize"))
      {
        int index = Array.IndexOf(_args, "-objectFactoryDebugDeserialize");
        if (index > -1)
        {
          if (_args.Length > index)
          {
            string debugItem = _args[index + 1];
            if (debugItem.IsNotBlank())
              g.AppConfig.ObjectFactoryDebugDeserialize = debugItem.Trim();
          }
        }
      }

      if (_args.Contains("-objectFactoryDebugSerialize"))
      {
        int index = Array.IndexOf(_args, "-objectFactoryDebugSerialize");
        if (index > -1)
        {
          if (_args.Length > index)
          {
            string debugItem = _args[index + 1];
            if (debugItem.IsNotBlank())
              g.AppConfig.ObjectFactoryDebugSerialize = debugItem.Trim();
          }
        }
      }

      g.AppConfig.LoadFromFile(configPath);
      g.AppConfig.LoadedBy = g.AppInfo.ModuleName;
    }

    public void LoadModuleConfig()
    {
      string configPath = g.AppConfigPath + @"\ModuleConfig.xmlx";
      g.ModuleConfig.Clear();
      g.ModuleConfig.LoadFromFile(configPath);
      g.ModuleConfig.LoadedBy = g.AppInfo.AppName;
    }

    public void Log(string message)
    {
      this.Logger.Log(message);
    }

    public void ClearLogFile()
    {
      this.Logger.ClearLogFile();
    }
    
    private void AssertAppDataStructure(string appDataPath)
    {
      appDataPath = appDataPath.Replace("/", "\\");
      string[] nodes = appDataPath.Split('\\');
      string[] programDataNodes = g.ProgramDataPath.Split('\\');

      string path = g.RemoveTrailingSlash(g.ProgramDataPath);
      string appDataParentPath = String.Empty;

      for (int i = programDataNodes.Length; i < nodes.Length; i++)
      {
        if (nodes[i] == "AppData")
            appDataParentPath = path;

        path = path + @"\" + nodes[i];

        if (!Directory.Exists(path))
          Directory.CreateDirectory(path);
      }

      if (g.AppInfo.AppConfigSuffix.IsBlank())
        g.AppConfigPath = g.AppDataPath + @"\AppConfig";
      else
        g.AppConfigPath = g.AppDataPath + @"\AppConfig" + "_" + g.AppInfo.AppConfigSuffix;

      if (!Directory.Exists(g.AppConfigPath))
        Directory.CreateDirectory(g.AppConfigPath);

      g.CentralConfigPath = g.AppDataPath + @"\CentralConfig";
      if (!Directory.Exists(g.CentralConfigPath))
        Directory.CreateDirectory(g.CentralConfigPath);

      g.LogPath = g.AppDataPath + @"\Log";
      if (!Directory.Exists(g.LogPath))
        Directory.CreateDirectory(g.LogPath);

      g.ResourcePath = g.AppDataPath + @"\Resources";
      if (!Directory.Exists(g.ResourcePath))
        Directory.CreateDirectory(g.ResourcePath);

      g.ReportsPath = g.AppDataPath + @"\Reports";
      if (!Directory.Exists(g.ReportsPath))
        Directory.CreateDirectory(g.ReportsPath);

      g.ImportsPath = g.AppDataPath + @"\Imports";
      if (!Directory.Exists(g.ImportsPath))
        Directory.CreateDirectory(g.ImportsPath);

      g.ExportsPath = g.AppDataPath + @"\Exports";
      if (!Directory.Exists(g.ExportsPath))
        Directory.CreateDirectory(g.ExportsPath);

      g.ErrorsPath = g.AppDataPath + @"\Errors";
      if (!Directory.Exists(g.ErrorsPath))
        Directory.CreateDirectory(g.ErrorsPath);

      g.MEFCatalog = g.AppDataPath + @"\MEFCatalog";

      if (File.Exists(g.MEFCatalog))
        File.Delete(g.MEFCatalog); 

      if (!Directory.Exists(g.MEFCatalog))
        Directory.CreateDirectory(g.MEFCatalog);

      g.DocPath = g.AppDataPath + @"\Doc";
      if (!Directory.Exists(g.DocPath))
        Directory.CreateDirectory(g.DocPath);

      g.HelpPath = g.AppDataPath + @"\Help";
      if (!Directory.Exists(g.HelpPath))
        Directory.CreateDirectory(g.HelpPath);

      g.PerformancePath = g.AppDataPath + @"\Performance";
      if (!Directory.Exists(g.PerformancePath))
        Directory.CreateDirectory(g.PerformancePath);
    }

    private void UpdateLicenseStatus()
    {
      switch (g.AppInfo.LicenseScheme)
      {
        case LicenseScheme.Simple1:
          if (g.AppConfig.ContainsKey("FreeUntil"))
            g.AppInfo.FreeUntil = g.AppConfig.GetDate("FreeUntil");

          if (g.AppConfig.ContainsKey("FreeAfter"))
            g.AppInfo.FreeAfter = g.AppConfig.GetDate("FreeAfter");

          if (DateTime.Now > g.AppInfo.FreeAfter)
          {
            g.AppInfo.LicenseStatus = LicenseStatus.Current;
            return;
          }

          if (DateTime.Now < g.AppInfo.FreeUntil)
          {
            g.AppInfo.LicenseStatus = LicenseStatus.Current;

            DateTime expiringDate = g.AppInfo.FreeUntil.AddDays(g.AppInfo.LicenseExpiringInterval * -1);
            if (DateTime.Now > expiringDate)
            {
              g.AppInfo.LicenseStatus = LicenseStatus.ExpiringSoon;
              g.AppInfo.LicenseRemainingDays = (g.AppInfo.FreeUntil - DateTime.Now).Days; 
            }
            return; 
          }

          g.AppInfo.LicenseStatus = LicenseStatus.Expired; 
          break;
                    
        default:
          g.AppInfo.LicenseStatus = LicenseStatus.Perpetual;
          return; 
      }
    }
  }
}
