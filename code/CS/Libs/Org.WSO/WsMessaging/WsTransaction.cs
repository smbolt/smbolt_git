using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Org.WSO.Transactions;

namespace Org.WSO
{
  public class WsTransaction
  {
    private WsTransactionHeader _transactionHeader;
    public WsTransactionHeader TransactionHeader
    {
      get {
        return _transactionHeader;
      }
      set {
        _transactionHeader = value;
      }
    }

    public TransactionBase Transaction {
      get;
      set;
    }

    private XElement _transactionBody;
    public XElement TransactionBody
    {
      get {
        return _transactionBody;
      }
      set {
        _transactionBody = value;
      }
    }

    public WsTransaction()
    {
      this.Transaction = null;
      _transactionHeader = new WsTransactionHeader();
      _transactionBody = new XElement("Empty");
    }
  }
}
