using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using Org.WebApi;
using Org.WebApi.Cache;
using Org.DB;
using Org.GS.Configuration;
using Org.GS.Logging;
using Org.GS.Network;
using Org.GS;

namespace Org.OpsControlApi.Services
{
  public class ServiceBase : IDisposable
  {
    private static ServiceCache _cache = new ServiceCache();
    public static ServiceCache Cache {
      get {
        return _cache;
      }
    }

    public Logger Logger {
      get {
        return _controller.Logger;
      }
    }

    private WebContext _webContext;
    protected WebContext WebContext {
      get {
        return _webContext;
      }
    }

    private ControllerBase _controller;
    protected ControllerBase Controller {
      get {
        return _controller;
      }
    }

    public ServiceBase(WebContext webContext, ControllerBase controller)
    {
      _webContext = webContext;
      _controller = controller;
    }

    public static TaskResult SendEmail(EmailMessage emailMessage)
    {
      var taskResult = new TaskResult("SendEmail");

      try
      {
        using (var emailHelper = new EmailHelper())
        {
          var smtpSpec = g.GetSmtpSpec("Default");
          if (!smtpSpec.ReadyToTestEmail())
          {
            taskResult.TaskResultStatus = TaskResultStatus.Failed;
            taskResult.Message = "SmtpSpec is not ready for sending email.";
            taskResult.EndDateTime = DateTime.Now;
            return taskResult;
          }

          var mailMessage = new System.Net.Mail.MailMessage();

          string substituteEmailTarget = g.CI(g.SystemInfo.ComputerName + "SubstituteEmailTarget");
          if (substituteEmailTarget.IsNotBlank())
            mailMessage.To.Add(substituteEmailTarget);
          else
          {
            foreach (var address in emailMessage.ToAddresses)
              mailMessage.To.Add(address);
          }

          mailMessage.From = new System.Net.Mail.MailAddress(emailMessage.FromAddress);

          mailMessage.Subject = emailMessage.Subject;
          mailMessage.Body = emailMessage.Body;
          mailMessage.IsBodyHtml = emailMessage.IsBodyHtml;

          SmtpParmsSet smtpParmsSet = new SmtpParmsSet();
          SmtpParms smtpParms = new SmtpParms();
          smtpParms.SmtpServer = smtpSpec.SmtpServer;
          smtpParms.SmtpPort = Int32.Parse(smtpSpec.SmtpPort);
          smtpParms.SmtpUserID = smtpSpec.SmtpUserID;
          smtpParms.SmtpPassword = smtpSpec.SmtpPassword;
          smtpParms.UseSmtpCredentials = true;
          smtpParms.SmtpEnableSSL = smtpSpec.EnableSSL;
          smtpParms.PickUpFromIIS = false;
          smtpParms.UseSmtpCredentials = true;
          smtpParms.SuppressEmailSend = false;
          smtpParmsSet.Add(smtpParms);

          emailHelper.SendEmail(smtpParmsSet, mailMessage);
        }

        taskResult.TaskResultStatus = TaskResultStatus.Success;
        taskResult.Message = "Sending email was successful.";
        taskResult.EndDateTime = DateTime.Now;
        return taskResult;
      }
      catch (Exception ex)
      {
        taskResult.TaskResultStatus = TaskResultStatus.Failed;
        taskResult.Message = "Sending email failed.";
        taskResult.Code = 4999;
        taskResult.FullErrorDetail = ex.ToReport();
        taskResult.Exception = ex;
        taskResult.EndDateTime = DateTime.Now;
        return taskResult;
      }
    }

    public static void LoadCache()
    {
      try
      {
        using (var repoBase = new RepositoryBase(g.ConnectionStringName, "Adsdi_Org"))
        {
          // get account statuses
          //var accountStatusList = repoBase.GetList<Bus.AccountStatus>();
          var accountStatusValues = new Dictionary<int, string>();
          //foreach(var accountStatus in accountStatusList)
          //{
          //  if (!accountStatusValues.ContainsKey(accountStatus.AccountStatusId))
          //    accountStatusValues.Add(accountStatus.AccountStatusId, accountStatus.AccountStatus1);
          //}
          Cache["AccountStatus"] = accountStatusValues;

          // get resume statuses
          //var resumeStatusList = repoBase.GetList<Bus.ResumeStatus>();
          var resumeStatusValues = new Dictionary<int, string>();
          //foreach(var resumeStatus in resumeStatusList)
          //{
          //  if (!resumeStatusValues.ContainsKey(resumeStatus.ResumeStatusId))
          //    resumeStatusValues.Add(resumeStatus.ResumeStatusId, resumeStatus.ResumeStatusDesc);
          //}
          Cache["ResumeStatus"] = resumeStatusValues;


          //var eventModelList = repoBase.GetList<Bus.Event>();
          var eventValues = new Dictionary<int, string>();
          //foreach(var eventModel in eventModelList)
          //{
          //  if (!eventValues.ContainsKey(eventModel.EventCode))
          //    eventValues.Add(eventModel.EventCode, eventModel.EventDesc);
          //}
          Cache["Event"] = eventValues;
        }
      }
      catch(Exception ex)
      {
        throw new Exception("An exception occurred attempting to build the Service Cache.", ex);
      }
    }

    public static void ClearCache()
    {
      Cache.Clear();
    }


    public void Log(int eventCode)
    {
      if (_controller.Logger == null)
        return;

      string logMessage = GetEventMessage(eventCode);

      _controller.Logger.Log(logMessage, _controller.SessionId, eventCode, GetOrgId(), GetAccountId(), 0, 0);
    }

    public void Log(int eventCode, int entityTypeId, int entityId)
    {
      if (_controller.Logger == null)
        return;

      string logMessage = GetEventMessage(eventCode);

      _controller.Logger.Log(logMessage, _controller.SessionId, eventCode, GetOrgId(), GetAccountId(), entityTypeId, entityId);
    }

    public void Log(LogSeverity logSeverity, int eventCode, int entityTypeId, int entityId)
    {
      if (_controller.Logger == null)
        return;

      string logMessage = GetEventMessage(eventCode);

      _controller.Logger.Log(logSeverity, logMessage, _controller.SessionId, eventCode, GetOrgId(), GetAccountId(), entityTypeId, entityId);
    }

    public void Log(LogSeverity logSeverity, int eventCode, int entityTypeId, int entityId, Exception ex)
    {
      if (_controller.Logger == null)
        return;

      string logMessage = GetEventMessage(eventCode);

      _controller.Logger.Log(logSeverity, logMessage, _controller.SessionId, eventCode, GetOrgId(), GetAccountId(), entityTypeId, entityId, ex);
    }

    public string GetEventMessage(int eventCode)
    {
      var eventDict = Cache.AsDictionary("Event");

      string eventMessage = String.Empty;
      if (eventDict.ContainsKey(eventCode))
      {
        eventMessage = eventDict[eventCode];
      }
      else
      {
        eventCode = 9999;
        eventMessage = Cache.AsDictionary("Event")[eventCode];
      }

      return eventMessage;
    }

    public int GetOrgId()
    {
      int orgId = 0;
      if (_controller.OrgId > -1)
        orgId = _controller.OrgId;

      return orgId;
    }

    public int GetAccountId()
    {
      int accountId = 0;
      if (_controller.AccountId > -1)
        accountId = _controller.AccountId;

      return accountId;
    }

    public virtual void Dispose()
    {
    }
  }
}