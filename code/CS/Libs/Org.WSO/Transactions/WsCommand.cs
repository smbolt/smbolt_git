using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;
using Org.WSO;

namespace Org.WSO.Transactions
{
  public enum WsCommandType
  {
    NotSet,
    Service,
    Site
  }
  public enum WsCommandName
  {
    NotSet,
    StartWinService, 
    StopWinService,
    PauseWinService,
    ResumeWinService,
    RestartWinService,
    GetWinServiceStatus,
    GetWinServiceInfo,
    GetWinServiceList,
    GetWinServices,
    
    StartWebSite,
    StopWebSite,
    RestartWebSite,
    GetWebSiteStatus,
    GetWebSiteInfo,
    GetWebServiceInfo,
    GetWebSiteList,
    GetWebSites,

    StartAppPool,
    StopAppPool,
    GetAppPoolStatus,
    GetAppPoolInfo,
    GetAppPoolList,
    GetAppPools,
    
    SendServiceCommand
  }

  [XMap(XType = XType.Element)]
  public class WsCommand
  {
    [XMap(IsRequired = true)]
    public WsCommandName WsCommandName { get; set; }

    [XMap(DefaultValue = "NotSet")]
    public TaskResultStatus TaskResultStatus { get; set; }

    [XMap(DefaultValue = "")]
    public string Message { get; set; }

    [XMap(XType = XType.Element)]
    public ObjectWrapper ObjectWrapper { get; set; }

    [XMap(DefaultValue = "0")]
    public float DurationSeconds { get; set; }

    //[XMap(XType = XType.Element, CollectionElements = "Parm", WrapperElement = "Parms", KeyName="K")]
    public Dictionary<string, string> Parms { get; set; }

    [XMap(DefaultValue = "0")]
    public int BeforeWaitMilliseconds { get; set; }

    [XMap(DefaultValue = "0")]
    public int AfterWaitMilliseconds { get; set; }

    public WsCommand()
    {
      this.WsCommandName = WsCommandName.NotSet;
      this.TaskResultStatus = GS.TaskResultStatus.NotSet;
      this.Message = String.Empty;
      this.ObjectWrapper = null;
      this.DurationSeconds = 0F;
      this.Parms = new Dictionary<string, string>();
      this.BeforeWaitMilliseconds = 0;
      this.AfterWaitMilliseconds = 0;
    }

    public void SetDuration(DateTime beginDT, DateTime endDT)
    {
      TimeSpan tsDuration = endDT - beginDT;
      int totalSeconds = Convert.ToInt32(tsDuration.TotalSeconds);
      int milliseconds = tsDuration.Milliseconds;
      this.DurationSeconds = totalSeconds + (float) milliseconds / 1000;
    }
  }
}
