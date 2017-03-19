using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Org.WSO
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

  public enum OSBits
  {
    NotSet,
    Bits32,
    Bits64
  }
}
