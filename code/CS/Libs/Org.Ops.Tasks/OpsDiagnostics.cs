using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;
using Org.GS.Configuration;
using Org.GS.Logging;
using Org.GS.Performance;
using Org.TP.Concrete;

namespace Org.Ops.Tasks
{
  public class OpsDiagnostics : TaskProcessorBase
  {
    private Logger _logger;

    public override async Task<TaskResult> ProcessTaskAsync(Func<bool> checkContinue)
    {
      TaskResult taskResult = base.InitializeTaskResult();

      _logger = new Logger();

      try
      {
        base.Notify("Diagnostics task processing starting on thread " + System.Threading.Thread.CurrentThread.ManagedThreadId.ToString() + ".");

        taskResult = await Task.Run<TaskResult>(() =>
        {
          this.Initialize();

          var perfProfileSet = GetParmValue("PerfProfileSet") as PerfProfileSet;

          var logger = new Logger();

          PerformanceCounter pc = null;
          var counters = new SortedList<PerformanceCounter, string>();
          var sb = new StringBuilder();

          sb.Append("Profile             Category                      Counter                       Instance                           Value" + g.crlf +
                    "------------------------------------------------------------------------------------------------------------------------");

          foreach (var perfProfile in perfProfileSet.Values)
          {
            foreach (var category in perfProfile.CategorySet.Values)
            {
              foreach (var counter in category.CounterSet.Values)
              {
                pc = new PerformanceCounter(counter.CategoryName, counter.CounterName, counter.InstanceName);
                pc.NextValue();
                counters.Add(pc, perfProfile.ProfileName);
              }
            }
          }

          System.Threading.Thread.Sleep(1000);

          foreach (var kvp in counters)
          {
            var value = kvp.Key.NextValue();
            sb.Append(g.crlf + kvp.Value.ToString().PadTo(20) + kvp.Key.CategoryName.PadTo(30) + kvp.Key.CounterName.PadTo(30) +
                      kvp.Key.InstanceName.PadTo(20) + value.ToString().PadToJustifyRight(20));
          }

          string message = sb.ToString();
          logger.Log(message);

          return new TaskResult(TaskRequest.TaskName).Success();
        });

        return taskResult;
      }
      catch (Exception ex)
      {
        return taskResult.Failed("An exception occurred during " + base.TaskRequest.TaskName + " task processing.", ex);
      }
    }

    protected override void Initialize()
    {
      base.Initialize();

      this.AssertParmExistence("PerfProfileSet");
    }
  }
}
