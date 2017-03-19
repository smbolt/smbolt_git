using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Org.GS;

namespace Org.WSO
{
  public class WsMessageHeader
  {
    public WsMessage WsMessage { get; set; }
    public HeaderType HeaderType { get; set; }
    public string Version { get; set; }
    public string AppName { get; set; }
    public string AppVersion { get; set; }
    public int OrgId { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public WsHost SenderHost { get; set; }
    public WsHost ReceiverHost { get; set; }
    public WsDateTime SenderSendDateTime { get; set; }
    public WsDateTime ReceiverReceiveDateTime { get; set; }
    public WsDateTime ReceiverRespondDateTime { get; set; }
    public WsDateTime SenderReceiveDateTime { get; set; }
    public string TransactionName { get { return Get_TransactionName(); } }


    public DateTime SenderSendLocalDateTime
    {
      get { return GetLocalDateTime(this.SenderSendDateTime); }
    }

    public DateTime ReceiverReceiveLocalDateTime
    {
      get { return GetLocalDateTime(this.ReceiverReceiveDateTime); }
    }

    public DateTime ReceiverRespondLocalDateTime
    {
      get { return GetLocalDateTime(this.ReceiverRespondDateTime); }
    }

    public DateTime SenderReceiveLocalDateTime
    {
      get { return GetLocalDateTime(this.SenderReceiveDateTime); }
    }

    public TimeSpan TotalResponseTime
    {
      get { return GetTotalResponseTime(); }
    }

    public TimeSpan ServerResponseTime
    {
      get { return GetServerResponseTime(); }
    }

    public bool TrackPerformance { get; set; }

    public WsPerformanceInfoSet PerformanceInfoSet { get; set; }

    public WsMessageHeader(WsMessage wsMessage)
    {
      this.WsMessage = WsMessage;
      this.HeaderType = HeaderType.Standard;
      this.Version = "0.0";
      this.AppName = g.AppInfo.AppName;
      this.AppVersion = g.AppInfo.AppVersion;
      this.OrgId = 0;
      this.UserName = String.Empty;
      this.Password = String.Empty;
      this.SenderHost = new WsHost();
      this.ReceiverHost = new WsHost();
      this.SenderSendDateTime = new WsDateTime();
      this.ReceiverReceiveDateTime = new WsDateTime();
      this.ReceiverRespondDateTime = new WsDateTime();
      this.SenderReceiveDateTime = new WsDateTime();
      this.TrackPerformance = false;
      this.PerformanceInfoSet = new WsPerformanceInfoSet();
    }


    public string ToReport(int indentChars, string messageFunction)
    {
      string crlf = g.crlf;
      string indent = g.BlankString(indentChars);
      StringBuilder sb = new StringBuilder();

      sb.Append(crlf);
      sb.Append(indent + "Web Service Message Header Report" + crlf);
      sb.Append(indent + "  Function                 : " + messageFunction + crlf);

      sb.Append(indent + "  Header Type              : " + this.HeaderType.ToString().PadRight(15).Substring(0, 15) +  "   Version   : " + this.Version + crlf);
      sb.Append(indent + "  Customer ID              : " + this.OrgId.ToString().PadRight(15).Substring(0, 15) + "   User Name   : " + this.UserName + "   Password : " + this.Password + crlf);
      sb.Append(indent + "  AppName                  : " + this.AppName.PadRight(15).Substring(0, 15) + "   AppVersion: " + this.AppVersion + crlf);

      sb.Append(indent + "  Sender Host" + crlf);
      if (this.SenderHost.IsUsed)
      {
        sb.Append(indent + "      Computer Name        : " + this.SenderHost.ComputerName + crlf);
        sb.Append(indent + "      Domain Name          : " + this.SenderHost.DomainName + crlf);
        sb.Append(indent + "      IP Address           : " + this.SenderHost.IPAddress + crlf);
        sb.Append(indent + "      User Name            : " + this.SenderHost.UserName + crlf);
      }

      sb.Append(indent + "  Receiver Host" + crlf);
      if (this.ReceiverHost.IsUsed)
      {
        sb.Append(indent + "      Computer Name        : " + this.ReceiverHost.ComputerName + crlf);
        sb.Append(indent + "      Domain Name          : " + this.ReceiverHost.DomainName + crlf);
        sb.Append(indent + "      IP Address           : " + this.ReceiverHost.IPAddress + crlf);
        sb.Append(indent + "      User Name            : " + this.ReceiverHost.UserName + crlf);
      }

      string senderSendDateTimeString = "No Data";
      string receiverReceiveDateTimeString = "No Data";
      string receiverRespondDateTimeString = "No Data";
      string senderReceiveDateTimeString = "No Data";

      if (!this.SenderSendDateTime.IsMinValue())
          senderSendDateTimeString = this.SenderSendDateTime.DateTime.ToString("MM/dd/yyyy HH:mm:ss.fff") + " " + this.SenderSendDateTime.TimeZoneInfo.DisplayName;

      if (!this.ReceiverReceiveDateTime.IsMinValue())
          receiverReceiveDateTimeString = this.ReceiverReceiveDateTime.DateTime.ToString("MM/dd/yyyy HH:mm:ss.fff") + " " + this.ReceiverReceiveDateTime.TimeZoneInfo.DisplayName;

      if (!this.ReceiverRespondDateTime.IsMinValue())
          receiverRespondDateTimeString = this.ReceiverRespondDateTime.DateTime.ToString("MM/dd/yyyy HH:mm:ss.fff") + " " + this.ReceiverRespondDateTime.TimeZoneInfo.DisplayName;

      if (!this.SenderReceiveDateTime.IsMinValue())
          senderReceiveDateTimeString = this.SenderReceiveDateTime.DateTime.ToString("MM/dd/yyyy HH:mm:ss.fff") + " " + this.SenderReceiveDateTime.TimeZoneInfo.DisplayName;

      sb.Append(indent + "  Time Stamps" + crlf);
      sb.Append(indent + "      Sender Send          : " + senderSendDateTimeString + crlf);
      sb.Append(indent + "      Receiver Receive     : " + receiverReceiveDateTimeString + crlf);
      sb.Append(indent + "      Receiver Respond     : " + receiverRespondDateTimeString + crlf);
      sb.Append(indent + "      Sender Receive       : " + senderReceiveDateTimeString + crlf);

      return sb.ToString();
    }

    private DateTime GetLocalDateTime(WsDateTime wsdt)
    {
      DateTime dt = DateTime.MinValue;

      if (wsdt.IsUsed)
      {
        TimeZoneInfo tzLocal = TimeZoneInfo.Local;
        dt = TimeZoneInfo.ConvertTime(wsdt.DateTime, wsdt.TimeZoneInfo, tzLocal);
      }

      return dt;
    }

    private TimeSpan GetTotalResponseTime()
    {
      TimeSpan ts = new TimeSpan();
      if (this.SenderSendDateTime.IsUsed && this.SenderReceiveDateTime.IsUsed)
      {
        DateTime dtSend = TimeZoneInfo.ConvertTime(this.SenderSendDateTime.DateTime, this.SenderSendDateTime.TimeZoneInfo);
        DateTime dtReceive = TimeZoneInfo.ConvertTime(this.SenderReceiveDateTime.DateTime, this.SenderReceiveDateTime.TimeZoneInfo);

        ts = dtReceive - dtSend;
      }

      return ts;
    }

    private TimeSpan GetServerResponseTime()
    {
      TimeSpan ts = new TimeSpan();
      if (this.ReceiverReceiveDateTime.IsUsed && this.ReceiverRespondDateTime.IsUsed)
      {
        DateTime dtReceive = TimeZoneInfo.ConvertTime(this.ReceiverReceiveDateTime.DateTime, this.ReceiverReceiveDateTime.TimeZoneInfo);
        DateTime dtRespond = TimeZoneInfo.ConvertTime(this.ReceiverRespondDateTime.DateTime, this.ReceiverRespondDateTime.TimeZoneInfo);

        ts = dtRespond - dtReceive;
      }

      return ts;
    }

    public void AddPerfInfoEntry(string entry)
    {
      if (!this.TrackPerformance)
        return;

      this.PerformanceInfoSet.AddEntry(entry);
    }

    public string Get_TransactionName()
    {
      if (this.WsMessage == null)
        return String.Empty;

      if (this.WsMessage.TransactionName == null)
        return string.Empty;

      return this.WsMessage.TransactionName;
    }
  }
}
