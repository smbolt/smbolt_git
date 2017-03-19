using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using Org.WSO.Transactions;
using Org.GS;

namespace Org.WSO
{
  public class MessageFactoryBase : IDisposable
  {
    public ObjectFactory2 ObjectFactory;
    public MessageFactoryBase() 
    {
      this.ObjectFactory = new ObjectFactory2();
    }

    public virtual TransactionBase CreateTransaction(WsParms wsParms)
    {
      TransactionBase transBase = null;

      switch (wsParms.TransactionName)
      {
        case "Ping":
          transBase = new PingRequest();
          break;

        case "GetAssemblyReport":
          transBase = new GetAssemblyReportRequest();
          break;

        case "WsCommand":
          transBase = Build_WsCommand_Request(wsParms);
          break;

        case "GetRunningTasksReport":
          transBase = new GetRunningTasksReportRequest();
          break;
      }
    
      if (transBase == null)
        return null;

      Type type = transBase.GetType();
      var piSet = type.GetProperties();
      foreach (var parm in wsParms.ParmSet)
      {
        foreach (var pi in piSet)
        {
          if (pi.Name.ToLower() == parm.ParameterName.ToLower())
          {
            pi.SetValue(transBase, parm.ParameterValue);
            break;
          }
        }
      }

      transBase.Name = wsParms.TransactionName;
      transBase.Version = wsParms.TransactionVersion; 
      return transBase;
    }

    private TransactionBase Build_WsCommand_Request(WsParms wsParms)
    {
      var trans = new WsCommandRequest();


      foreach (var parm in wsParms.ParmSet)
      {
        if (parm.ParameterType == typeof(WsCommand))
          trans.WsCommandSet.Add(((WsCommand)parm.ParameterValue));
      }

      // the work here is to build a WsCommandRequest object 
      // and add to it's WsCommandSet a WsCommand with WsCommandName = drop down value ("SendServiceCommand")
      // and add a string/string entry into the Parms property of the WsCommand using "ServiceCommand" as key and the command to execute as the value (from another dropdown)


      // get your parameters like this...


      //var fullPathParm = wsParms.ParmSet.Where(p => p.ParameterName == "FullPath").FirstOrDefault();
      //if (fullPathParm != null)
      //  trans.FullPath = fullPathParm.ParameterValue.ToString();



      return trans;
    }


    // THESE METHODS MIGHT BE USEFUL IN CREATING TRANSACTIONS
    // BUT THE "TransactionBase" TYPE SHOULD BE RETURNED AND THE "WsParms" TYPE SHOULD BE PASSED IN

    //public static WsMessage BuildStartServiceRequestMessage(MessagingParticipant participant, WsParms parms)
    //{
    //    WsMessage message = InitializeMessage(participant);
    //    message.MessageBody.Transaction.TransactionHeader.TransactionType = TransactionType.CommandRequest;
    //    CommandRequest commandRequest = new CommandRequest();
    //    Command command = new Command();
    //    command.CommandName = CommandName.StartService;
    //    command.TaskResultStatus = TaskResultStatus.NotYetTried;
    //    command.Message = String.Empty;
    //    command.BeforeWaitMilliseconds = 500;
    //    command.AfterWaitMilliseconds = 500;
    //    command.Parms.Add("ServiceName", "WatchGuard SSLVPN Service");
    //    commandRequest.CommandSet.Add(command);
    //    ObjectFactory2 f = new ObjectFactory2();
    //    message.MessageBody.Transaction.TransactionBody = f.Serialize(commandRequest);
    //    return message;
    //}

    //public static WsMessage BuildStopServiceRequestMessage(MessagingParticipant participant, WsParms parms)
    //{
    //    WsMessage message = InitializeMessage(participant);
    //    message.MessageBody.Transaction.TransactionHeader.TransactionType = TransactionType.CommandRequest;
    //    CommandRequest commandRequest = new CommandRequest();
    //    Command command = new Command();
    //    command.CommandName = CommandName.StopService;
    //    command.TaskResultStatus = TaskResultStatus.NotYetTried;
    //    command.Message = String.Empty;
    //    command.BeforeWaitMilliseconds = 500;
    //    command.AfterWaitMilliseconds = 500;
    //    command.Parms.Add("ServiceName", "WatchGuard SSLVPN Service");
    //    commandRequest.CommandSet.Add(command);
    //    ObjectFactory2 f = new ObjectFactory2();
    //    message.MessageBody.Transaction.TransactionBody = f.Serialize(commandRequest);
    //    return message;
    //}

    //public static WsMessage BuildRestartServiceRequestMessage(MessagingParticipant participant, WsParms parms)
    //{
    //    WsMessage message = InitializeMessage(participant);
    //    message.MessageBody.Transaction.TransactionHeader.TransactionType = TransactionType.CommandRequest;
    //    CommandRequest commandRequest = new CommandRequest();

    //    Command stopCommand = new Command();
    //    stopCommand.CommandName = CommandName.StopService;
    //    stopCommand.TaskResultStatus = TaskResultStatus.NotYetTried;
    //    stopCommand.Message = String.Empty;
    //    stopCommand.BeforeWaitMilliseconds = 500;
    //    stopCommand.AfterWaitMilliseconds = 500;
    //    stopCommand.Parms.Add("ServiceName", "WatchGuard SSLVPN Service");
    //    commandRequest.CommandSet.Add(stopCommand);

    //    Command startCommand = new Command();
    //    startCommand.CommandName = CommandName.StartService;
    //    startCommand.TaskResultStatus = TaskResultStatus.NotYetTried;
    //    startCommand.Message = String.Empty;
    //    startCommand.BeforeWaitMilliseconds = 500;
    //    startCommand.AfterWaitMilliseconds = 500;
    //    startCommand.Parms.Add("ServiceName", "WatchGuard SSLVPN Service");
    //    commandRequest.CommandSet.Add(startCommand);
    //    ObjectFactory2 f = new ObjectFactory2();
    //    message.MessageBody.Transaction.TransactionBody = f.Serialize(commandRequest);
    //    return message;
    //}

    //public static WsMessage BuildPauseServiceRequestMessage(MessagingParticipant participant, WsParms parms)
    //{
    //    WsMessage message = InitializeMessage(participant);
    //    message.MessageBody.Transaction.TransactionHeader.TransactionType = TransactionType.CommandRequest;
    //    CommandRequest commandRequest = new CommandRequest();
    //    Command command = new Command();
    //    command.CommandName = CommandName.PauseService;
    //    command.TaskResultStatus = TaskResultStatus.NotYetTried;
    //    command.Message = String.Empty;
    //    command.BeforeWaitMilliseconds = 500;
    //    command.AfterWaitMilliseconds = 500;
    //    command.Parms.Add("ServiceName", "WatchGuard SSLVPN Service");
    //    commandRequest.CommandSet.Add(command);
    //    ObjectFactory2 f = new ObjectFactory2();
    //    message.MessageBody.Transaction.TransactionBody = f.Serialize(commandRequest);
    //    return message;
    //}

    //public static WsMessage BuildResumeServiceRequestMessage(MessagingParticipant participant, WsParms parms)
    //{
    //    WsMessage message = InitializeMessage(participant);
    //    message.MessageBody.Transaction.TransactionHeader.TransactionType = TransactionType.CommandRequest;
    //    CommandRequest commandRequest = new CommandRequest();
    //    Command command = new Command();
    //    command.CommandName = CommandName.ResumeService;
    //    command.TaskResultStatus = TaskResultStatus.NotYetTried;
    //    command.Message = String.Empty;
    //    command.BeforeWaitMilliseconds = 500;
    //    command.AfterWaitMilliseconds = 500;
    //    command.Parms.Add("ServiceName", "WatchGuard SSLVPN Service");
    //    commandRequest.CommandSet.Add(command);
    //    ObjectFactory2 f = new ObjectFactory2();
    //    message.MessageBody.Transaction.TransactionBody = f.Serialize(commandRequest);
    //    return message;
    //}

    //public static WsMessage BuildGetServiceStatusRequestMessage(MessagingParticipant participant, WsParms parms)
    //{
    //    WsMessage message = InitializeMessage(participant);
    //    message.MessageBody.Transaction.TransactionHeader.TransactionType = TransactionType.CommandRequest;
    //    CommandRequest commandRequest = new CommandRequest();
    //    Command command = new Command();
    //    command.CommandName = CommandName.GetServiceStatus;
    //    command.TaskResultStatus = TaskResultStatus.NotYetTried;
    //    command.Message = String.Empty;
    //    command.BeforeWaitMilliseconds = 500;
    //    command.AfterWaitMilliseconds = 500;
    //    command.Parms.Add("ServiceName", "WatchGuard SSLVPN Service");
    //    commandRequest.CommandSet.Add(command);
    //    ObjectFactory2 f = new ObjectFactory2();
    //    message.MessageBody.Transaction.TransactionBody = f.Serialize(commandRequest);
    //    return message;
    //}

    //public static WsMessage BuildGetWebServiceInfoRequestMessage(MessagingParticipant participant, WsParms parms)
    //{
    //    WsMessage message = InitializeMessage(participant);
    //    message.MessageBody.Transaction.TransactionHeader.TransactionType = TransactionType.CommandRequest;
    //    CommandRequest commandRequest = new CommandRequest();
    //    Command command = new Command();
    //    command.CommandName = CommandName.GetWebServiceInfo;
    //    command.TaskResultStatus = TaskResultStatus.NotYetTried;
    //    command.Message = String.Empty;
    //    command.BeforeWaitMilliseconds = 0;
    //    command.AfterWaitMilliseconds = 0;
    //    commandRequest.CommandSet.Add(command);
    //    ObjectFactory2 f = new ObjectFactory2();
    //    message.MessageBody.Transaction.TransactionBody = f.Serialize(commandRequest);
    //    return message;
    //}

    //public static WsMessage BuildSendFilesRequestMessage(MessagingParticipant participant, WsParms parms, List<FileObject> filesToSend)
    //{
    //    WsMessage message = InitializeMessage(participant);
    //    message.MessageBody.Transaction.TransactionHeader.TransactionType = TransactionType.SendFilesRequest;
    //    SendFilesRequest sendFilesRequest = new SendFilesRequest();
    //    sendFilesRequest.FileObjects = filesToSend;
    //    ObjectFactory2 f = new ObjectFactory2();
    //    message.MessageBody.Transaction.TransactionBody = f.Serialize(sendFilesRequest);
    //    return message;
    //}

    //public static WsMessage BuildCommandRequestMessage(MessagingParticipant participant, WsParms parms)
    //{
    //    WsMessage message = InitializeMessage(participant);
    //    message.MessageBody.Transaction.TransactionHeader.TransactionType = TransactionType.CommandRequest;
    //    CommandRequest commandRequest = new CommandRequest();
    //    commandRequest.CommandSet = parms.CommandSet;
    //    ObjectFactory2 f = new ObjectFactory2();
    //    message.MessageBody.Transaction.TransactionBody = f.Serialize(commandRequest);
    //    return message;
    //}

    //public static WsMessage BuildSendServiceCommandRequestMessage(MessagingParticipant participant, WsParms parms)
    //{
    //    WsMessage message = InitializeMessage(participant);
    //    message.MessageBody.Transaction.TransactionHeader.TransactionType = TransactionType.SendFilesRequest;
    //    CommandRequest commandRequest = new CommandRequest();
    //    Command command = new Command();
    //    command.CommandName = CommandName.SendServiceCommand;
    //    command.TaskResultStatus = TaskResultStatus.NotYetTried;
    //    command.Message = String.Empty;
    //    command.BeforeWaitMilliseconds = 0;
    //    command.AfterWaitMilliseconds = 0;
    //    commandRequest.CommandSet.Add(command);
    //    ObjectFactory2 f = new ObjectFactory2();
    //    message.MessageBody.Transaction.TransactionBody = f.Serialize(commandRequest);
    //    return message;
    //}

    //public static WsMessage BuildGetWebSiteListRequestMessage(MessagingParticipant participant, WsParms parms)
    //{
    //    WsMessage message = InitializeMessage(participant);
    //    message.MessageBody.Transaction.TransactionHeader.TransactionType = TransactionType.GetWebSiteListRequest;
    //    GetWebSiteListRequest getWebSiteListRequest = new GetWebSiteListRequest();
    //    ObjectFactory2 f = new ObjectFactory2();
    //    message.MessageBody.Transaction.TransactionBody = f.Serialize(getWebSiteListRequest);
    //    return message;
    //}

    //public static WsMessage BuildGetControlRequestMessage(MessagingParticipant participant, WsParms parms)
    //{
    //    WsMessage message = InitializeMessage(participant);
    //    message.MessageBody.Transaction.TransactionHeader.TransactionType = TransactionType.GetControlRequest;
    //    GetControlRequest getControlRequest = new GetControlRequest();
    //    ObjectFactory2 f = new ObjectFactory2();
    //    message.MessageBody.Transaction.TransactionBody = f.Serialize(getControlRequest);
    //    return message;
    //}

    //public static WsMessage BuildGetWinServiceListRequestMessage(MessagingParticipant participant, WsParms parms)
    //{
    //    WsMessage message = InitializeMessage(participant);
    //    message.MessageBody.Transaction.TransactionHeader.TransactionType = TransactionType.GetWinServiceListRequest;
    //    GetWinServiceListRequest getWinServiceListRequest = new GetWinServiceListRequest();
    //    ObjectFactory2 f = new ObjectFactory2();
    //    message.MessageBody.Transaction.TransactionBody = f.Serialize(getWinServiceListRequest);
    //    return message;
    //}

    //public static WsMessage BuildGetConfigListRequestMessage(MessagingParticipant participant, GetConfigListCommand getConfigListCommand, ConfigFileType configFileType, string profileName)
    //{
    //    WsMessage message = InitializeMessage(participant);
    //    message.MessageBody.Transaction.TransactionHeader.TransactionType = TransactionType.GetConfigListRequest;
    //    GetConfigListRequest getConfigListRequest = new GetConfigListRequest();
    //    getConfigListRequest.GetFilesFrom = "\\FromPath\\";
    //    getConfigListRequest.SendFilesTo = "\\ToPath\\";
    //    getConfigListRequest.ConfigType = configFileType;
    //    getConfigListRequest.GetConfigListCommand = getConfigListCommand;
    //    getConfigListRequest.ProfileName = profileName;
    //    ObjectFactory2 f = new ObjectFactory2();
    //    message.MessageBody.Transaction.TransactionBody = f.Serialize(getConfigListRequest); 
    //    return message;
    //}

    //public static WsMessage BuildGetConfigFileRequestMessage(MessagingParticipant participant, GetConfigFileCommand getConfigFileCommand, ConfigFileType configFileType, string profileName, string fileName)
    //{
    //    WsMessage message = InitializeMessage(participant);
    //    message.MessageBody.Transaction.TransactionHeader.TransactionType = TransactionType.GetConfigFileRequest;
    //    GetConfigFileRequest getConfigFileRequest = new GetConfigFileRequest();
    //    getConfigFileRequest.GetFilesFrom = "\\FromPath\\";
    //    getConfigFileRequest.SendFilesTo = "\\ToPath\\";
    //    getConfigFileRequest.ConfigType = configFileType;
    //    getConfigFileRequest.GetConfigFileCommand = getConfigFileCommand;
    //    getConfigFileRequest.ProfileName = profileName;
    //    getConfigFileRequest.FileName = fileName;
    //    ObjectFactory2 f = new ObjectFactory2();
    //    message.MessageBody.Transaction.TransactionBody = f.Serialize(getConfigFileRequest); 
    //    return message;
    //}


    //public static WsMessage BuildGetControlRequestMessage(MessagingParticipant participant)
    //{
    //    WsMessage message = InitializeMessage(participant);
    //    message.MessageBody.Transaction.TransactionHeader.TransactionType = TransactionType.GetControlRequest;
    //    ObjectFactory2 f = new ObjectFactory2();
    //    GetControlRequest getControlRequest = new GetControlRequest();
    //    message.MessageBody.Transaction.TransactionBody = f.Serialize(getControlRequest);
    //    return message;
    //}


    
    public WsMessage InitWsMessage(WsParms wsParms)
    {
      WsMessage message = new WsMessage();

      message.MessageHeader.Version = "1.0";

      switch (wsParms.MessagingParticipant)
      {
        case MessagingParticipant.Sender:
          message.MessageHeader.SenderHost = GetWebServiceHost();
          message.MessageHeader.SenderSendDateTime = MessageFactory.GetWebServiceDateTime();
          break;

        case MessagingParticipant.Receiver:
          message.MessageHeader.ReceiverHost = GetWebServiceHost();
          message.MessageHeader.ReceiverReceiveDateTime = MessageFactory.GetWebServiceDateTime();
          break;
      }

      message.TransactionHeader.TransactionName = wsParms.TransactionName;
      message.TransactionHeader.TransactionVersion = wsParms.TransactionVersion;
      message.TransactionHeader.TransactionStatus = TransactionStatus.InitialRequest;
      return message;
    }
    
    public static WsHost GetWebServiceHost()
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

    public static WsDateTime GetWebServiceDateTime()
    {
      return new WsDateTime(NetworkHelper.GetTime(), TimeZoneInfo.Local);
    }

    public static WsMessage CreateErrorMessageOnSendError(WsMessage sentMessage, Exception ex)
    {
      WsMessage errorMessage = new WsMessage(sentMessage.MessageHeader);
      errorMessage.TransactionHeader.TransactionName = "ErrorResponse";
      errorMessage.TransactionHeader.TransactionVersion = "1.0.0.0";
      errorMessage.MessageBody.Transaction.TransactionHeader.TransactionStatus = TransactionStatus.Error;
      ErrorResponse errorResponse = new ErrorResponse(ex);
      errorResponse.Message = "Error occurred on web service message send - " + ex.Message;
      ObjectFactory2 f = new ObjectFactory2();
      errorMessage.MessageBody.Transaction.TransactionBody = f.Serialize(errorResponse);

      return errorMessage;
    }

    public static WsMessage CreateErrorMessage(WsMessage receivedMessage, WsMessage responseMessage, Exception ex, int orgId)
    {
      WsMessage errorMessage = new WsMessage();
      errorMessage.MessageHeader.Version = "1.0";
      errorMessage.MessageHeader.SenderHost = receivedMessage.MessageHeader.SenderHost;
      errorMessage.MessageHeader.SenderSendDateTime = receivedMessage.MessageHeader.SenderSendDateTime;
      errorMessage.MessageHeader.ReceiverHost = GetWebServiceHost();
      errorMessage.MessageHeader.ReceiverReceiveDateTime = MessageFactory.GetWebServiceDateTime();
      errorMessage.TransactionHeader.TransactionName = "ErrorResponse";
      errorMessage.TransactionHeader.TransactionVersion = "1.0.0.0";
      errorMessage.TransactionHeader.TransactionStatus = TransactionStatus.Error;

      ErrorResponse errorResponse = new ErrorResponse(ex);
      errorResponse.Message = ex.ToReport();
      var f = new ObjectFactory2();
      errorMessage.MessageBody.Transaction.TransactionBody = f.Serialize(errorResponse);
      errorMessage.MessageHeader.OrgId = orgId;
      g.ClearMemoryLog();
      return errorMessage;
    }

    public void Dispose()
    {
    }
  }
}
