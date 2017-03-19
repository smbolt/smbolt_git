using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Org.GS;
using Org.GS.Configuration;
using Org.WSO;
using Org.WSO.Transactions;
using Microsoft.Build.Construction;
using Microsoft.Build.Evaluation;
using Microsoft.Build.Execution;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace Org.AX
{
  [ObfuscationAttribute(Exclude = true, ApplyToMembers = true)]
  [XMap(XType = XType.Element)]
  public class Axion
  {
    [XMap(IsKey = true)]
    public string Name { get; set; }

    [XMap(IsRequired = true)]
    public AxionType AxionType { get; set; }

    [XMap(DefaultValue = "True")]
    public bool IsActive { get; set; }

    [XMap]
    public string Src { get; set; }

    [XMap]
    public string Tgt { get; set; }

    [XMap(DefaultValue = "True")]
    public bool Overwrite { get; set; }

    [XMap(DefaultValue = "False")]
    public bool Recursive { get; set; }

    [XMap(XType = XType.Element, ClassName = "FileSet", WrapperElement = "IncludedFileSet", CollectionElements = "File")]
    public FileSet IncludedFileSet { get; set; }

    [XMap(XType = XType.Element, ClassName = "FileSet", WrapperElement = "ExcludedFileSet", CollectionElements = "File")]
    public FileSet ExcludedFileSet { get; set; }

    private FileMatchCriteria _fileMatchCriteria;

    public bool IsDryRun { get; set; }

    public AxResult AxResult { get; set; }

    private FileSystemUtility _fsu;
    private ServiceManager _svcMgr;
    private SiteManager _siteMgr;
    private AppPoolManager _appPoolMgr;

    public Axion()
    {
      this.Name = String.Empty;
      this.AxionType = AxionType.NotSet;
      this.IsActive = true;
      this.Src = String.Empty;
      this.Tgt = String.Empty;
      this.Overwrite = false;
      this.Recursive = false;
      this.IncludedFileSet = new FileSet();
      this.ExcludedFileSet = new FileSet();
      this.AxResult = new AxResult(this);
      this.IsDryRun = false;
    }

    public TaskResult Run(bool isDryRun)
    {
      _fileMatchCriteria = new FileMatchCriteria(this.IncludedFileSet, this.ExcludedFileSet);
      this.IsDryRun = this.AxResult.IsDryRun = isDryRun;

      _fsu = new FileSystemUtility(this.IsDryRun);
      _svcMgr = new ServiceManager(this.IsDryRun);
      _siteMgr = new SiteManager(this.IsDryRun);
      _appPoolMgr = new AppPoolManager(this.IsDryRun);


      try { ValidateSrcAndTgt(); }
      catch (Exception ex)
      {
        return new TaskResult("ValidateSrcAndTgt").Failed(ex.Message);
      }

      this.AxResult.HasRun = true;
      switch (this.AxionType)
      {
        case AxionType.CopyFiles: return CopyFiles();
        case AxionType.MoveFiles: return MoveFiles();
        case AxionType.DeleteFiles: return DeleteFiles();
        case AxionType.DeleteAllFolderContents: return DeleteAllFolderContents();
        case AxionType.DeleteRecursive: return DeleteRecursive();
        case AxionType.StartLocalWinService: return _svcMgr.StartWinService(this.Tgt);
        case AxionType.StopLocalWinService: return _svcMgr.StopWinService(this.Tgt);
        case AxionType.PauseLocalWinService: return _svcMgr.PauseWinService(this.Tgt);
        case AxionType.ResumeLocalWinService: return _svcMgr.ResumeWinService(this.Tgt);
        case AxionType.GetLocalWinServiceStatus: return _svcMgr.GetWinServiceStatus(this.Tgt);
        case AxionType.GetLocalWinServiceInfo: return GetWinServiceInfo();
        case AxionType.GetLocalWinServiceList: return GetWinServiceList();
        case AxionType.StartLocalWebSite: return _siteMgr.StartStopWebSite(this.Tgt, SiteCommand.Start);
        case AxionType.StopLocalWebSite: return _siteMgr.StartStopWebSite(this.Tgt, SiteCommand.Stop);
        case AxionType.GetLocalWebSiteStatus: return _siteMgr.GetWebSiteStatus(this.Tgt);
        case AxionType.GetLocalWebSiteInfo: return GetWebSiteInfo();
        case AxionType.GetLocalWebSiteList: return GetWebSiteList();
        case AxionType.StartLocalAppPool: return _appPoolMgr.StartStopAppPool(this.Tgt, AppPoolCommand.Start);
        case AxionType.StopLocalAppPool: return _appPoolMgr.StartStopAppPool(this.Tgt, AppPoolCommand.Stop);
        case AxionType.GetLocalAppPoolStatus: return _appPoolMgr.GetAppPoolStatus(this.Tgt);
        case AxionType.GetLocalAppPoolInfo: return GetAppPoolInfo();
        case AxionType.GetLocalAppPoolList: return GetAppPoolList();

        case AxionType.StartRemoteWinService:
        case AxionType.StopRemoteWinService:
        case AxionType.PauseRemoteWinService:
        case AxionType.ResumeRemoteWinService:
        case AxionType.GetRemoteWinServiceStatus:
        case AxionType.GetRemoteWinServiceInfo:
        case AxionType.GetRemoteWinServiceList:
        case AxionType.StartRemoteWebSite:
        case AxionType.StopRemoteWebSite:
        case AxionType.GetRemoteWebSiteStatus:
        case AxionType.GetRemoteWebSiteInfo:
        case AxionType.GetRemoteWebServiceInfo:
        case AxionType.GetRemoteWebSiteList:
        case AxionType.StartRemoteAppPool:
        case AxionType.StopRemoteAppPool:
        case AxionType.GetRemoteAppPoolStatus:
        case AxionType.GetRemoteAppPoolInfo:
        case AxionType.GetRemoteAppPoolList:
          return RunThroughWebService();

        default:
          return new TaskResult(this.AxionType.ToString()).Failed("Axion is not configured to run AxionType '" + this.AxionType + "'.");
      }
    }

    private TaskResult CopyFiles()
    {
      TaskResult taskResult = new TaskResult("Axion:" + this.Name + "(" + this.AxionType.ToString() + ")");
      taskResult.IsDryRun = this.IsDryRun;
      try
      {
        this.AxResult.FilesCopied = _fsu.CopyFiles(Src, Tgt, _fileMatchCriteria, this.Overwrite);

        taskResult.Object = this.AxResult;
        return taskResult.Success();
      }
      catch (Exception ex)
      {
        return taskResult.Failed("An exception occurred while attempting to run Axion named '" + this.Name + "' with AxionType '" + this.AxionType.ToString() + "'.", ex);
      }
    }

    private TaskResult MoveFiles()
    {
      TaskResult taskResult = new TaskResult("Axion:" + this.Name + "(" + this.AxionType.ToString() + ")");
      taskResult.IsDryRun = this.IsDryRun;
      try
      {
        this.AxResult.FilesMoved = _fsu.MoveFiles(Src, Tgt, _fileMatchCriteria, this.Overwrite);

        taskResult.Object = this.AxResult;
        return taskResult.Success();
      }
      catch (Exception ex)
      {
        return taskResult.Failed("An exception occurred while attempting to run Axion named '" + this.Name + "' with AxionType '" + this.AxionType.ToString() + "'.", ex);
      }
    }

    private TaskResult DeleteFiles()
    {
      TaskResult taskResult = new TaskResult("Axion:" + this.Name + "(" + this.AxionType.ToString() + ")");
      taskResult.IsDryRun = this.IsDryRun;
      try
      {
        this.AxResult.HasRun = true;
        this.AxResult.FilesDeleted = _fsu.DeleteFiles(Tgt, _fileMatchCriteria);

        taskResult.Object = this.AxResult;
        return taskResult.Success();
      }
      catch (Exception ex)
      {
        return taskResult.Failed("An exception occurred while attempting to run Axion named '" + this.Name + "' with AxionType '" + this.AxionType.ToString() + "'.", ex);
      }
    }

    private TaskResult DeleteAllFolderContents()
    {
      TaskResult taskResult = new TaskResult("Axion:" + this.Name + "(" + this.AxionType.ToString() + ")");
      taskResult.IsDryRun = this.IsDryRun;

      try
      {
        taskResult.Object = this.AxResult;
        return taskResult.Success();
      }
      catch (Exception ex)
      {
        return taskResult.Failed("An exception occurred while attempting to run Axion named '" + this.Name + "' with AxionType '" + this.AxionType.ToString() + "'.", ex);
      }
    }

    private TaskResult DeleteRecursive()
    {
      TaskResult taskResult = new TaskResult("Axion:" + this.Name + "(" + this.AxionType.ToString() + ")");
      taskResult.IsDryRun = this.IsDryRun;

      try
      {
        _fsu.DeleteDirectoryRecursive(Tgt);
        taskResult.Object = this.AxResult;
        return taskResult.Success();
      }
      catch (Exception ex)
      {
        return taskResult.Failed("An exception occurred while attempting to run Axion named '" + this.Name + "' with AxionType '" + this.AxionType.ToString() + "'.", ex);
      }
    }

    private TaskResult GetWinServiceInfo()
    {
      TaskResult taskResult = new TaskResult("Axion:" + this.Name + "(" + this.AxionType.ToString() + ")");
      taskResult.IsDryRun = this.IsDryRun;
      try
      {
        
        WinService winService = _svcMgr.GetWinService(this.Tgt);

        if (winService == null)
          return taskResult.Failed("The requested Windows Service does not exist.");

        string winServiceInfo = "Win Service '" + winService.Name + "' on Machine '" + winService.MachineName + "' " + g.crlf +
                                "Status:    " + winService.WinServiceStatus + g.crlf +
                                "Can Pause: " + winService.CanPauseAndContinue + g.crlf +
                                "Can Stop:  " + winService.CanStop;

        taskResult.Object = winService;
        return taskResult.Success(winServiceInfo);
      }
      catch (Exception ex)
      {
        return taskResult.Failed("An exception occurred while getting Windows Service info." + g.crlf2 + ex.ToReport());
      }
    }

    private TaskResult GetWinServiceList()
    {
      TaskResult taskResult = new TaskResult("Axion:" + this.Name + "(" + this.AxionType.ToString() + ")");
      taskResult.IsDryRun = this.IsDryRun;
      try
      {
        
        WinServiceSet winServices = _svcMgr.GetWinServices();

        StringBuilder serviceMessage = new StringBuilder();
        serviceMessage.Append("Status".PadTo(10) + "Service Name" + g.crlf);
        serviceMessage.Append("---------------------------------------");
        foreach (var winService in winServices)
          serviceMessage.Append(g.crlf + winService.WinServiceStatus.ToString().PadTo(10) + winService.Name);

        taskResult.Object = winServices;
        return taskResult.Success(serviceMessage.ToString());
      }
      catch (Exception ex)
      {
        return taskResult.Failed("An exception occurred while getting a list of Windows Services." + g.crlf2 + ex.ToReport());
      }
    }

    private TaskResult GetWebSiteInfo()
    {
      TaskResult taskResult = new TaskResult("Axion:" + this.Name + "(" + this.AxionType.ToString() + ")");
      taskResult.IsDryRun = this.IsDryRun;
      try
      {
        WebSite webSite = _siteMgr.GetWebSite(this.Tgt);

        if (webSite == null)
          return taskResult.Failed("The Web Site '" + this.Tgt + "' does not exist.");

        taskResult.Object = webSite;
        return taskResult.Success("Web Site '" + webSite.Name + "' with ID '" + webSite.Id + "' is currently '" + webSite.WebSiteStatus.ToString() + "'.");
      }
      catch (Exception ex)
      {
        return taskResult.Failed("An exception occurred while getting Web Site info." + g.crlf2 + ex.ToReport());
      }
    }

    private TaskResult GetWebSiteList()
    {
      TaskResult taskResult = new TaskResult("Axion:" + this.Name + "(" + this.AxionType.ToString() + ")");
      taskResult.IsDryRun = this.IsDryRun;
      try
      {
        WebSiteSet webSites = _siteMgr.GetWebSites();
        StringBuilder siteMessage = new StringBuilder();
        siteMessage.Append("Site ID  Site Name".PadTo(39) + "Status" + g.crlf);
        siteMessage.Append("-----------------------------------------------------");

        foreach (WebSite webSite in webSites)
          siteMessage.Append(g.crlf + webSite.Id.ToString().PadTo(9) + webSite.Name.PadTo(30) + webSite.WebSiteStatus.ToString());

        return taskResult.Success(siteMessage.ToString());
      }
      catch (Exception ex)
      {
        return taskResult.Failed("An exception occurred while getting a list of Web Sites." + g.crlf2 + ex.ToReport());
      }
    }

    private TaskResult GetAppPoolInfo()
    {
      TaskResult taskResult = new TaskResult("Axion:" + this.Name + "(" + this.AxionType.ToString() + ")");
      taskResult.IsDryRun = this.IsDryRun;
      try
      {
        AppPool appPool = _appPoolMgr.GetAppPool(this.Tgt);

        if (appPool == null)
          return taskResult.Failed("The App Pool '" + this.Tgt + "' does not exist.");

        return taskResult.Success("App Pool '" + appPool.Name + "' is currently '" + appPool.AppPoolStatus.ToString() + "'." + g.crlf +
                                  "AutoStart: " + appPool.AutoStart + g.crlf +
                                  "Enable32BitAppOnWin64: " + appPool.Enable32BitAppOnWin64);
      }
      catch (Exception ex)
      {
        return taskResult.Failed("An exception occurred while getting App Pool info." + g.crlf2 + ex.ToReport());
      }
    }

    private TaskResult GetAppPoolList()
    {
      TaskResult taskResult = new TaskResult("Axion:" + this.Name + "(" + this.AxionType.ToString() + ")");
      taskResult.IsDryRun = this.IsDryRun;
      try
      {
        AppPoolSet appPools = _appPoolMgr.GetAppPools();
        StringBuilder appPoolMessage = new StringBuilder();
        appPoolMessage.Append("Status".PadTo(9) + "App Pool Name" + g.crlf);
        appPoolMessage.Append("-----------------------------------------------------");

        foreach (AppPool appPool in appPools)
          appPoolMessage.Append(g.crlf + appPool.AppPoolStatus.ToString().PadTo(9) + appPool.Name);

        return taskResult.Success(appPoolMessage.ToString());
      }
      catch (Exception ex)
      {
        return taskResult.Failed("An exception occurred while getting  a list of App Pools." + g.crlf2 + ex.ToReport());
      }
    }

    public TaskResult RunThroughWebService()
    {
      var taskResult = new TaskResult("Axion:" + this.Name + "(" + this.AxionType.ToString() + ")");
      try
      {
        var axResult = new AxResult(this);
        axResult.HasRun = true;
        taskResult.Object = axResult;

        WsMessage responseMessage = SendRequestMessage();

        ObjectFactory2 f = new ObjectFactory2();

        switch (responseMessage.TransactionHeader.TransactionName)
        {
          case "ErrorResponse":
            ErrorResponse errorResponse = f.Deserialize(responseMessage.TransactionBody) as ErrorResponse;
            string errorResponseMessage = errorResponse.Message;
            errorResponseMessage += errorResponse.HasException ? (g.crlf + errorResponse.WsException.ToReport()) : (g.crlf + "No exception" + g.crlf);
            return taskResult.Failed(errorResponseMessage);

          case "WsCommand":
            WsCommandResponse commandResponse = f.Deserialize(responseMessage.TransactionBody) as WsCommandResponse;
            foreach (WsCommand wsCommand in commandResponse.WsCommandSet)
            {
              if (wsCommand.TaskResultStatus == TaskResultStatus.Failed)
                return taskResult.Failed(wsCommand.Message);

              if (wsCommand.ObjectWrapper != null && wsCommand.ObjectWrapper.Object.IsNotBlank() && wsCommand.ObjectWrapper.AssemblyQualifiedName.IsNotBlank())
              {
                object obj = wsCommand.ObjectWrapper.GetObject();
                switch (wsCommand.ObjectWrapper.ObjectTypeShort)
                {
                  case "WebSite":
                    axResult.WebSitesAffected.Add(obj as WebSite);
                    break;

                  case "WinService":
                    axResult.WinServicesAffected.Add(obj as WinService);
                    break;

                  case "AppPool":
                    axResult.AppPoolsAffected.Add(obj as AppPool);
                    break;

                  default:
                    return taskResult.Failed("The Type '" + wsCommand.ObjectWrapper.ObjectTypeShort + "' in ObjectWrapper is was an unexcpected Type for the command " + wsCommand.WsCommandName);
                }
              }
              axResult.ReportBase += wsCommand.Message;
            }
            return taskResult.Success();

          default:
            return taskResult.Failed("Unrecognized TransactionName '" + responseMessage.TransactionHeader.TransactionName + "' was returned in the response WsMessage.");
        }
      }
      catch (Exception ex)
      {
        return taskResult.Failed("An exception occurred while attempting to run Axion named '" + this.Name + "' with AxionType '" + this.AxionType.ToString() + "'.", ex);
      }
    }

    private WsMessage SendRequestMessage()
    {
      string[] hostPortService = Src.Split('.');
      string host = hostPortService[0];
      string port = hostPortService[1];
      string webService = hostPortService[2] + ".svc";

      ConfigWsSpec configWsSpec = new ConfigWsSpec(WebServiceBinding.BasicHttp, host, port, webService);
      WsParms wsParms = new WsParms();
      wsParms.TransactionName = "WsCommand";
      wsParms.TransactionVersion = "1.0.0.0";
      wsParms.MessagingParticipant = MessagingParticipant.Sender;
      wsParms.ConfigWsSpec = configWsSpec;
      wsParms.TrackPerformance = false;

      wsParms.DomainName = g.SystemInfo.DomainName;
      wsParms.MachineName = g.SystemInfo.ComputerName;
      wsParms.UserName = g.SystemInfo.UserName;
      wsParms.ModuleCode = g.AppInfo.ModuleCode;
      wsParms.ModuleName = g.AppInfo.ModuleName;
      wsParms.ModuleVersion = g.AppInfo.AppVersion;
      wsParms.AppName = g.AppInfo.AppName;
      wsParms.AppVersion = g.AppInfo.AppVersion;

      wsParms.ModuleCode = 1000;
      wsParms.ModuleName = "migr";
      wsParms.ModuleVersion = "1.0.0.0";
      wsParms.OrgId = 3;

      WsCommand wsCommand = new WsCommand();
      wsCommand.WsCommandName = GetWsCommandName();

      wsCommand.Message = Tgt;
      //wsCommand.Parms.Add("ServiceName", txtParameters.Text);
      Parm parm = new Parm("WsCommand", wsCommand);
      parm.ParameterType = typeof(WsCommand);
      wsParms.ParmSet.Add(parm);

      WsMessage requestMessage = new MessageFactory().CreateRequestMessage(wsParms);

      return WsClient.InvokeServiceCall(wsParms, requestMessage);
    }

    private WsCommandName GetWsCommandName()
    {
      string name = this.AxionType.ToString().Replace("Local", String.Empty).Replace("Remote", String.Empty);

      WsCommandName wsCommandName = name.ToEnum<WsCommandName>(WsCommandName.NotSet);

      if (wsCommandName == WsCommandName.NotSet)
        throw new Exception("Failed to convert AxionType '" + this.AxionType.ToString() + "' to WsCommandName.");

      return wsCommandName;
    }

    //private TaskResult VerifyWebSite(SiteManager siteMgr, string host, string service)
    //{
    //  TaskResult taskResult = new TaskResult("Axion" + this.Name + "(" + this.AxionType.ToString() + ")");
    //  try
    //  {



    //    return taskResult.Success();
    //  }
    //  catch (Exception ex)
    //  {
    //    return taskResult.Failed("An exception occurred while attempting to run Axion named '" + this.Name + "' with AxionType '" + this.AxionType.ToString() + "'.", ex);
    //  }
    //}

    private void ValidateSrcAndTgt()
    {
      switch (this.AxionType)
      {
        case AxionType.CopyFiles:
        case AxionType.MoveFiles:
          if (!_fsu.IsDirectory(this.Src))
            throw new Exception("Src '" + this.Src + "' is not an existing directory, so AxionType '" + this.AxionType.ToString() + "' could not be run.");
          break;
        case AxionType.DeleteFiles:
        case AxionType.DeleteAllFolderContents:
        case AxionType.DeleteRecursive:
          if (!_fsu.IsDirectory(this.Tgt))
            throw new Exception("Tgt '" + this.Tgt + "' is not an existing directory, so AxionType '" + this.AxionType.ToString() + "' could not be run.");
          break;

        case AxionType.StartLocalWinService:
        case AxionType.StopLocalWinService:
        case AxionType.PauseLocalWinService:
        case AxionType.ResumeLocalWinService:
        case AxionType.GetLocalWinServiceStatus:
        case AxionType.GetLocalWinServiceInfo:
        case AxionType.StartLocalWebSite:
        case AxionType.StopLocalWebSite:
        case AxionType.GetLocalWebSiteStatus:
        case AxionType.GetLocalWebSiteInfo:
        case AxionType.StartLocalAppPool:
        case AxionType.StopLocalAppPool:
        case AxionType.GetLocalAppPoolStatus:
        case AxionType.GetLocalAppPoolInfo:
          if (this.Tgt.IsBlank())
            throw new Exception("Tgt cannot be blank when running AxionType '" + this.AxionType.ToString() + "'.");
          break;

        case AxionType.StartRemoteWinService:
        case AxionType.StopRemoteWinService:
        case AxionType.PauseRemoteWinService:
        case AxionType.ResumeRemoteWinService:
        case AxionType.GetRemoteWinServiceStatus:
        case AxionType.GetRemoteWinServiceInfo:
        case AxionType.StartRemoteWebSite:
        case AxionType.StopRemoteWebSite:
        case AxionType.GetRemoteWebSiteStatus:
        case AxionType.GetRemoteWebSiteInfo:
        case AxionType.StartRemoteAppPool:
        case AxionType.StopRemoteAppPool:
        case AxionType.GetRemoteAppPoolStatus:
        case AxionType.GetRemoteAppPoolInfo:
          if (this.Tgt.IsBlank())
            throw new Exception("Tgt cannot be blank when running AxionType '" + this.AxionType.ToString() + "'.");
          if (this.Src.Split('.').Count() != 3)
            throw new Exception("Invalid Src format for AxionType '" + this.AxionType.ToString() + "' to run through WebService. Correct format is [HOST].[PORT].[WEBSERVICE-NAME]");
          break;

        case AxionType.GetRemoteWebServiceInfo:
        case AxionType.GetRemoteWinServiceList:
        case AxionType.GetRemoteWebSiteList:
        case AxionType.GetRemoteAppPoolList:
          if (this.Src.Split('.').Count() != 3)
            throw new Exception("Invalid Src format for AxionType '" + this.AxionType.ToString() + "' to run through WebService. Correct format is [HOST].[PORT].[WEBSERVICE-NAME]");
          break;

      }
    }
  }
}
