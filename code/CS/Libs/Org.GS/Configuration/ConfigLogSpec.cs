using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Org.GS.Logging;
using Org.GS;

namespace Org.GS.Configuration
{
  [ObfuscationAttribute(Exclude = true, ApplyToMembers = true)]
  public class ConfigLogSpec : ConfigObjectBase
  {
    [OrgConfigItem] public LogMethod LogMethod { get; set; }
    [OrgConfigItem] public string LogDbSpecPrefix { get; set; }
    [OrgConfigItem] public string LogPath { get; set; }
    [OrgConfigItem] public LogFileFrequency LogFileFrequency { get; set; }
    [OrgConfigItem] public LogFileSizeManagementMethod LogFileSizeManagementMethod { get; set; }
    [OrgConfigItem] public LogFileSizeManagementAgent LogFileSizeManagementAgent { get; set; }
    [OrgConfigItem] public int LogFileAgeMaxDays { get; set; }
    [OrgConfigItem] public int LogFileSizeMax { get; set; }
    [OrgConfigItem] public int LogFileSizeTrim { get; set; }
    [OrgConfigItem] public bool DiagnosticLogging { get; set; }

    public LogMethod OriginalLogMethod { get; set; }
    public string OriginalLogDbSpecPrefix { get; set; }
    public string OriginalLogPath { get; set; }
    public LogFileFrequency OriginalLogFileFrequency { get; set; }
    public LogFileSizeManagementMethod OriginalLogFileSizeManagementMethod { get; set; }
    public LogFileSizeManagementAgent OriginalLogFileSizeManagementAgent { get; set; }
    public int OriginalLogFileAgeMaxDays { get; set; }
    public int OriginalLogFileSizeMax { get; set; }
    public int OriginalLogFileSizeTrim { get; set; }
    public bool OriginalDiagnosticLogging { get; set; }

    public LogMethod VerifiedLogMethod { get; set; }
    public string VerifiedLogDbSpecPrefix { get; set; }
    public string VerifiedLogPath { get; set; }
    public LogFileFrequency VerifiedLogFileFrequency { get; set; }
    public LogFileSizeManagementMethod VerifiedLogFileSizeManagementMethod { get; set; }
    public LogFileSizeManagementAgent VerifiedLogFileSizeManagementAgent { get; set; }
    public int VerifiedLogFileAgeMaxDays { get; set; }
    public int VerifiedLogFileSizeMax { get; set; }
    public int VerifiedLogFileSizeTrim { get; set; }
    public bool VerifiedDiagnosticLogging { get; set; }        

    public ConfigLogSpec(string namingPrefix)
      :base (namingPrefix)
    {
      Initialize();
    }

    public ConfigLogSpec()
    {
      Initialize();
    }

    private void Initialize()
    {
      this.LogMethod = LogMethod.NoLogging;
      this.LogDbSpecPrefix = "Log";
      this.LogPath = String.Empty;
      this.LogFileFrequency = LogFileFrequency.Continuous;
      this.LogFileSizeManagementMethod = LogFileSizeManagementMethod.TotalBytes;
      this.LogFileSizeManagementAgent = LogFileSizeManagementAgent.Logger;
      this.LogFileAgeMaxDays = 20;
      this.LogFileSizeMax = 2000000;
      this.LogFileSizeTrim = 750000;
      this.DiagnosticLogging = false; 

      SetVerifiedProperties();
      SetOriginalProperties(); 
    }

    public bool CanAdvance()
    {
      return true;
    }

    public string GetDescriptionString()
    {
      switch (this.NamingPrefix.ToLower())
      {
        case "":
          return "No descriptions defined";
      }

      return "No descriptions defined";
    }

    public void SetVerifiedProperties()
    {
      this.VerifiedLogMethod = this.LogMethod;
      this.VerifiedLogDbSpecPrefix = this.LogDbSpecPrefix;
      this.VerifiedLogPath = this.LogPath;
      this.VerifiedLogFileFrequency = this.LogFileFrequency;
      this.VerifiedLogFileSizeManagementMethod = this.LogFileSizeManagementMethod;
      this.VerifiedLogFileSizeManagementAgent = this.LogFileSizeManagementAgent;
      this.VerifiedLogFileSizeMax = this.LogFileSizeMax;
      this.VerifiedLogFileSizeTrim = this.LogFileSizeTrim;
      this.VerifiedDiagnosticLogging = this.DiagnosticLogging;
    }

    public override void SetOriginalProperties()
    {
      this.OriginalLogMethod = this.LogMethod;
      this.OriginalLogDbSpecPrefix = this.LogDbSpecPrefix;
      this.OriginalLogPath = this.LogPath;
      this.OriginalLogFileFrequency = this.LogFileFrequency;
      this.OriginalLogFileSizeManagementMethod = this.LogFileSizeManagementMethod;
      this.OriginalLogFileSizeManagementAgent = this.LogFileSizeManagementAgent;
      this.OriginalLogFileSizeMax = this.LogFileSizeMax;
      this.OriginalLogFileSizeTrim = this.LogFileSizeTrim;
      this.OriginalDiagnosticLogging = this.DiagnosticLogging;
    }

    public bool IsObjectUpdated()
    {
      if (this.LogMethod == this.OriginalLogMethod &&
          this.LogDbSpecPrefix == this.OriginalLogDbSpecPrefix &&
          this.LogPath == this.OriginalLogPath &&
          this.LogFileFrequency == this.OriginalLogFileFrequency &&
          this.LogFileSizeManagementMethod == this.OriginalLogFileSizeManagementMethod &&
          this.LogFileSizeManagementAgent == this.OriginalLogFileSizeManagementAgent &&
          this.LogFileSizeMax == this.OriginalLogFileSizeMax &&
          this.LogFileSizeTrim == this.OriginalLogFileSizeTrim &&
          this.DiagnosticLogging == this.OriginalDiagnosticLogging)
        return false;

      return true;
    }
  }
}
