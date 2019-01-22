using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.DynamoDB
{
  public enum DbConnectionStatus
  {
    NotSet,
    TcpPortNotListening,
    Failed,
    Ready
  }

}
