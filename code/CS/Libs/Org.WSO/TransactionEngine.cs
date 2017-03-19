using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using Org.GS;

namespace Org.WSO
{
  public class TransactionEngine
  {
    private WsMessage _message;
    public WsMessage Message 
    {
      get { return _message; }
      set
      {
        _message = value;
        this.MessageHeader = _message.MessageHeader;
        this.TransactionHeader = _message.MessageBody.Transaction.TransactionHeader;
        this.TransactionBody = _message.MessageBody.Transaction.TransactionBody; 
      } 
    }

    public WsMessageHeader MessageHeader { get; set; }
    public WsTransactionHeader TransactionHeader { get; set; }
    public XElement TransactionBody { get; set; }

    public virtual XElement ProcessTransaction(WsMessage message)
    {
      return new XElement("Empty");
    }

    public void Dispose()
    {
    }
  }
}
