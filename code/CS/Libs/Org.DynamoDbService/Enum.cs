using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.DynamoDbService
{
  public enum DynamoDbServiceState
  {
    Stopped,
    Running,
    Paused,
    Faulted
  }

}
