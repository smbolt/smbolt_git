using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.AX
{
  public enum AxionType
  {
    NotSet,
    StartLocalWinService,
    StopLocalWinService,
    PauseLocalWinService,
    ResumeLocalWinService,
    GetLocalWinServiceStatus,
    GetLocalWinServiceInfo,
    GetLocalWinServiceList,

    StartRemoteWinService,
    StopRemoteWinService,
    PauseRemoteWinService,
    ResumeRemoteWinService,
    GetRemoteWinServiceStatus,
    GetRemoteWinServiceInfo,
    GetRemoteWinServiceList,

    StartLocalWebSite,
    StopLocalWebSite,
    GetLocalWebSiteStatus,
    GetLocalWebSiteInfo,
    GetLocalWebSiteList,

    StartRemoteWebSite,
    StopRemoteWebSite,
    GetRemoteWebSiteStatus,
    GetRemoteWebSiteInfo,
    GetRemoteWebServiceInfo,
    GetRemoteWebSiteList,

    StartLocalAppPool,
    StopLocalAppPool,
    GetLocalAppPoolStatus,
    GetLocalAppPoolInfo,
    GetLocalAppPoolList,

    StartRemoteAppPool,
    StopRemoteAppPool,
    GetRemoteAppPoolStatus,
    GetRemoteAppPoolInfo,
    GetRemoteAppPoolList,

    CopyFiles,
    MoveFiles,
    DeleteFiles,
    DeleteAllFolderContents,
    DeleteRecursive,

    VerifyWinService,
    VerifyWebSite
  }
}
