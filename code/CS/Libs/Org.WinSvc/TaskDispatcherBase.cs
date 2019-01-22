using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.TP;
using Org.GS;

namespace Org.WinSvc
{
  public class TaskDispatcherBase : ITaskDispatcher
  {
    public event Action<NotifyMessage> NotifyMessage;
    public bool ContinueTask {
      get;
      set;
    }

    public TaskDispatcherBase()
    {

    }

    public async virtual Task<TaskResult> DispatchTaskAsync(ITaskProcessor taskProcessor, TaskRequest taskRequest)
    {
      throw new Exception("The 'DispatchTaskAsync' must be overridden in derived classes.");
    }

    public void NotifyMessageMethod(NotifyMessage notifyMessage)
    {
      if (this.NotifyMessage != null)
        this.NotifyMessage(notifyMessage);
    }

    public bool CheckContinue()
    {
      return ContinueTask;
    }
  }
}
