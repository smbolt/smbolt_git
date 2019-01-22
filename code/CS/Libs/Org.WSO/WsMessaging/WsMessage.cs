using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Org.GS;

namespace Org.WSO
{
  public class WsMessage
  {
    public WsMessageHeader MessageHeader { get; set; }
    public WsMessageBody MessageBody { get; set; }
    public string MessageDebug { get; set; }
    public bool IsErrorTransaction { get { return Get_IsErrorTransaction(); } }

    public XElement TransactionBody
    {
      get { return this.MessageBody.Transaction.TransactionBody; }
      set { this.MessageBody.Transaction.TransactionBody = value; }
    }

    public WsTransactionHeader TransactionHeader
    {
      get { return this.MessageBody.Transaction.TransactionHeader; }
      set { this.MessageBody.Transaction.TransactionHeader = value; }
    }

    public string TransactionName
    {
      get { return this.MessageBody.Transaction.TransactionHeader.TransactionName; }
    }

    public TransactionStatus TransactionStatus
    {
      get { return this.MessageBody.Transaction.TransactionHeader.TransactionStatus; }
    }

    public WsMessage()
    {
      this.MessageHeader = new WsMessageHeader(this);
      this.MessageBody = new WsMessageBody();
      this.MessageDebug = String.Empty;
    }

    public WsMessage(WsMessageHeader header)
    {
      this.MessageHeader = new WsMessageHeader(this);
      this.MessageBody = new WsMessageBody();
      this.MessageDebug = String.Empty;
      PopulateMessageFromHeader(header);
    }

    public WsMessage(string message)
    {
      this.MessageDebug = String.Empty;
      PopulateMessage(message);
    }

    public WsMessage(WsParms parms, XElement transactionBody)
    {      
      try
      {
        this.MessageDebug = String.Empty;
        this.MessageHeader = new WsMessageHeader(this);
        this.MessageBody = new WsMessageBody();

        this.MessageHeader.Version = "1.0";
        this.TransactionHeader.TransactionStatus = TransactionStatus.InitialRequest;

        switch (parms.MessagingParticipant)
        {
          case MessagingParticipant.Sender:
            this.MessageHeader.SenderHost = parms.WsHost;
            this.MessageHeader.SenderSendDateTime = MessageFactory.GetWebServiceDateTime();
            break;

          case MessagingParticipant.Receiver:
            this.MessageHeader.ReceiverHost = parms.WsHost;
            this.MessageHeader.ReceiverReceiveDateTime = MessageFactory.GetWebServiceDateTime();
            break;
        }
      
        this.MessageHeader.OrgId = parms.OrgId;
        this.MessageHeader.UserName = parms.UserName;
        this.MessageHeader.Password = parms.Password;
        this.MessageHeader.AppName = parms.AppName;
        this.MessageHeader.Version = parms.AppVersion; 
        this.MessageHeader.TrackPerformance = parms.TrackPerformance;

        this.TransactionHeader.TransactionName = parms.TransactionName; 
        this.TransactionHeader.TransactionVersion = parms.TransactionVersion; 
        this.TransactionBody = transactionBody;
      }
      catch(Exception ex)
      {
        throw new Exception("An exception occurred attempting to create new WsMessage.", ex); 
      }
    }

    public XElement GetXml()
    {
      XElement message = new XElement("WsMessage");
      message.Add(this.GetMessageHeaderXml());
      message.Add(this.GetMessageBodyXml());
      if (this.MessageDebug == null)
        this.MessageDebug = String.Empty;
      message.Add(new XElement("MessageDebug", this.MessageDebug)); 

      return message;
    }

    private XElement GetMessageHeaderXml()
    {
      XElement messageHeader = new XElement("WsMessageHeader");
      messageHeader.Add(new XAttribute("HeaderType", this.MessageHeader.HeaderType.ToString()));
      messageHeader.Add(new XAttribute("Version", this.MessageHeader.Version));

      XElement customer = new XElement("Org");
      customer.Add(new XAttribute("OrgId", this.MessageHeader.OrgId.ToString()));
      customer.Add(new XAttribute("UserName", this.MessageHeader.UserName.Trim()));
      customer.Add(new XAttribute("Password", this.MessageHeader.Password.Trim()));
      messageHeader.Add(customer);

      XElement sendingApplication = new XElement("SendingApplication");
      sendingApplication.Add(new XAttribute("AppName", this.MessageHeader.AppName.Trim()));
      sendingApplication.Add(new XAttribute("AppVersion", this.MessageHeader.AppVersion.Trim()));
      messageHeader.Add(sendingApplication);

      XElement timeStamps = new XElement("TimeStamps");
      if (this.MessageHeader.SenderSendDateTime.IsUsed)
        timeStamps.Add(BuildTimeStampXml("SenderSend", this.MessageHeader.SenderSendDateTime));
      if (this.MessageHeader.ReceiverReceiveDateTime.IsUsed)
        timeStamps.Add(BuildTimeStampXml("ReceiverReceive", this.MessageHeader.ReceiverReceiveDateTime));
      if (this.MessageHeader.ReceiverRespondDateTime.IsUsed)
        timeStamps.Add(BuildTimeStampXml("ReceiverRespond", this.MessageHeader.ReceiverRespondDateTime));
      if (this.MessageHeader.SenderReceiveDateTime.IsUsed)
        timeStamps.Add(BuildTimeStampXml("SenderReceive", this.MessageHeader.SenderReceiveDateTime));
      messageHeader.Add(timeStamps);

      if (this.MessageHeader.SenderHost.IsUsed)
      {
        XElement senderHost = GetWebServiceHostXml("SenderHost", this.MessageHeader.SenderHost);
        messageHeader.Add(senderHost);
      }

      if (this.MessageHeader.ReceiverHost.IsUsed)
      {
          XElement receiverHost = GetWebServiceHostXml("ReceiverHost", this.MessageHeader.ReceiverHost);
          messageHeader.Add(receiverHost);
      }

      if (this.MessageHeader.TrackPerformance)
      {
        messageHeader.Add(new XElement("TrackPerformance", "True")); 
      }

      if (this.MessageHeader.TrackPerformance)
      {
        XElement perfInfo = this.MessageHeader.PerformanceInfoSet.GetXml();
        messageHeader.Add(perfInfo);
      }

      return messageHeader;
    }

    public void AddPerfInfoEntry(string entry)
    {
      if (this.MessageHeader == null)
        return;

      if (!this.MessageHeader.TrackPerformance)
        return;

      this.MessageHeader.PerformanceInfoSet.AddEntry(entry); 
    }

    private XElement BuildTimeStampXml(string elementName, WsDateTime wsdt)
    {
      XElement timeStampXml = new XElement(elementName);
      if (wsdt.IsUsed)
      {
        timeStampXml.Add(new XAttribute("DateTime", wsdt.DateTime.ToString(@"MM/dd/yyyy HH:mm:ss.fff")));
        timeStampXml.Add(new XAttribute("TimeZone", wsdt.TimeZoneInfo.Id));
      }
      return timeStampXml;
    }

    private XElement GetWebServiceHostXml(string hostName, WsHost wsh)
    {
      XElement host = new XElement(hostName);
      host.Add(new XAttribute("DomainName", wsh.DomainName));
      host.Add(new XAttribute("ComputerName", wsh.ComputerName));
      host.Add(new XAttribute("IPAddress", wsh.IPAddress));
      host.Add(new XAttribute("UserName", wsh.UserName));
      return host;
    }

    private XElement GetMessageBodyXml()
    {
      XElement messageBody = new XElement("WsMessageBody");
      XElement transaction = new XElement("Transaction");
      messageBody.Add(transaction);

      XElement transactionHeader = new XElement("TransactionHeader");
      transactionHeader.Add(new XAttribute("TransactionName", this.MessageBody.Transaction.TransactionHeader.TransactionName));
      transactionHeader.Add(new XAttribute("TransactionVersion", this.MessageBody.Transaction.TransactionHeader.TransactionVersion)); 
      transactionHeader.Add(new XAttribute("TransactionStatus", this.MessageBody.Transaction.TransactionHeader.TransactionStatus.ToString()));
      transaction.Add(transactionHeader);

      XElement transactionBody = new XElement("TransactionBody");
      transactionBody.Add(this.MessageBody.Transaction.TransactionBody);
      transaction.Add(transactionBody);

      return messageBody;
    }

    private void PopulateMessage(string message)
    {
      try
      {
        XElement messageElement = XElement.Parse(message);
        PopulateMessageHeader(messageElement.Element("WsMessageHeader"));
        PopulateMessageBody(messageElement.Element("WsMessageBody"));
        this.MessageDebug = messageElement.Element("MessageDebug").Value; 
      }
      catch (Exception ex)
      {
        throw new Exception("An error occurred populating the WsMessage from xml in WsMessage.PopulateMessage, see inner exception", ex);
      }
    }

    private void PopulateMessageFromHeader(WsMessageHeader header)
    {
      this.MessageHeader.HeaderType = header.HeaderType;
      this.MessageHeader.Version = header.Version;
      this.MessageHeader.OrgId = header.OrgId;
      this.MessageHeader.UserName = String.Empty;
      this.MessageHeader.Password = String.Empty;
      this.MessageHeader.SenderSendDateTime = header.SenderSendDateTime;
      this.MessageHeader.ReceiverReceiveDateTime = header.ReceiverReceiveDateTime;
      this.MessageHeader.ReceiverRespondDateTime = header.ReceiverRespondDateTime;
      this.MessageHeader.SenderReceiveDateTime = header.SenderReceiveDateTime;
      this.MessageHeader.SenderHost = header.SenderHost;
      this.MessageHeader.ReceiverHost = header.ReceiverHost;
      this.MessageHeader.TrackPerformance = header.TrackPerformance;
      this.MessageHeader.PerformanceInfoSet = header.PerformanceInfoSet; 
    }

    private void PopulateMessageHeader(XElement messageHeaderXml)
    {
      this.MessageHeader = new WsMessageHeader(this);

      this.MessageHeader.HeaderType = (HeaderType)Enum.Parse(typeof(HeaderType), messageHeaderXml.Attribute("HeaderType").Value);
      this.MessageHeader.Version = messageHeaderXml.Attribute("Version").Value;
      this.MessageHeader.OrgId = Int32.Parse(messageHeaderXml.Element("Org").Attribute("OrgId").Value);
      this.MessageHeader.UserName = messageHeaderXml.Element("Org").Attribute("UserName").Value;
      this.MessageHeader.Password = messageHeaderXml.Element("Org").Attribute("Password").Value;

      this.MessageHeader.AppName = "Not Used";
      this.MessageHeader.AppVersion = "1.0";
      if (messageHeaderXml.Element("SendingApplication") != null)
      {
        this.MessageHeader.AppName = messageHeaderXml.Element("SendingApplication").Attribute("AppName").Value.Trim();
        this.MessageHeader.AppVersion = messageHeaderXml.Element("SendingApplication").Attribute("AppVersion").Value.Trim();
      }

      XElement timeStamps = messageHeaderXml.Element("TimeStamps");

      if (timeStamps.Element("SenderSend") != null)
        this.MessageHeader.SenderSendDateTime = BuildWebServiceDateTime(timeStamps.Element("SenderSend"));

      if (timeStamps.Element("ReceiverReceive") != null)
        this.MessageHeader.ReceiverReceiveDateTime = BuildWebServiceDateTime(timeStamps.Element("ReceiverReceive"));

      if (timeStamps.Element("ReceiverRespond") != null)
        this.MessageHeader.ReceiverRespondDateTime = BuildWebServiceDateTime(timeStamps.Element("ReceiverRespond"));

      if (timeStamps.Element("SenderReceive") != null)
        this.MessageHeader.SenderReceiveDateTime = BuildWebServiceDateTime(timeStamps.Element("SenderReceive"));

      if (messageHeaderXml.Element("SenderHost") != null)
        this.MessageHeader.SenderHost = BuildWebServiceHost(messageHeaderXml.Element("SenderHost"));

      if (messageHeaderXml.Element("ReceiverHost") != null)
        this.MessageHeader.ReceiverHost = BuildWebServiceHost(messageHeaderXml.Element("ReceiverHost"));

      if (messageHeaderXml.Element("TrackPerformance") != null)
      {
        this.MessageHeader.TrackPerformance = Boolean.Parse(messageHeaderXml.Element("TrackPerformance").Value);
      }

      if (messageHeaderXml.Element("PerfInfoSet") != null)
      {
        this.MessageHeader.PerformanceInfoSet = new WsPerformanceInfoSet();
        this.MessageHeader.PerformanceInfoSet.LoadFromXml(messageHeaderXml.Element("PerfInfoSet"));
      }
    }

    private WsDateTime BuildWebServiceDateTime(XElement wsdtXml)
    {
      WsDateTime wsdt = new WsDateTime();

      wsdt.IsUsed = true;
      wsdt.DateTime = DateTime.Parse(wsdtXml.Attribute("DateTime").Value);
      wsdt.TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(wsdtXml.Attribute("TimeZone").Value);

      return wsdt;
    }

    private WsHost BuildWebServiceHost(XElement wshXml)
    {
      WsHost wsh = new WsHost();
  
      wsh.IsUsed = true;
      wsh.DomainName = wshXml.Attribute("DomainName").Value;
      wsh.ComputerName = wshXml.Attribute("ComputerName").Value;
      wsh.IPAddress = wshXml.Attribute("IPAddress").Value;
      wsh.UserName = wshXml.Attribute("UserName").Value;

      return wsh;
    }

    private void PopulateMessageBody(XElement messageBodyXml)
    {
      this.MessageBody = new WsMessageBody();

      XElement transactionHeader = messageBodyXml.Element("Transaction").Element("TransactionHeader");
      this.MessageBody.Transaction.TransactionHeader.TransactionName = transactionHeader.Attribute("TransactionName").Value;
      this.MessageBody.Transaction.TransactionHeader.TransactionVersion = transactionHeader.Attribute("TransactionVersion").Value;
      this.MessageBody.Transaction.TransactionHeader.TransactionStatus = (TransactionStatus)Enum.Parse(typeof(TransactionStatus), transactionHeader.Attribute("TransactionStatus").Value);
      this.MessageBody.Transaction.TransactionBody = messageBodyXml.Element("Transaction").Element("TransactionBody").Elements().First();
    }

    private bool Get_IsErrorTransaction()
    {
      if (this.MessageBody.Transaction.TransactionHeader.TransactionName == "ErrorResponse")
        return true;

      return false;
    }
  }
}
