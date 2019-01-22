using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Org.WSO
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

    FlushAppDomains,
    GetAssemblyReport,
    GetAppDomainReport,
    GetRunningTaskReport,
    ShowServiceTaskReport,
    RemoveFromQueue,
    RemoveFromAssignment,
    RunNow,

    SendServiceCommand,
    RefreshTaskList,
    RefreshTaskRequests
  }

  public enum TransactionStatus
  {
    NotSet,
    InitialRequest,
    Success,
    Failed,
    Warning,
    Error
  }

  public enum OSBits
  {
    NotSet,
    Bits32,
    Bits64
  }
}
