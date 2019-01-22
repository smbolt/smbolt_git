using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.Pipes
{
  public enum NamedPipeServerStatus
  {
    Created,
    Listening,
    ListeningHalted,
    Closed,
    Error
  }

}
