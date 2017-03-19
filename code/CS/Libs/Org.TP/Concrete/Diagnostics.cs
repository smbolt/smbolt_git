using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using System.Data;
using System.Data.SqlClient;
using Org.GS;
using Org.TP.Concrete;

namespace Org.TP
{
  public class Diagnostics : TaskProcessorBase
  {
    private string _controlFileFullPath;


    public override async Task<TaskResult> ProcessTaskAsync(Func<bool> checkContinue)
    {
      TaskResult taskResult = base.InitializeTaskResult();

      try
      {
        base.Notify("Diagnostics task processing starting on thread " + System.Threading.Thread.CurrentThread.ManagedThreadId.ToString() + ".");

        taskResult = await Task.Run<TaskResult>(() =>
        {
          Initialize();

          if (!File.Exists(_controlFileFullPath))
            return taskResult.Failed("Diagnostics control file '" + _controlFileFullPath + "' does not exist.");

          var f = new ObjectFactory2();
          string controlData = File.ReadAllText(_controlFileFullPath);
          var controlXml = XElement.Parse(controlData);
          var dc = f.Deserialize(controlXml) as DiagnosticsControl;

          if (dc.RunFsActionSet)
          {
            using (var fsEngine = new FSEngine())
            {
              taskResult.TaskResultSet.Add(taskResult.TaskResultSet.Count, fsEngine.Run(dc.FSActionSet));
            }
          }

          taskResult.TaskResultStatus = taskResult.TaskResultSet.GetWorstResult();

          return taskResult;
        });

        System.Threading.Thread.Sleep(2000);

        return taskResult;
      }
      catch (Exception ex)
      {
        return taskResult.Failed("An exception occurred during " + base.TaskRequest.TaskName + " task processing.", ex);
      }
    }

    private void Initialize()
    {
      base.Initialize();

      this.AssertParmExistence("ControlFileFullPath");

      _controlFileFullPath = this.GetParmValue("ControlFileFullPath").ToString();
    }


    ~Diagnostics()
    {
      Dispose(false);
    }

  }
}