using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Threading.Tasks;
using Org.GS;
using Org.WSO;
using Org.WSO.Transactions;

namespace Org.WSO.Transactions
{
  [XMap(XType = XType.Element)]
  public class ErrorResponse : TransactionBase
  {
    private Exception _exception;
    public Exception Exception
    {
      get {
        return _exception;
      }
      set
      {
        _exception = value;
        BuildWsException(value);
      }
    }

    public bool HasException {
      get {
        return this.WsException != null;
      }
    }

    [XMap(XType = XType.Element)]
    public WsException WsException {
      get;
      set;
    }

    public string Report {
      get;
      set;
    }

    public ErrorResponse()
    {
      this.TransactionStatus = TransactionStatus.NotSet;
      this.Message = String.Empty;
      this.Exception = null;
      this.WsException = null;
    }

    public ErrorResponse(Exception ex)
    {
      this.TransactionStatus = TransactionStatus.Error;
      this.Message = ex.Message;
      this.Exception = ex;
      this.Report = ex.ToReport();
    }

    private void BuildWsException(Exception ex)
    {
      if (_exception == null)
        return;

      this.WsException = new WsException(ex);
      this.Report = ex.ToReport();
    }

    public string ToReport()
    {
      StringBuilder sb = new StringBuilder();

      WsException wsEx;

      if (this.WsException == null && _exception != null)
        this.WsException = new WsException(_exception);

      if (this.WsException == null)
        return this.Message;

      wsEx = this.WsException;
      int level = 1;

      while (wsEx != null)
      {
        sb.Append("Level " + level.ToString() + " " + wsEx.Message + wsEx.StackTrace + g.crlf2);
        wsEx = wsEx.InnerException;
        level++;
      }

      return sb.ToString();
    }
  }
}
