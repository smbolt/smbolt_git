using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.TP
{
  public interface ITaskProcessor : IDisposable
  {
    int EntityId {
      get;
    }
    string Name {
      get;
    }
    event Action<NotifyMessage> NotifyMessage;
    event Action<ProgressMessage> ProgressUpdate;
    TaskRequest TaskRequest {
      get;
      set;
    }
    ParmSet ParmSet {
      get;
      set;
    }
    Task<TaskResult> ProcessTaskAsync(Func<bool> checkContinue);
    TaskResult ProcessTask();
    string Identify();
  }
}
