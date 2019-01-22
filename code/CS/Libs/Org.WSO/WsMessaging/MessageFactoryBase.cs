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

        case "WsCommand":
          transBase = Build_WsCommandRequest(wsParms);
          break;

        case "ServiceEngineCommand":
          transBase = Build_ServiceEngineCommandRequest(wsParms);
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

    private TransactionBase Build_WsCommandRequest(WsParms wsParms)
    {
      var trans = new WsCommandRequest();

      foreach (var parm in wsParms.ParmSet)
      {
        if (parm.ParameterType == typeof(WsCommand))
        {
          trans.WsCommandSet.Add(((WsCommand)parm.ParameterValue));
        }
      }

      return trans;
    }

    private TransactionBase Build_ServiceEngineCommandRequest(WsParms wsParms)
    {
      var trans = new ServiceEngineCommandRequest();

      foreach (var parm in wsParms.ParmSet)
      {
        if (parm.ParameterType == typeof(WsCommand))
          trans.WsCommandSet.Add(((WsCommand)parm.ParameterValue));
      }

      return trans;
    }

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
