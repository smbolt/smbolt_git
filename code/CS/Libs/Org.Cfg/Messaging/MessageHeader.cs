using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.Cfg.Messaging
{
  public class MessageHeader
  { 
    public Message Message { get; set; }
    public string Version { get; set; }
    public string AppName { get; set; }
    public string AppVersion { get; set; }
    public int OrgId { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public Host SenderHost { get; set; }
    public Host ReceiverHost { get; set; }
    public MessageDateTime SenderSendDateTime { get; set; }
    public MessageDateTime ReceiverReceiveDateTime { get; set; }
    public MessageDateTime ReceiverRespondDateTime { get; set; }
    public MessageDateTime SenderReceiveDateTime { get; set; }
    public DateTime? SenderSendLocalDateTime { get { return this.SenderSendDateTime.ToLocalDateTime(); } }
    public DateTime? ReceiverReceiveLocalDateTime { get { return this.ReceiverReceiveDateTime.ToLocalDateTime(); } }
    public DateTime? ReceiverRespondLocalDateTime { get { return this.ReceiverRespondDateTime.ToLocalDateTime(); } }
    public DateTime? SenderReceiveLocalDateTime { get { return this.SenderReceiveDateTime.ToLocalDateTime(); } }
    public TimeSpan? TotalResponseTime { get { return (TimeSpan?)null; } }  // implement this
    public TimeSpan? ServerResponseTime { get { return (TimeSpan?)null; } } // implement this
    public string TransactionName { get { return Get_TransactionName(); } }
    public bool TrackPerformance { get; set; }
    public PerformanceInfoSet PerformanceInfoSet { get; set; }

    public MessageHeader(Message message)
    {
      this.Message = message;
      this.Initialize();
    }

    private void Initialize()
    {
      this.Version = String.Empty;
      this.AppName = String.Empty;
      this.AppVersion = String.Empty;
      this.OrgId = 0;
      this.UserName = String.Empty;
      this.Password = String.Empty;
      this.SenderHost = null;
      this.ReceiverHost = null;
      this.SenderSendDateTime = null;
      this.ReceiverReceiveDateTime = null;
      this.ReceiverRespondDateTime = null;
      this.SenderReceiveDateTime =  null;
    }

    public void AddPerfInfoEntry(string entry)
    {
      if (!this.TrackPerformance)
        return;

      this.PerformanceInfoSet.AddEntry(entry);
    }

    public string Get_TransactionName()
    {
      if (this.Message == null || this.Message.TransactionName == null)
        return String.Empty;

      return this.Message.TransactionName;
    }
  }
}
