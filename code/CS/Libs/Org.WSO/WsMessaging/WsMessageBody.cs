using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Org.WSO
{
  public class WsMessageBody
  {
    private WsTransaction _transaction;
    public WsTransaction Transaction
    {
      get {
        return _transaction;
      }
      set {
        _transaction = value;
      }
    }

    public WsMessageBody()
    {
      _transaction = new WsTransaction();
    }
  }
}
