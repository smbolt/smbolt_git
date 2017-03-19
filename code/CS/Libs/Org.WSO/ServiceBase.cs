using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Threading.Tasks;
using System.IO;
using System.ServiceModel;
using System.Web.Hosting;
using System.Reflection;
using System.Net;
using Org.Notify;
using Org.GS;
using Org.GS.Configuration;
using Org.GS.Logging;
using Org.GS.Notifications;
using Org.WSO.Transactions;

namespace Org.WSO
{
  public class ServiceBase : IDisposable
  {
    private int _entityId = 9999;
    public virtual int EntityId { get { return _entityId; } set { _entityId = value; } }
    protected ServiceState _serviceState; 
    private WsHost _wsHost;
    protected WsHost WsHost
    {
      get
      {
        if (_wsHost == null)
          _wsHost = this.GetWebServiceHost();
        return _wsHost;
      }
    }
    public string TransactionName { get; set; }
    public int OrgId { get; set; }
    public string AbsoluteUri { get; set; }
    public string IPAddress { get; set; }
    public int Port { get; set; }
    public string AppName { get; set; }
    public WsMessage ReceivedMessage { get; set; }
    public string ReceivedMessageString { get { return (ReceivedMessage != null ? ReceivedMessage.GetXml().ToString() : "NULL"); } }
    public WsMessage ResponseMessage { get; set; }
    public WsDateTime ReceiverReceiveTime { get; set; }
    public List<string> TraceLog { get; set; }  
    public Logger Logger; 
    private Encryptor _encryptor;
    public Encryptor Encryptor { get { return _encryptor; } }
    private NotificationsManager _notificationsManager;
    public string ServiceInfo { get { return Get_ServiceInfo(); } }
    public string ServiceIdentification { get { return Get_ServiceIdentification(); } }

    public ServiceBase()
    {
      try
      {
        _encryptor = new Encryptor();
        _notificationsManager = new NotificationsManager();

        StartupLogging.StartupLogPath = System.Web.Hosting.HostingEnvironment.MapPath("~") + @"StartupLog"; 
        StartupLogging.WriteStartupLog("Service Base at point 1");
        this.Logger = new Logger();
        this.Logger.ModuleId = g.AppInfo.ModuleCode;
        this.Port = OperationContext.Current.Host.BaseAddresses[0].Port;
        this.IPAddress = NetworkHelper.GetCurrentIpAddress();
        this.AppName = OperationContext.Current.Host.Description.Name;
        this.AbsoluteUri = OperationContext.Current.Host.BaseAddresses[0].AbsoluteUri;
        this.ReceiverReceiveTime = MessageFactory.GetWebServiceDateTime();
        this.ReceivedMessage = new WsMessage();
        this.ResponseMessage = new WsMessage();
        this.TraceLog = new List<string>();

        StartupLogging.WriteStartupLog("Service Base at point 2");

        _serviceState = ServiceContext.GetCurrent(this).ServiceState;

        StartupLogging.WriteStartupLog("Service Base at point 3");

        if (_serviceState.IsNew)
        {
          string message = this.ServiceIdentification + " is starting.";
          Logger.Log(message, 1001, this.EntityId);
          ProcessNotifications(message); 
        }

        StartupLogging.WriteStartupLog("Service Base at point 4");
      }
      catch(Exception ex)
      {
        int code = 6018;
        string message = "An exception occurrred in the ServiceBase constructor (code " + code.ToString() + ").";
        ProcessNotifications("Exception occurred in " + this.ServiceIdentification, message + g.crlf2 + ex.ToReport()); 
        StartupLogging.WriteStartupLog(message + g.crlf + ex.ToReport());
        Logger.Log(LogSeverity.SEVR, message, code, this.EntityId, ex);
        throw new Exception(message, ex);
      }
    }

    public void PreTransactionProcessing(string receivedMessage)
    {
      try
      {
        _serviceState.InvokeCount++; 
        string decryptedReceivedMessageString = _encryptor.DecryptString(receivedMessage);        
        this.ReceivedMessage = new WsMessage(decryptedReceivedMessageString);
        SetLoggerProperties();
        this.OrgId = this.ReceivedMessage.MessageHeader.OrgId;
        this.TransactionName = this.ReceivedMessage.TransactionHeader.TransactionName;
      }
      catch(Exception ex)
      {             
        throw new Exception("An exception occurred during PreTransaction processing of the web service message.", ex); 
      }
    }

    public IRequestProcessorFactory GetRequestProcessorFactory(string processorNameAndVersion )
    {
      return _serviceState.GetRequestProcessorFactory(processorNameAndVersion);
    }

    private void SetLoggerProperties()
    {
      Logger.ClientHost = this.ReceivedMessage.MessageHeader.SenderHost.DomainAndComputer;
      Logger.ClientIp = this.ReceivedMessage.MessageHeader.SenderHost.IPAddress;
      Logger.ClientUser = this.ReceivedMessage.MessageHeader.SenderHost.DomainAndUser;
      Logger.ClientApplication = this.ReceivedMessage.MessageHeader.AppName;
      Logger.ClientApplicationVersion = this.ReceivedMessage.MessageHeader.AppVersion;
      Logger.TransactionName = this.ReceivedMessage.TransactionName;
    }

    public string PostTransactionProcessing(XElement responseTransactionBodyXml)
    {
      try
      {
        TransactionStatus transactionStatus = TransactionStatus.Success;
        if (responseTransactionBodyXml.Attribute("TransactionStatus") != null)
          transactionStatus = g.ToEnum<TransactionStatus>(responseTransactionBodyXml.Attribute("TransactionStatus").Value.Trim(), TransactionStatus.NotSet);
      
        this.ResponseMessage.MessageHeader.SenderSendDateTime = this.ReceivedMessage.MessageHeader.SenderSendDateTime;
        this.ResponseMessage.MessageHeader.ReceiverReceiveDateTime = this.ReceiverReceiveTime;
        this.ResponseMessage.MessageHeader.ReceiverRespondDateTime = MessageFactory.GetWebServiceDateTime();
        this.ResponseMessage.MessageHeader.SenderHost = this.ReceivedMessage.MessageHeader.SenderHost;
        this.ResponseMessage.MessageHeader.ReceiverHost = this.WsHost;
        this.ResponseMessage.MessageHeader.AppName = g.AppInfo.AppName;
        this.ResponseMessage.MessageHeader.AppVersion = g.AppInfo.AppVersion;

        switch (transactionStatus)
        {
          case TransactionStatus.Success:
          case TransactionStatus.Failed:
          case TransactionStatus.Warning:
            this.ResponseMessage.TransactionHeader.TransactionStatus = transactionStatus;
            this.ResponseMessage.TransactionHeader.TransactionName = this.ReceivedMessage.TransactionHeader.TransactionName;
            this.ResponseMessage.TransactionHeader.TransactionVersion = this.ReceivedMessage.TransactionHeader.TransactionVersion;
            break;

          default:
            this.ResponseMessage.TransactionHeader.TransactionStatus = transactionStatus;
            this.ResponseMessage.TransactionHeader.TransactionName = "ErrorResponse";
            this.ResponseMessage.TransactionHeader.TransactionVersion = this.ReceivedMessage.TransactionHeader.TransactionVersion;
            break;
        }
 
        this.ResponseMessage.MessageBody.Transaction.TransactionBody = responseTransactionBodyXml;
            
        if (this.ReceivedMessage.MessageHeader.TrackPerformance)
        {
          this.ReceivedMessage.AddPerfInfoEntry("Prior to response message serialization");
          this.ResponseMessage.MessageHeader.TrackPerformance = true;
          this.ResponseMessage.MessageHeader.PerformanceInfoSet = this.ReceivedMessage.MessageHeader.PerformanceInfoSet; 
        }
      
        XElement responseMessageXml = this.ResponseMessage.GetXml();                
        string encryptedResponseMessage = _encryptor.EncryptString(responseMessageXml.ToString());  
        return encryptedResponseMessage;
      }
      catch(Exception ex)
      {        
        throw new Exception("An exception occurred during PostTransaction processing of web service message.", ex);
      }
    }

    public void ProcessNotifications(string subject)
    {
      ProcessNotifications(subject, String.Empty, String.Empty);
    }

    public void ProcessNotifications(string subject, string message)
    {
      ProcessNotifications(subject, message, String.Empty);
    }

    public void ProcessNotifications(string subject, string message, string notifyEventName)
    {
      try
      {
        var notifyConfigs = _serviceState.NotifyConfigs;
        var notifyConfigSetName = _serviceState.NotifyConfigSetName;

        if (notifyConfigs == null)
          notifyConfigs = _serviceState.DefaultNotifyConfigs;

        if (notifyConfigs == null)
          return;

        if (!notifyConfigs.ContainsKey(notifyConfigSetName))
          return;

        var notifyConfigSet = notifyConfigs[notifyConfigSetName];

        var notification = new Notification();
        notification.Subject = subject;
        notification.Body = message;
        notification.Subject = subject;
        notification.NotificationSource = NotificationSource.NamedEvent;
        string eventName = notifyEventName;
        if (eventName.IsBlank())
          eventName = _serviceState.DefaultNotifyEventName;
        notification.EventName = eventName;
        
        using (var notifyEngine = new NotifyEngine(notifyConfigSet, _serviceState.NotifySmtpParms))
        {
          notifyEngine.NotifyAction += _notificationsManager.NotifyActionHandler;
          notifyEngine.ProcessNotificationsAsync(notification).ContinueWith(r => { NotificationsAsyncComplete(r); });            
        }
      }
      catch (Exception ex)
      {
        Logger.Log(LogSeverity.WARN, "An exception occurred while attempting to process a notification with subject '" + subject + "' " +
                   "message '" + message.PadTo(100).Trim() + "' for notification event name '" + notifyEventName + "'.", 6015, ex);
      }
    }
       
    private void NotificationsAsyncComplete(Task<TaskResult> result)
    {
      switch (result.Status)
      {
        case TaskStatus.RanToCompletion:
          TaskResult taskResult = result.Result;
          switch (taskResult.TaskResultStatus)
          {
            case TaskResultStatus.Success:
              //Logger.Log("Notification async task RanToCompletion (TPL status) and TaskResult status 'Success'.", 6009, this.EntityId); 
              break;

            case TaskResultStatus.Warning:
              Logger.Log("Notification async task RanToCompletion (TPL status) and TaskResult status 'Warning'.", 6010, this.EntityId); 
              break;

            case TaskResultStatus.Failed:
              Logger.Log("Notification async task RanToCompletion (TPL status) and TaskResult status 'Failed'.", 6011, this.EntityId); 
              break;

            case TaskResultStatus.NotExecuted:
              break;

            default:
              Logger.Log("Notification async task RanToCompletion (TPL status) with unanticipated TaskResult status '" + taskResult.TaskResultStatus.ToString() + "'.", 6012, this.EntityId); 
              break;
          }
          break;

        case TaskStatus.Faulted:
          Logger.Log(LogSeverity.WARN, "Notification task completed with TPL task status 'Faulted'.", 6013, this.EntityId); 
          break;

        default:
          Logger.Log(LogSeverity.WARN, "Notification task completed with unanticipated TPL task status '" + result.Status.ToString() + "'.", 6014, this.EntityId); 
          break;
      }
    }
        
    public void NotifyHost(IpdxMessage ipdxMessage)
    {
      if (ipdxMessage == null)
        return;

      IPDX.SendIpdxData(ipdxMessage); 
    }
    
    private WsHost GetWebServiceHost()
    {
      WsHost host = new WsHost();
      host.IsUsed = true;
      host.UserName = Environment.UserName;
      host.DomainName = Environment.UserDomainName;
      host.ComputerName = Dns.GetHostName();
      IPHostEntry hostEntry = Dns.GetHostEntry(host.ComputerName);

      string hold169Address = String.Empty;

      if (hostEntry.AddressList.Count() > 0)
      {
        foreach (IPAddress ipAddress in hostEntry.AddressList)
        {
          if (ipAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
          {
            string ip = ipAddress.ToString().Trim();
            if (ip.Length > 8)
            {
              if (ip.Substring(0, 8) == "169.254.")
                hold169Address = ip;
              else
              {
                host.IPAddress = ipAddress.ToString();
                break;
              }
            }
          }
        }

        if (host.IPAddress.Trim().Length == 0)
        {
          if (hold169Address != String.Empty)
            host.IPAddress = hold169Address;
          else
            host.IPAddress = hostEntry.AddressList[0].ToString();
        }
      }

      return host;
    }

    private string Get_ServiceInfo()
    {
      string svcInfo = "Host: " + this.WsHost.DomainAndComputer + g.crlf +
                       "IP: " + this.WsHost.IPAddress + g.crlf +
                       "Service: " + this.AppName + g.crlf +
                       "URI: " + this.AbsoluteUri + g.crlf +
                       "Transaction: " + this.TransactionName;

      return svcInfo;
    }

    private string Get_ServiceIdentification()
    {
      return "Web Service " + this.AppName + " on " + this.WsHost.DomainAndComputer; 
    }

    public void GenerateException()
    {
      int numerator = 1;
      int denominator = 1;
      float quotient = numerator / --denominator;
    }

    public void Dispose()
    {      
    }
  }
}
