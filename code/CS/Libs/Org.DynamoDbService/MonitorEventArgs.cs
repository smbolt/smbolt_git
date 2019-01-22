using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.DynamoDbService
{
  public class MonitorEventArgs
  {
    public DynamoDbServiceState DynamoDbServiceState { get; set; }
    public string Message { get; set; }

    public MonitorEventArgs(DynamoDbServiceState dynamoDbServiceState, string message)
    {
      this.DynamoDbServiceState = dynamoDbServiceState;
      this.Message = message;
    }
  }
}
