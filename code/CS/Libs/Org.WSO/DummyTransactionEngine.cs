using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Xml.Linq;
using Org.GS;
using Org.WSO.Transactions;

namespace Org.WSO
{
  public class DummyTransactionEngine : TransactionEngine, IDisposable
  {
    public DummyTransactionEngine(WsMessage requestMessage)
    {
      g.LogToMemory("MainSvcEngine Created");

      base.Message = requestMessage;
      base.MessageHeader = requestMessage.MessageHeader;
      base.TransactionHeader = requestMessage.MessageBody.Transaction.TransactionHeader;
      base.TransactionBody = requestMessage.MessageBody.Transaction.TransactionBody;
    }

    ~DummyTransactionEngine()
    {
      g.LogToMemory("MainSvcEngine Destructor");
      Dispose();
    }

    public override System.Xml.Linq.XElement ProcessTransaction(WsMessage message)
    {
      return new XElement("DummyElement");
    }

    public void Dispose()
    {
      g.LogToMemory("MainSvcEngine Disposing");
    }
  }
}
