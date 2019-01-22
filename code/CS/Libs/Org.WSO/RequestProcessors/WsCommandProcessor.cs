using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using System.Reflection;
using System.Xml.Linq;
using System.ServiceProcess;
using System.DirectoryServices;
using System.Management;
using Org.GS;
using Org.WSO.Transactions;

namespace Org.WSO
{
  [Serializable]
  public class WsCommandProcessor : RequestProcessorBase, IRequestProcessor
  {
    private TransactionStatus _summaryStatus = TransactionStatus.Success;
    private ServiceManager _serviceManager = new ServiceManager();
    private SiteManager _siteManager = new SiteManager();
    private AppPoolManager _appPoolManager = new AppPoolManager();

    public override XElement ProcessRequest()
    {
      try
      {
        DateTime beginRequestDT = DateTime.Now;
        WsCommandResponse commandResponse = new WsCommandResponse();
        WsCommandRequest commandRequest = null;

        base.Initialize(MethodBase.GetCurrentMethod());

        ObjectFactory2 f = new ObjectFactory2();
        commandRequest = f.Deserialize(base.TransactionEngine.TransactionBody) as WsCommandRequest;

        var taskResult = new TaskResult();

        foreach (WsCommand command in commandRequest.WsCommandSet)
        {
          try
          {
            ValidateParms(command);

            if (command.BeforeWaitMilliseconds > 0)
              System.Threading.Thread.Sleep(command.BeforeWaitMilliseconds);

            switch (command.WsCommandName)
            {
              case WsCommandName.StartWinService:
                taskResult = _serviceManager.StartWinService(command.Parms["WinServiceName"].Trim());
                break;

              case WsCommandName.StopWinService:
                taskResult = _serviceManager.StopWinService(command.Parms["WinServiceName"].Trim());
                break;

              case WsCommandName.PauseWinService:
                taskResult = _serviceManager.PauseWinService(command.Parms["WinServiceName"].Trim());
                break;

              case WsCommandName.ResumeWinService:
                taskResult = _serviceManager.ResumeWinService(command.Parms["WinServiceName"].Trim());
                break;

              case WsCommandName.GetWinServiceStatus:
                taskResult = _serviceManager.GetWinServiceStatus(command.Parms["WinServiceName"].Trim());
                break;

              case WsCommandName.GetWinServiceInfo:
                taskResult = GetWinServiceInfo(command);
                break;

              case WsCommandName.GetWinServiceList:
                taskResult = GetWinServiceList();
                break;

              case WsCommandName.GetWinServices:
                taskResult = GetWinServices();
                break;

              case WsCommandName.StartWebSite:
                taskResult = _siteManager.StartStopWebSite(command.Parms["WebSiteName"].Trim(), SiteCommand.Start);
                break;

              case WsCommandName.StopWebSite:
                taskResult = _siteManager.StartStopWebSite(command.Parms["WebSiteName"].Trim(), SiteCommand.Stop);
                break;

              case WsCommandName.GetWebSiteStatus:
                taskResult = _siteManager.GetWebSiteStatus(command.Parms["WebSiteName"].Trim());
                break;

              case WsCommandName.GetWebSiteInfo:
                taskResult = GetWebSiteInfo(command);
                break;

              case WsCommandName.GetWebServiceInfo:
                taskResult = GetWebServiceInfo();
                break;

              case WsCommandName.GetWebSiteList:
                taskResult = GetWebSiteList();
                break;

              case WsCommandName.GetWebSites:
                taskResult = GetWebSites();
                break;

              case WsCommandName.StartAppPool:
                taskResult = _appPoolManager.StartStopAppPool(command.Parms["AppPoolName"].Trim(), AppPoolCommand.Start);
                break;

              case WsCommandName.StopAppPool:
                taskResult = _appPoolManager.StartStopAppPool(command.Parms["AppPoolName"].Trim(), AppPoolCommand.Stop);
                break;

              case WsCommandName.GetAppPoolStatus:
                taskResult = _appPoolManager.GetAppPoolStatus(command.Parms["AppPoolName"].Trim());
                break;

              case WsCommandName.GetAppPoolInfo:
                taskResult = GetAppPoolInfo(command);
                break;

              case WsCommandName.GetAppPoolList:
                taskResult = GetAppPoolList(command);
                break;

              case WsCommandName.GetAppPools:
                taskResult = GetAppPools();
                break;

              case WsCommandName.SendServiceCommand:
                taskResult = SendServiceCommand(command);
                break;

              case WsCommandName.GetAssemblyReport:
                bool includeAllAssemblies = true;
                if (command.Parms.ContainsKey("IncludeAllAssemblies"))
                  includeAllAssemblies = command.Parms["IncludeAllAssemblies"].ToBoolean();
                taskResult = GetAssemblyReport(includeAllAssemblies);
                break;

              case WsCommandName.GetAppDomainReport:
                taskResult = GetAppDomainReport();
                break;
            }

            if (command.AfterWaitMilliseconds > 0)
              System.Threading.Thread.Sleep(command.AfterWaitMilliseconds);

            WsCommand commandResult = new WsCommand();
            commandResult.WsCommandName = command.WsCommandName;
            commandResult.TaskResultStatus = taskResult.TaskResultStatus;
            commandResult.Message = taskResult.Message;
            commandResult.SetDuration(taskResult.BeginDateTime, DateTime.Now);
            commandResult.BeforeWaitMilliseconds = command.BeforeWaitMilliseconds;
            commandResult.AfterWaitMilliseconds = command.AfterWaitMilliseconds;
            commandResult.Parms = command.Parms;

            if (taskResult.Object != null)
              commandResult.ObjectWrapper = new ObjectWrapper(taskResult.Object);

            commandResponse.WsCommandSet.Add(commandResult);
            TransactionStatus commandStatus = g.ToEnum<TransactionStatus>(g.TranslateStatus(taskResult.TaskResultStatus), TransactionStatus.NotSet);
            UpdateSummaryStatus(commandStatus);
          }
          catch (Exception ex)
          {
            WsCommand commandResult = new WsCommand();
            commandResult.WsCommandName = command.WsCommandName;
            commandResult.TaskResultStatus = TaskResultStatus.Failed;
            commandResult.Message = ex.ToReport();
            commandResult.SetDuration(taskResult.BeginDateTime, DateTime.Now);
            commandResult.BeforeWaitMilliseconds = command.BeforeWaitMilliseconds;
            commandResult.AfterWaitMilliseconds = command.AfterWaitMilliseconds;
            commandResult.Parms = command.Parms;

            commandResponse.WsCommandSet.Add(commandResult);
            TransactionStatus commandStatus = g.ToEnum<TransactionStatus>(g.TranslateStatus(taskResult.TaskResultStatus), TransactionStatus.NotSet);
            UpdateSummaryStatus(commandStatus);
            break;
          }
        }

        commandResponse.TransactionStatus = _summaryStatus;

        if (_summaryStatus == TransactionStatus.Success)
          commandResponse.Message = "All commands were processed successfully.";
        else
          commandResponse.Message = "One or more commands did not complete successfully.";

        DateTime endRequestDT = DateTime.Now;
        commandResponse.WsCommandSet.SetDuration(beginRequestDT, endRequestDT);

        //base.WriteSuccessLog("0000", "000");

        XElement responseElement = f.Serialize(commandResponse);
        return responseElement;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred when attempting to process the WsCommandRequest.", ex);
      }
    }

    private void UpdateSummaryStatus(TransactionStatus commandStatus)
    {
      if (commandStatus != TransactionStatus.Success)
        _summaryStatus = TransactionStatus.Failed;
    }

    private TaskResult GetWinServiceInfo(WsCommand command)
    {
      try
      {
        TaskResult taskResult = new TaskResult("GetWinServiceInfo");
        WinService winService = _serviceManager.GetWinService(command.Parms["WinServiceName"].Trim());

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
        throw new Exception("An exception occurred while getting Windows Service info.", ex);
      }
    }

    private TaskResult GetWinServiceList()
    {
      try
      {
        TaskResult taskResult = new TaskResult("GetWinServiceList");
        WinServiceSet winServices = _serviceManager.GetWinServices();

        StringBuilder serviceMessage = new StringBuilder();
        serviceMessage.Append("Status".PadTo(10) + "Service Name" + g.crlf);
        serviceMessage.Append("---------------------------------------");
        foreach (var winService in winServices)
          serviceMessage.Append(g.crlf + winService.WinServiceStatus.ToString().PadTo(10) + winService.Name);

        return taskResult.Success(serviceMessage.ToString());
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while getting a list of Windows Services.", ex);
      }
    }

    private TaskResult GetWinServices()
    {
      TaskResult taskResult = new TaskResult("GetWinServices");
      WinServiceSet winServices = _serviceManager.GetWinServices();
      taskResult.Object = winServices;

      return taskResult.Success();
    }

    private TaskResult GetWebSiteInfo(WsCommand command)
    {
      try
      {
        TaskResult taskResult = new TaskResult("GetWebSiteInfo");
        WebSite webSite = _siteManager.GetWebSite(command.Parms["WebSiteName"]);

        if (webSite == null)
          return taskResult.Failed("The Web Site '" + command.Parms["WebSiteName"] + "' does not exist.");

        taskResult.Object = webSite;
        return taskResult.Success("Web Site '" + webSite.Name + "' with ID '" + webSite.Id + "' is currently '" + webSite.WebSiteStatus.ToString() + "'.");
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while getting Web Site info", ex);
      }
    }

    private TaskResult GetWebServiceInfo()
    {
      try
      {
        TaskResult taskResult = new TaskResult("GetWebServiceInfo");

        string webServiceInfo = "Web Service '" + base.ServiceBase.AppName + "' running at URL '" +
                                base.ServiceBase.AbsoluteUri + "' is being hosted by '" + g.AppInfo.AppName + "' application type '" +
                                g.AppInfo.OrgApplicationType.ToString() + "' in module '" + g.AppInfo.ModuleName + "'.";

        return taskResult.Success(webServiceInfo);
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while getting Web Service info", ex);
      }
    }

    private TaskResult GetWebSiteList()
    {
      try
      {
        TaskResult taskResult = new TaskResult("GetWebSiteList");

        WebSiteSet webSites = _siteManager.GetWebSites();
        StringBuilder siteMessage = new StringBuilder();
        siteMessage.Append("Site ID  Site Name".PadTo(39) + "Status" + g.crlf);
        siteMessage.Append("-----------------------------------------------------");

        foreach (WebSite webSite in webSites)
          siteMessage.Append(g.crlf + webSite.Id.ToString().PadTo(9) + webSite.Name.PadTo(30) + webSite.WebSiteStatus.ToString());

        return taskResult.Success(siteMessage.ToString());
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while getting a list of Web Sites.", ex);
      }
    }

    private TaskResult GetWebSites()
    {
      TaskResult taskResult = new TaskResult("GetWebSites");
      WebSiteSet webSites = _siteManager.GetWebSites();
      taskResult.Object = webSites;

      return taskResult.Success();
    }

    private TaskResult GetAppPoolInfo(WsCommand command)
    {
      try
      {
        TaskResult taskResult = new TaskResult("GetAppPoolInfo");
        AppPool appPool = _appPoolManager.GetAppPool(command.Parms["AppPoolName"]);

        if (appPool == null)
          return taskResult.Failed("The App Pool '" + command.Parms["AppPoolName"] + "' does not exist.");

        return taskResult.Success("App Pool '" + appPool.Name + "' is currently '" + appPool.AppPoolStatus.ToString() + "'." + g.crlf +
                                  "AutoStart: " + appPool.AutoStart + g.crlf +
                                  "Enable32BitAppOnWin64: " + appPool.Enable32BitAppOnWin64);
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while getting App Pool info", ex);
      }
    }

    private TaskResult GetAppPoolList(WsCommand command)
    {
      try
      {
        TaskResult taskResult = new TaskResult("GetAppPoolList");

        AppPoolSet appPools = _appPoolManager.GetAppPools();
        StringBuilder appPoolMessage = new StringBuilder();
        appPoolMessage.Append("Status".PadTo(9) + "App Pool Name" + g.crlf);
        appPoolMessage.Append("-----------------------------------------------------");

        foreach (AppPool appPool in appPools)
          appPoolMessage.Append(g.crlf + appPool.AppPoolStatus.ToString().PadTo(9) + appPool.Name);

        return taskResult.Success(appPoolMessage.ToString());
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while getting  a list of App Pools.", ex);
      }
    }

    private TaskResult GetAppPools()
    {
      TaskResult taskResult = new TaskResult("GetAppPools");
      AppPoolSet appPools = _appPoolManager.GetAppPools();
      taskResult.Object = appPools;

      return taskResult.Success();
    }

    private TaskResult GetAssemblyReport(bool includeAllAssemblies)
    {
      TaskResult taskResult = new TaskResult("GetAssemblyReport");
      taskResult.Message = AssemblyHelper.GetAssemblyReport(includeAllAssemblies);
      return taskResult.Success();
    }

    private TaskResult GetAppDomainReport()
    {
      if (this.ServiceBase == null)
        return new TaskResult("GetAppDomainReport", "The ServiceBase reference of the WsCommandProcessor is null.", TaskResultStatus.Failed);

      return this.ServiceBase.GetAppDomainReport();
    }

    private TaskResult SendServiceCommand(WsCommand command)
    {
      TaskResult taskResult = new TaskResult("SendServiceCommand");

      IpdxMessage ipdxMessage = new IpdxMessage();
      ipdxMessage.Sender = base.ServiceBase.AppName;
      ipdxMessage.MessageType = IpdxMessageType.CommandSet;
      ipdxMessage.Recipient = "TaskEngine";
      //ipdxMessage.CommandSet.Add(command);

      base.ServiceBase.NotifyHost(ipdxMessage);

      taskResult.TaskResultStatus = TaskResultStatus.Success;
      taskResult.Message = "Ipdx command sent to TaskEngine;";
      taskResult.EndDateTime = DateTime.Now;
      return taskResult;
    }

    private void ValidateParms(WsCommand command)
    {
      switch (command.WsCommandName)
      {
        case WsCommandName.StartWinService:
        case WsCommandName.StopWinService:
        case WsCommandName.PauseWinService:
        case WsCommandName.ResumeWinService:
        case WsCommandName.GetWinServiceStatus:
        case WsCommandName.GetWinServiceInfo:
          if (!command.Parms.ContainsKey("WinServiceName"))
            throw new Exception("Windows Service name was not specified in " + command.WsCommandName.ToString() + " command.");

          if (command.Parms["WinServiceName"].IsBlank())
            throw new Exception("Windows Service name specified in " + command.WsCommandName.ToString() + " command is blank.");
          break;

        case WsCommandName.StartWebSite:
        case WsCommandName.StopWebSite:
        case WsCommandName.GetWebSiteStatus:
        case WsCommandName.GetWebSiteInfo:
          if (!command.Parms.ContainsKey("WebSiteName"))
            throw new Exception("Web Site name was not specified in " + command.WsCommandName.ToString() + " command.");

          if (command.Parms["WebSiteName"].IsBlank())
            throw new Exception("Web Site name specified in " + command.WsCommandName.ToString() + " command is blank.");
          break;

        case WsCommandName.StartAppPool:
        case WsCommandName.StopAppPool:
        case WsCommandName.GetAppPoolStatus:
        case WsCommandName.GetAppPoolInfo:
          if (!command.Parms.ContainsKey("AppPoolName"))
            throw new Exception("App Pool name was not specified in " + command.WsCommandName.ToString() + " command.");

          if (command.Parms["AppPoolName"].IsBlank())
            throw new Exception("App Pool name specified in " + command.WsCommandName.ToString() + " command is blank.");
          break;

      }
    }
  }
}
