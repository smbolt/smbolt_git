using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.Cfg.Messaging
{
  public class TransactionHeader
  {
    public string TransactionName {
      get;
      set;
    }
    public string TransactionVersion {
      get;
      set;
    }
    public TransactionStatus TransactionStatus {
      get;
      set;
    }
    public string ProcessorNameAndVersion {
      get {
        return this.TransactionName + "Processor_" + this.TransactionVersion;
      }
    }

    public TransactionHeader()
    {
      this.TransactionName = String.Empty;
      this.TransactionVersion = String.Empty;
      this.TransactionStatus = TransactionStatus.NotSet;
    }
  }
}
