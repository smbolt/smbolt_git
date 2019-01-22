using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TPL = System.Threading.Tasks;
using System.ComponentModel.Composition;
using System.Data;
using System.Data.SqlClient;
using Org.GS;
using Org.TP.Concrete;

namespace Org.TP
{
  public class Dummy : TaskProcessorBase
  {
    public override async TPL.Task<TaskResult> ProcessTaskAsync(Func<bool> checkContinue)
    {
      TaskResult taskResult = base.InitializeTaskResult();

      try
      {
        base.Notify("Dummy task processing starting on thread " + System.Threading.Thread.CurrentThread.ManagedThreadId.ToString() + ".");

        taskResult = await TPL.Task.Run<TaskResult>(() =>
        {
          return taskResult.Success();
        });

        System.Threading.Thread.Sleep(2000);

        return taskResult;
      }
      catch (Exception ex)
      {
        return taskResult.Failed("An exception occurred during " + base.TaskRequest.TaskName + " task processing.", ex);
      }
    }

    ~Dummy()
    {
      Dispose(false);
    }
  }
}