using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.TP;
using Org.GS;

namespace Org.WinSvc
{
  public interface ITaskDispatcher
  {
    event Action<NotifyMessage> NotifyMessage;
    bool ContinueTask { get; set; }
    Task<TaskResult> DispatchTaskAsync(ITaskProcessor taskProcessor, TaskRequest taskRequest);
    void NotifyMessageMethod(NotifyMessage notifyMessage);
    bool CheckContinue();
  }
}
