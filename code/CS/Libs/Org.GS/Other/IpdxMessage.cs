using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Org.GS
{
  public enum IpdxMessageType
  {
    Text,
    CommandSet,
    Notification
  }

  public class IpdxMessage
  {
    public string Sender {
      get;
      set;
    }
    public string Recipient {
      get;
      set;
    }
    public IpdxMessageType MessageType {
      get;
      set;
    }
    public string Text {
      get;
      set;
    }
    public IpdxCommandSet IpdxCommandSet {
      get;
      set;
    }

    public IpdxMessage()
    {
      this.Sender = String.Empty;
      this.Recipient = "*";
      this.MessageType = IpdxMessageType.Text;
      this.Text = String.Empty;
      this.IpdxCommandSet = new IpdxCommandSet();
    }

    public IpdxMessage(string recipient, string text)
    {
      this.Sender = String.Empty;
      this.Recipient = recipient;
      this.MessageType = IpdxMessageType.Text;
      this.Text = text;
      this.IpdxCommandSet = new IpdxCommandSet();
    }

    public IpdxMessage(string recipient, IpdxMessageType messageType, string text)
    {
      this.Sender = String.Empty;
      this.Recipient = recipient;
      this.MessageType = messageType;
      this.Text = text;
      this.IpdxCommandSet = new IpdxCommandSet();
    }
  }
}
