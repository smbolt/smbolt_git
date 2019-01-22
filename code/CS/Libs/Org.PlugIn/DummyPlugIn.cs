using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Org.TP.Concrete;
using Org.GS;

namespace Org.PlugIn
{
  public class DummyPlugIn : TaskProcessorBase
  {
    public override TaskResult ProcessTask()
    {
      TaskResult taskResult = base.InitializeTaskResult();

      var sb = new StringBuilder();

      try
      {
        sb.Append(this.ProcessorName + " task processing is beginning." + g.crlf);

        base.Notify(this.ProcessorName + " task processing starting on thread " + System.Threading.Thread.CurrentThread.ManagedThreadId.ToString() + ".");
        base.ProgressNotify(this.ProcessorName, this.ProcessorName + " is progressing.", 0, 3); 


        sb.Append(this.ProcessorName + " starting sub-task 1" + g.crlf);
        Thread.Sleep(2000);
        sb.Append(this.ProcessorName + " finished with sub-task 1" + g.crlf);
        base.Notify(this.ProcessorName + " finished processing sub-task 1.");
        base.ProgressNotify(this.ProcessorName, this.ProcessorName + " is progressing.", 1, 3);         


        sb.Append(this.ProcessorName + " starting sub-task 2" + g.crlf);
        Thread.Sleep(2000);
        sb.Append(this.ProcessorName + " finished with sub-task 2" + g.crlf);
        base.Notify("Dummy task finished processing sub-task 2.");
        base.ProgressNotify(this.ProcessorName, this.ProcessorName + " is progressing.", 2, 3); 

        sb.Append(this.ProcessorName + " starting sub-task 3" + g.crlf);
        Thread.Sleep(2000);
        sb.Append(this.ProcessorName + " finished with sub-task 3" + g.crlf);
        base.Notify(this.ProcessorName + " finished processing sub-task 3.");
        base.ProgressNotify(this.ProcessorName, this.ProcessorName + " is progressing.", 3, 3);

        base.Notify(this.ProcessorName + " processing is finished." + System.Threading.Thread.CurrentThread.ManagedThreadId.ToString() + ".");
        sb.Append(this.ProcessorName + " task processing is ending." + g.crlf);

        return taskResult.Success(sb.ToString());
      }
      catch (Exception ex)
      {
        return taskResult.Failed("An exception occurred during " + base.TaskRequest.TaskName + " task processing.", ex);
      }
    }
  }
}
