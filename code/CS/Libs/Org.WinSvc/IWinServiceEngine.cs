using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.WinSvc
{
  public interface IWinServiceEngine
  {
    // Events
    event Action<IpdxMessage> NotifyHost;

    // Interface Properties
    string ServiceName { get; set; }
    string EngineName { get; set; }
    int EntityId { get; set; }
    WinServiceState WinServiceState { get; set; }
    bool RunningAsService { get; }
    bool IsSuspended { get; set; }
    bool IsSuspendedReported { get; set; }
    ITaskDispatcher TaskDispatcher { get; set; }
    bool RefreshTaskList { get; set; }
    bool RefreshTaskRequests { get; set; }
    TaskRequestSet TaskRequests { get; set; }
    int TasksProcessed { get; set; }
    bool GetNextTaskNow { get; set; }

    // Interface Methods
    bool Start();
    void Stop();
    void Pause();
    void Resume();
    void WireUpSuperMethod(Func<string, object[], TaskResult> func);
  }
}
