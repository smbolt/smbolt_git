using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Org.Cfg.Messaging;
using Org.GS;

namespace Org.Cfg.Messaging
{
  public class Message : JsonObject                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    
  {
    public MessageHeader MessageHeader { get; set; }
    public MessageBody MessageBody { get; set; }
    public string MessageDebug { get; set; }
    public bool IsErrorTransaction { get { return Get_IsErrorTransaction(); } }

    [JsonIgnore]
    public string TransactionName
    {
      get 
      {
        if (this.MessageBody == null || this.MessageBody.Transaction == null ||
            this.MessageBody.Transaction.TransactionHeader == null ||
            this.MessageBody.Transaction.TransactionHeader.TransactionName.IsBlank())
          return String.Empty;

        return this.MessageBody.Transaction.TransactionHeader.TransactionName; 
      }
    }

    public Message()
    {
      this.MessageHeader = new MessageHeader(this);
      this.MessageBody = new MessageBody();
    }

    public Message(string json)
      : base(json)
    {
    }

    public Message Load()
    {
      try
      {
        dynamic msg = base.JObject;
        this.MessageHeader = ((JObject)msg.MessageHeader).ToObject<MessageHeader>();
        this.MessageBody = ((JObject)msg.MessageBody).ToObject<MessageBody>();
        this.MessageBody.TransactionJson = msg.MessageBody.Transaction;
        return this;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to load json-based properties to the Message object.", ex); 
      }
    }
    
    private bool Get_IsErrorTransaction()
    {
      if (this.MessageBody.Transaction.TransactionHeader.TransactionName == "ErrorResponse")
        return true;

      return false;
    }
  }

  public static class MessagingExtensionMethods
  {

    public static MessageHeader MessageHeader(this JsonObject j, Message message = null)
    {
      var messageHeader = new MessageHeader(message);


      return messageHeader;
    }
  }
}
