using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Org.GS.Configuration
{
  [ObfuscationAttribute(Exclude = true, ApplyToMembers = true)]
  public class ConfigSyncSpec : ConfigObjectBase
  {
    public string SpecName { get; set; }

    [OrgConfigItem] public string LeftFolder { get; set; }
    [OrgConfigItem] public string RightFolder { get; set; }
    [OrgConfigItem] public string Filter { get; set; }
    [OrgConfigItem] public string ScriptName { get; set; }
    [OrgConfigItem] public bool Active { get; set; }

    public string VerifiedLeftFolder { get; set; }
    public string VerifiedRightFolder { get; set; }
    public string VerifiedFilter { get; set; }
    public string VerifiedScriptName { get; set; }
    public bool VerifiedActive { get; set; }

    public string OriginalLeftFolder { get; set; }
    public string OriginalRightFolder { get; set; }
    public string OriginalFilter { get; set; }
    public string OriginalScriptName { get; set; }
    public bool OriginalActive { get; set; }

    public bool SyncSpecVerified { get; set; }
    public bool SkipSyncSpecConfig { get; set; }

    public string DescriptionString
    {
      get { return GetDescriptionString(); }
    }

    public override bool IsUpdated
    {
      get { return IsObjectUpdated(); }
    }

    public ConfigSyncSpec()
    {
      Initialize();
    }

    public ConfigSyncSpec(string namingPrefix) 
        : base(namingPrefix)
    {
      Initialize();
    }

    private void Initialize()
    {
      this.SpecName = String.Empty;

      this.RightFolder = String.Empty;
      this.LeftFolder = String.Empty;
      this.Filter = String.Empty;
      this.ScriptName = String.Empty;
      this.Active = true;

      SetVerifiedProperties();
      SetOriginalProperties();

      this.SyncSpecVerified = false;
      this.SkipSyncSpecConfig = false;
    }

    public bool CanAdvance()
    {
      if (this.SkipSyncSpecConfig)
        return true;

      if (this.SyncSpecVerified)
      {
        if (this.VerifiedLeftFolder == this.LeftFolder &&
          this.VerifiedRightFolder == this.RightFolder &&
          this.VerifiedFilter == this.Filter &&
          this.VerifiedScriptName == this.ScriptName &&
          this.VerifiedActive == this.Active)
          return true;
      }

      return false;
    }

    private string GetDescriptionString()
    {
      return "Folder Sync Specification";
    }

    private bool IsObjectUpdated()
    {
      if (this.OriginalLeftFolder == this.LeftFolder &&
        this.OriginalRightFolder == this.RightFolder &&
        this.OriginalFilter == this.Filter &&
        this.OriginalScriptName == this.ScriptName && 
        this.OriginalActive == this.Active)
        return false;

      return true;
    }

    public bool IsValid()
    {
      if (this.LeftFolder.IsNotBlank() && this.RightFolder.IsNotBlank() && this.ScriptName.IsNotBlank())
        return true;

      return false;
    }

    public void SetAsVerified()
    {
      SetVerifiedProperties();
      this.SyncSpecVerified = true;
    }

    private void SetVerifiedProperties()
    {
      this.VerifiedLeftFolder = this.LeftFolder;
      this.VerifiedRightFolder = this.RightFolder;
      this.VerifiedFilter = this.Filter;
      this.VerifiedScriptName = this.ScriptName;
      this.VerifiedActive = this.Active;
    }

    public override void SetOriginalProperties()
    {
      this.OriginalLeftFolder = this.LeftFolder;
      this.OriginalRightFolder = this.RightFolder;
      this.OriginalFilter = this.Filter;
      this.OriginalScriptName = this.ScriptName;
      this.OriginalActive = this.Active;
    }
  }
}
