using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.Cfg.Messaging
{
  public class TransactionBase
  {
    public TransactionHeader TransactionHeader {
      get;
      set;
    }
    public string Name {
      get {
        return this.GetType().Name;
      }
    }
    public string Version {
      get {
        return this.Get_Version();
      }
    }
    public TransactionStatus TransactionStatus {
      get;
      set;
    }
    public string Message {
      get;
      set;
    }
    public string Code {
      get;
      set;
    }

    public TransactionBase()
    {
      this.TransactionHeader = new TransactionHeader();
      this.TransactionStatus = TransactionStatus.NotSet;
      this.Message = String.Empty;
      this.Code = String.Empty;
    }

    private string Get_Version()
    {
      var version = (TransactionVersion)this.GetType().GetCustomAttributes(typeof(TransactionVersion), true).FirstOrDefault();
      if (version == null)
        return "1.0.0.0";
      return version.VersionString;
    }
  }
}
