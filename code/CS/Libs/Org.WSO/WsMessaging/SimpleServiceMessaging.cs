using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Org.GS;

namespace Org.WSO
{
  public class SimpleServiceMessaging
  {
    private static Encryptor encryptor = new Encryptor();

    public static WsMessage SendMessage(WsMessage sendMessage, string endpoint, int sendTimeoutSeconds = 0)
    {
      string transactionName = "Unknown";

      try
      {
        transactionName = sendMessage.MessageBody.Transaction.TransactionHeader.TransactionName;

        XElement sendMessageXml = sendMessage.GetXml();
        string sendMessageString = sendMessageXml.ToString();

        SimpleServiceAdapter simpleServiceAdapter = new SimpleServiceAdapter();
        string encryptedSentMessageString = encryptor.EncryptString(sendMessageString);
        string encryptedResponseMessageString = simpleServiceAdapter.SendMessage(encryptedSentMessageString, endpoint, sendTimeoutSeconds);
        string responseMessageString = encryptor.DecryptString(encryptedResponseMessageString);
        WsMessage responseMessage = new WsMessage(responseMessageString);
        responseMessage.MessageHeader.SenderReceiveDateTime = MessageFactory.GetWebServiceDateTime();
        return responseMessage;
      }
      catch (System.ServiceModel.EndpointNotFoundException enfx)
      {
        //if (ServiceHelper.ServiceBase != null)
        //    ServiceHelper.SendAlert(ServiceHelper.ServiceBase.AppName, "SimpleServiceMessaging", "SendMessage", "0003", "101", "Sending Trans " + transactionType + ".", enfx);
        WsMessage errorMessage = MessageFactory.CreateErrorMessageOnSendError(sendMessage, enfx);
        errorMessage.MessageHeader.SenderReceiveDateTime = MessageFactory.GetWebServiceDateTime();
        return errorMessage;
      }
      catch (Exception ex)
      {
        //if (ServiceHelper.ServiceBase != null)
        //    ServiceHelper.SendAlert(ServiceHelper.ServiceBase.AppName, "SimpleServiceMessaging", "SendMessage", "0004", "103", "Sending Trans " + transactionType + ".", ex);
        WsMessage errorMessage = MessageFactory.CreateErrorMessageOnSendError(sendMessage, ex);
        errorMessage.MessageHeader.SenderReceiveDateTime = MessageFactory.GetWebServiceDateTime();
        return errorMessage;
      }
    }

    public static void SendMessageOneWay(WsMessage sendMessage, string endpoint)
    {
      string transactionName = "Unknown";

      try
      {
        transactionName = sendMessage.MessageBody.Transaction.TransactionHeader.TransactionName;

        XElement sendMessageXml = sendMessage.GetXml();
        string sendMessageString = sendMessageXml.ToString();

        SimpleServiceAdapter simpleServiceAdapter = new SimpleServiceAdapter();
        string encryptedSentMessageString = encryptor.EncryptString(sendMessageString);
        simpleServiceAdapter.SendMessageOneWay(encryptedSentMessageString, endpoint);
      }
      catch// (System.ServiceModel.EndpointNotFoundException enfx)
      {
        //if (ServiceHelper.ServiceBase == null)
        //    ServiceHelper.SendAlert(G.ConfigName, "SimpleServiceMessaging", "SendMessageOneWay", "0001", "101", "Sending Trans " + transactionType + ".", enfx);
        //else
        //    ServiceHelper.SendAlert(ServiceHelper.ServiceBase.AppName, "SimpleServiceMessaging", "SendMessageOneWay", "0001", "101", "Sending Trans " + transactionType + ".", enfx);
      }
      //catch// (Exception ex)
      //{
      //    //if (ServiceHelper.ServiceBase == null)
      //    //    ServiceHelper.SendAlert(G.ConfigName, "SimpleServiceMessaging", "SendMessageOneWay", "0002", "103", "Sending Trans " + transactionType + ".", ex);
      //    //else
      //    //    ServiceHelper.SendAlert(ServiceHelper.ServiceBase.AppName, "SimpleServiceMessaging", "SendMessageOneWay", "0002", "103", "Sending Trans " + transactionType + ".", ex);
      //}
    }


    public static WsMessage SendMessage_TCP(WsMessage sendMessage, string endpoint)
    {
      string transactionName = "Unknown";

      try
      {
        transactionName = sendMessage.MessageBody.Transaction.TransactionHeader.TransactionName;

        XElement sendMessageXml = sendMessage.GetXml();
        string sendMessageString = sendMessageXml.ToString();

        SimpleServiceAdapter simpleServiceAdapter = new SimpleServiceAdapter();
        string encryptedSentMessageString = encryptor.EncryptString(sendMessageString);
        string encryptedResponseMessageString = simpleServiceAdapter.SendMessage_TCP(encryptedSentMessageString, endpoint);
        string responseMessageString = encryptor.DecryptString(encryptedResponseMessageString);
        WsMessage responseMessage = new WsMessage(responseMessageString);
        responseMessage.MessageHeader.SenderReceiveDateTime = MessageFactory.GetWebServiceDateTime();
        return responseMessage;
      }
      catch (System.ServiceModel.EndpointNotFoundException enfx)
      {
        //if (ServiceHelper.ServiceBase != null)
        //    ServiceHelper.SendAlert(ServiceHelper.ServiceBase.AppName, "SimpleServiceMessaging", "SendMessage", "0003", "101", "Sending Trans " + transactionType + ".", enfx);
        WsMessage errorMessage = MessageFactory.CreateErrorMessageOnSendError(sendMessage, enfx);
        errorMessage.MessageHeader.SenderReceiveDateTime = MessageFactory.GetWebServiceDateTime();
        return errorMessage;
      }
      catch (Exception ex)
      {
        //if (ServiceHelper.ServiceBase != null)
        //    ServiceHelper.SendAlert(ServiceHelper.ServiceBase.AppName, "SimpleServiceMessaging", "SendMessage", "0004", "103", "Sending Trans " + transactionType + ".", ex);
        WsMessage errorMessage = MessageFactory.CreateErrorMessageOnSendError(sendMessage, ex);
        errorMessage.MessageHeader.SenderReceiveDateTime = MessageFactory.GetWebServiceDateTime();
        return errorMessage;
      }
    }
  }
}
