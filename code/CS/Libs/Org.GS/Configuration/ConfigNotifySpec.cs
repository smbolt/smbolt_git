using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Org.GS;


namespace Org.GS.Configuration
{
  [ObfuscationAttribute(Exclude = true, ApplyToMembers = true)]
  public class ConfigNotifySpec : ConfigObjectBase
  {
    [OrgConfigItem] public NotifyConfigMode NotifyConfigMode {
      get;
      set;
    }
    [OrgConfigItem] public string NotifyConfigSetName {
      get;
      set;
    }
    [OrgConfigItem] public string NotifyDbSpecPrefix {
      get;
      set;
    }
    [OrgConfigItem] public string NotifyDefaultEventName {
      get;
      set;
    }
    [OrgConfigItem] public bool NotifyOnGetTasks {
      get;
      set;
    }
    [OrgConfigItem] public bool NotifyOnNoWorkDone {
      get;
      set;
    }

    public NotifyConfigMode OriginalNotifyConfigMode {
      get;
      set;
    }
    public string OriginalNotifyConfigSetName {
      get;
      set;
    }
    public string OriginalNotifyDbSpecPrefix {
      get;
      set;
    }
    public string OriginalNotifyDefaultEventName {
      get;
      set;
    }
    public bool OriginalNotifyOnGetTasks {
      get;
      set;
    }
    public bool OriginalNotifyOnNoWorkDone {
      get;
      set;
    }

    public NotifyConfigMode VerifiedNotifyConfigMode {
      get;
      set;
    }
    public string VerifiedNotifyConfigSetName {
      get;
      set;
    }
    public string VerifiedNotifyDbSpecPrefix {
      get;
      set;
    }
    public string VerifiedNotifyDefaultEventName {
      get;
      set;
    }
    public bool VerifiedNotifyOnGetTasks {
      get;
      set;
    }
    public bool VerifiedNotifyOnNoWorkDone {
      get;
      set;
    }

    public ConfigNotifySpec(string namingPrefix)
      :base (namingPrefix)
    {
      Initialize();
    }

    public ConfigNotifySpec()
    {
      Initialize();
    }

    private void Initialize()
    {
      this.NotifyConfigMode = NotifyConfigMode.None;
      this.NotifyConfigSetName = String.Empty;
      this.NotifyDbSpecPrefix = String.Empty;
      this.NotifyDefaultEventName = "Default";
      this.NotifyOnGetTasks = false;
      this.NotifyOnNoWorkDone = false;

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

    public void SetAsVerified()
    {
      SetVerifiedProperties();
    }

    public void SetVerifiedProperties()
    {
      this.VerifiedNotifyConfigMode = this.NotifyConfigMode;
      this.VerifiedNotifyConfigSetName = this.NotifyConfigSetName;
      this.VerifiedNotifyDbSpecPrefix = this.NotifyDbSpecPrefix;
      this.VerifiedNotifyDefaultEventName = this.NotifyDefaultEventName;
      this.VerifiedNotifyOnGetTasks = this.NotifyOnGetTasks;
      this.VerifiedNotifyOnNoWorkDone = this.NotifyOnNoWorkDone;
    }

    public override void SetOriginalProperties()
    {
      this.OriginalNotifyConfigMode = this.NotifyConfigMode;
      this.OriginalNotifyConfigSetName = this.NotifyConfigSetName;
      this.OriginalNotifyDbSpecPrefix = this.NotifyDbSpecPrefix;
      this.OriginalNotifyDefaultEventName = this.NotifyDefaultEventName;
      this.OriginalNotifyOnGetTasks = this.NotifyOnGetTasks;
      this.OriginalNotifyOnNoWorkDone = this.NotifyOnNoWorkDone;
    }

    public bool IsObjectUpdated()
    {
      if (this.NotifyConfigMode == this.OriginalNotifyConfigMode &&
          this.NotifyConfigSetName == this.OriginalNotifyConfigSetName &&
          this.NotifyDbSpecPrefix == this.OriginalNotifyDbSpecPrefix &&
          this.NotifyDefaultEventName == this.NotifyDefaultEventName &&
          this.NotifyOnGetTasks == this.OriginalNotifyOnGetTasks &&
          this.NotifyOnNoWorkDone == this.OriginalNotifyOnNoWorkDone)
        return false;

      return true;
    }
  }
}
