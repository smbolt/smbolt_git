using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Org.GS.Configuration
{
  [Serializable]
  [ObfuscationAttribute(Exclude = true, ApplyToMembers = true)]
  public class ConfigFtpSpec : ConfigObjectBase
  {
    [OrgConfigItem]
    public string FtpServer { get; set; }
    [OrgConfigItem]
    public string FtpUserId { get; set; }
    [OrgConfigItem]
    public string FtpPassword { get; set; }
    [OrgConfigItem]
    public bool FtpKeepAlive { get; set; }
    [OrgConfigItem]
    public bool FtpUsePassive { get; set; }
    [OrgConfigItem]
    public bool FtpUseBinary { get; set; }
    [OrgConfigItem]
    public int FtpBufferSize { get; set; }

    public string VerifiedFtpServer { get; set; }
    public string VerifiedFtpUserId { get; set; }
    public string VerifiedFtpPassword { get; set; }
    public bool VerifiedFtpKeepAlive { get; set; }
    public bool VerifiedFtpUsePassive { get; set; }
    public bool VerifiedFtpUseBinary { get; set; }
    public int VerifiedFtpBufferSize { get; set; }

    private string OriginalFtpServer;
    private string OriginalFtpUserId;
    private string OriginalFtpPassword;
    private bool OriginalFtpKeepAlive { get; set; }
    private bool OriginalFtpUsePassive { get; set; }
    private bool OriginalFtpUseBinary { get; set; }
    private int OriginalFtpBufferSize { get; set; }

    public bool FtpConnectionVerified { get; set; }
    public bool SkipFtpConnectionConfig { get; set; }

    //public override ConfigStatus ConfigStatus
    //{
    //    get { return GetConfigStatus(); }
    //}


    public string DescriptionString
    {
      get { return GetDescriptionString(); }
    }

    public override bool IsUpdated
    {
      get { return IsObjectUpdated(); }
    }

    public ConfigFtpSpec(string namingPrefix)
        : base(namingPrefix)
    {
      Initialize();
    }

    public ConfigFtpSpec()
    {
      Initialize();
    }

    private void Initialize()
    {
      this.FtpServer = String.Empty;
      this.FtpUserId = String.Empty;
      this.FtpPassword = String.Empty;
      this.FtpKeepAlive = false;
      this.FtpUsePassive = false;
      this.FtpUseBinary = false;
      this.FtpBufferSize = 0;

      SetVerifiedProperties();
      SetOriginalProperties();

      this.FtpConnectionVerified = false;
      this.SkipFtpConnectionConfig = false;
    }

    public bool CanAdvance()
    {
      if (this.SkipFtpConnectionConfig)
        return true;

      if (this.FtpConnectionVerified)
      {
        if (this.VerifiedFtpServer == this.FtpServer &&
          this.VerifiedFtpUserId == this.FtpUserId &&
          this.VerifiedFtpPassword == this.FtpPassword &&
          this.VerifiedFtpKeepAlive == this.FtpKeepAlive &&
          this.VerifiedFtpUsePassive == this.FtpUsePassive &&
          this.VerifiedFtpUseBinary == this.FtpUseBinary &&
          this.VerifiedFtpBufferSize == this.FtpBufferSize
          )
          return true;
      }

      return false;
    }

    private string GetDescriptionString()
    {
      return "Ftp parameters";
    }

    public bool IsReadyToConnect()
    {
        return (this.FtpServer.IsNotBlank() && this.FtpUserId.IsNotBlank() && this.FtpPassword.IsNotBlank());
    }

    private bool IsObjectUpdated()
    {
      if (this.OriginalFtpServer == this.FtpServer &&
        this.OriginalFtpUserId == this.FtpUserId &&
        this.OriginalFtpPassword == this.FtpPassword &&
        this.OriginalFtpKeepAlive == this.FtpKeepAlive &&
        this.OriginalFtpUsePassive == this.FtpUsePassive &&
        this.OriginalFtpUseBinary == this.FtpUseBinary &&
        this.OriginalFtpBufferSize == this.FtpBufferSize
        )
        return false;

      return true;
    }

    public void SetAsVerified()
    {
      SetVerifiedProperties();
      this.FtpConnectionVerified = true;
    }

    private void SetVerifiedProperties()
    {
      this.VerifiedFtpServer = this.FtpServer;
      this.VerifiedFtpUserId = this.FtpUserId;
      this.VerifiedFtpPassword = this.FtpPassword;
      this.VerifiedFtpKeepAlive = this.FtpKeepAlive;
      this.VerifiedFtpUsePassive = this.FtpUsePassive;
      this.VerifiedFtpUseBinary = this.FtpUseBinary;
      this.VerifiedFtpBufferSize = this.FtpBufferSize;
    }

    public override void SetOriginalProperties()
    {
      this.OriginalFtpServer = this.FtpServer;
      this.OriginalFtpUserId = this.FtpUserId;
      this.OriginalFtpPassword = this.FtpPassword;
      this.OriginalFtpKeepAlive = this.FtpKeepAlive;
      this.OriginalFtpUsePassive = this.FtpUsePassive;
      this.OriginalFtpUseBinary = this.FtpUseBinary;
      this.OriginalFtpBufferSize = this.FtpBufferSize;
    }
  }
}
