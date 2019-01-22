using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.Cfg.Messaging
{
  public enum TransactionStatus
  {
    NotSet,
    InitialRequest,
    Success,
    Failed,
    Warning,
    Error
  }
}
