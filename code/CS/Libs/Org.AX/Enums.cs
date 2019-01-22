using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Org.AX
{
  [ObfuscationAttribute(Exclude = true)]
  public enum AxFunction
  {
    NotSet,
    StartWinService,
    StopWinService,
    PauseWinService,
    ResumelWinService,
    GetWinServiceStatus,
    GetWinServiceInfo,
    GetWinServiceList,

    StartWebSite,
    StopWebSite,
    GetWebSiteStatus,
    GetWebSiteInfo,
    GetWebSiteList,

    StartAppPool,
    StopAppPool,
    GetAppPoolStatus,
    GetAppPoolInfo,
    GetAppPoolList,

    CopyFiles,
    MoveFiles,
    DeleteFiles,
    DeleteAllFolderContents,
    DeleteRecursive,

    VerifyWinService,
    VerifyWebSite
  }

  [ObfuscationAttribute(Exclude = true)]
  public enum AxActionClass
  {
    FileSystemAction,
    ServiceManagementAction,
    NotSet
  }

  [ObfuscationAttribute(Exclude = true)]
  public enum ProfileStatus
  {
    NotSet,
    Active,
    Disabled
  }

  [ObfuscationAttribute(Exclude = true)]
  public enum InclusionResult
  {
    IncludedByDefault,
    IncludedFileMatch,
    IncludedByExtension,
    IncludedExtensionExclusionSpec,
    ExcludedByExtension,
    ExcludedBySpec
  }

}
