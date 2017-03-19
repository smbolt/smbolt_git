using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Org.GS.Configuration
{
  [ObfuscationAttribute(Exclude = true, ApplyToMembers = true)]
  public class ConfigSmtpSpec : ConfigObjectBase
  {
    public string SpecName { get; set; }

    [OrgConfigItem]
    public string SmtpServer { get; set; }
    [OrgConfigItem]
    public string SmtpPort { get; set; }
    [OrgConfigItem]
    public string SmtpUserID { get; set; }
    [OrgConfigItem]
    public string SmtpPassword { get; set; }
    [OrgConfigItem]
    public bool EnableSSL { get; set; }
    [OrgConfigItem]
    public bool PickUpFromIIS { get; set; }
    [OrgConfigItem]
    public bool AllowAnonymous { get; set; }
    [OrgConfigItem]
    public string EmailFromAddress { get; set; }

    public bool LocalNoOverride { get; set; }

    public string VerifiedSmtpServer { get; set; }
    public string VerifiedSmtpPort { get; set; }
    public string VerifiedSmtpUserID { get; set; }
    public string VerifiedSmtpPassword { get; set; }
    public bool VerifiedEnableSSL { get; set; }
    public bool VerifiedPickUpFromIIS { get; set; }
    public bool VerifiedAllowAnonymous { get; set; }
    public string VerifiedEmailFromAddress { get; set; }

    public string OriginalSmtpServer { get; set; }
    public string OriginalSmtpPort { get; set; }
    public string OriginalSmtpUserID { get; set; }
    public string OriginalSmtpPassword { get; set; }
    public bool OriginalEnableSSL { get; set; }
    public bool OriginalPickUpFromIIS { get; set; }
    public bool OriginalAllowAnonymous { get; set; }
    public string OriginalEmailFromAddress { get; set; }

    public bool SmtpSpecVerified { get; set; }
    public bool SkipSmtpSpecConfig { get; set; }

    public string DescriptionString
    {
      get { return GetDescriptionString(); }
    }

    public ConfigSmtpSpec()
    {
      Initialize();
    }

    public ConfigSmtpSpec(string namingPrefix)
        : base(namingPrefix)
    {
      Initialize();
    }

    private void Initialize()
    {
      this.SpecName = String.Empty;

      this.SmtpServer = String.Empty;
      this.SmtpPort = String.Empty;
      this.SmtpUserID = String.Empty;
      this.SmtpPassword = String.Empty;
      this.EnableSSL = false;
      this.PickUpFromIIS = false;
      this.AllowAnonymous = false;
      this.EmailFromAddress = String.Empty;

      SetVerifiedProperties();
      SetOriginalProperties();

      this.SmtpSpecVerified = false;
      this.SkipSmtpSpecConfig = false;

        this.LocalNoOverride = false;
    }

    public bool CanAdvance()
    {
      if (this.SkipSmtpSpecConfig)
        return true;

      if (this.SmtpSpecVerified)
      {
        if (this.VerifiedSmtpServer == this.SmtpServer &&
          this.VerifiedSmtpPort == this.SmtpPort &&
          this.VerifiedSmtpUserID == this.SmtpUserID &&
          this.VerifiedSmtpPassword == this.SmtpPassword &&
          this.VerifiedEnableSSL == this.EnableSSL &&
          this.VerifiedPickUpFromIIS == this.PickUpFromIIS &&
          this.VerifiedAllowAnonymous == this.AllowAnonymous && 
          this.VerifiedEmailFromAddress == this.EmailFromAddress)
          return true;
      }

      return false;
    }

    private string GetDescriptionString()
    {
      return "Smtp Parameters";
    }

    private bool IsObjectUpdated()
    {
      if (this.OriginalSmtpServer == this.SmtpServer &&
        this.OriginalSmtpPort == this.SmtpPort &&
        this.OriginalSmtpUserID == this.SmtpUserID &&
        this.OriginalSmtpPassword == this.SmtpPassword &&
        this.OriginalEnableSSL == this.EnableSSL &&
        this.OriginalPickUpFromIIS == this.PickUpFromIIS &&
        this.OriginalAllowAnonymous == this.AllowAnonymous &&
        this.OriginalEmailFromAddress == this.EmailFromAddress)
        return false;

      return true;
    }

    public void SetAsVerified()
    {
      SetVerifiedProperties();
      this.SmtpSpecVerified = true;
    }

    private void SetVerifiedProperties()
    {
      this.VerifiedSmtpServer = this.SmtpServer;
      this.VerifiedSmtpPort = this.SmtpPort;
      this.VerifiedSmtpUserID = this.SmtpUserID;
      this.VerifiedSmtpPassword = this.SmtpPassword;
      this.VerifiedEnableSSL = this.EnableSSL;
      this.VerifiedPickUpFromIIS = this.PickUpFromIIS;
      this.VerifiedAllowAnonymous = this.AllowAnonymous;
      this.VerifiedEmailFromAddress = this.EmailFromAddress;
    }

    public override void SetOriginalProperties()
    {
      this.OriginalSmtpServer = this.SmtpServer;
      this.OriginalSmtpPort = this.SmtpPort;
      this.OriginalSmtpUserID = this.SmtpUserID;
      this.OriginalSmtpPassword = this.SmtpPassword;
      this.OriginalEnableSSL = this.EnableSSL;
      this.OriginalPickUpFromIIS = this.PickUpFromIIS;
      this.OriginalAllowAnonymous = this.AllowAnonymous;
      this.OriginalEmailFromAddress = this.EmailFromAddress;
    }

    public bool ReadyToTestEmail()
    {
      if (this.EmailFromAddress.IsBlank())
        return false;

      if (this.AllowAnonymous)
      {
        if (this.SmtpServer.IsNotBlank() &&
          this.SmtpPort.IsNumeric())
        {
          return true;
        }
      }

      if (this.SmtpServer.IsNotBlank() &&
        this.SmtpPort.IsNumeric() &&
        this.SmtpUserID.IsNotBlank() &&
        this.SmtpPassword.IsNotBlank())
      {
        return true;
      }

      return false;
    }
  }
}
