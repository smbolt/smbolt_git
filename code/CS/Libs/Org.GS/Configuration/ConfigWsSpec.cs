using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Org.GS.Configuration
{
  [ObfuscationAttribute(Exclude = true, ApplyToMembers = true)]
  public class ConfigWsSpec : ConfigObjectBase
  {
    public string SpecName { get; set; }

    [OrgConfigItem] public WebServiceBinding WsBinding { get; set; }
    [OrgConfigItem] public string WsHost { get; set; }
    [OrgConfigItem] public string WsPort { get; set; }
    [OrgConfigItem] public string WsServiceName { get; set; }

    public WebServiceBinding VerifiedWsBinding { get; set; }
    public string VerifiedWsHost { get; set; }
    public string VerifiedWsPort { get; set; }
    public string VerifiedWsServiceName { get; set; }

    public WebServiceBinding OriginalWsBinding { get; set; }
    public string OriginalWsHost { get; set; }
    public string OriginalWsPort { get; set; }
    public string OriginalWsServiceName { get; set; }

    public bool WebSvcSpecVerified { get; set; }
    public bool SkipWebSvcSpecConfig { get; set; }

    public string DescriptionString
    {
      get { return GetDescriptionString(); }
    }

    public override bool IsUpdated
    {
      get { return IsObjectUpdated(); }
    }

    public string WebServiceEndpoint
    {
      get { return GetWebServiceEndpoint(); }
    }

    public ConfigWsSpec()
    {
      Initialize();
    }

    public ConfigWsSpec(string namingPrefix) 
        : base(namingPrefix)
    {
      Initialize();
    }

    public ConfigWsSpec(WebServiceBinding binding, string host, string port, string serviceName = "")
    {
      this.Initialize();

      this.WsBinding = binding;
      this.WsHost = host;
      this.WsPort = port;
      this.WsServiceName = serviceName;
    }

    private void Initialize()
    {
      this.SpecName = String.Empty;

      this.WsBinding = WebServiceBinding.NotSet;
      this.WsHost = String.Empty;
      this.WsPort = String.Empty;
      this.WsServiceName = String.Empty;

      SetVerifiedProperties();
      SetOriginalProperties();

      this.WebSvcSpecVerified = false;
      this.SkipWebSvcSpecConfig = false;
    }

    public bool CanAdvance()
    {
      if (this.SkipWebSvcSpecConfig)
        return true;

      if (this.WebSvcSpecVerified)
      {
        if (this.VerifiedWsBinding == this.WsBinding &&
          this.VerifiedWsHost == this.WsHost &&
          this.VerifiedWsPort == this.WsPort &&
          this.VerifiedWsServiceName == this.WsServiceName)
          return true;
      }

      return false;
    }

    private string GetDescriptionString()
    {
      return "Web Service Specification";
    }


    public string GetProtocolString()
    {
      string protocol = String.Empty;

      switch (this.WsBinding)
      {
        case WebServiceBinding.BasicHttp:
        case WebServiceBinding.WsHttp:
          protocol = "http";
          break;

        case WebServiceBinding.WsHttps:
          protocol = "https";
          break;

        case WebServiceBinding.NetTcp:
          protocol = "net.tcp";
          break;
      }

      return protocol;
    }

    public string GetWebServiceEndpoint()
    {
      string protocol = GetProtocolString();

      if (this.WsServiceName.IsBlank())
      {
        if (this.WsPort.IsBlank())
          return protocol + "://" + this.WsHost.Trim();
        else
          return protocol + "://" + this.WsHost.Trim() + ":" + this.WsPort.Trim();
      }
      else
      {
        if (this.WsPort.IsBlank())
          return protocol + "://" + this.WsHost.Trim() + @"/" + this.WsServiceName.Trim();
        else
          return protocol + "://" + this.WsHost.Trim() + ":" + this.WsPort.Trim() + @"/" + this.WsServiceName.Trim();
      }
    }

    public bool IsReadyToConnect()
    {
      if (this.WsBinding != WebServiceBinding.NotSet && this.WsHost.IsNotBlank() && this.WsPort.IsNotBlank())
        return true;

      return false;
    }

    private bool IsObjectUpdated()
    {
      if (this.OriginalWsBinding == this.WsBinding &&
        this.OriginalWsHost == this.WsHost &&
        this.OriginalWsPort == this.WsPort &&
        this.OriginalWsServiceName == this.WsServiceName)
        return false;

      return true;
    }

    public void SetAsVerified()
    {
      SetVerifiedProperties();
      this.WebSvcSpecVerified = true;
    }

    private void SetVerifiedProperties()
    {
      this.VerifiedWsBinding = this.WsBinding;
      this.VerifiedWsHost = this.WsHost;
      this.VerifiedWsPort = this.WsPort;
      this.VerifiedWsServiceName = this.WsServiceName;
    }

    public override void SetOriginalProperties()
    {
      this.OriginalWsBinding = this.WsBinding;
      this.OriginalWsHost = this.WsHost;
      this.OriginalWsPort = this.WsPort;
      this.OriginalWsServiceName = this.WsServiceName;
    }
  }
}
