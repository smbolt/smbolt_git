using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;
using Org.TP;

namespace Org.Ops.Tasks
{
  [Export(typeof(ITaskProcessorFactory))]
  [ExportMetadata("Name", "CommonProcessors")]
  [ExportMetadata("Version", "1.0.0.0")]
  [ExportMetadata("Processors",
                  "OpsDiagnostics_1.0.0.0 " +
                  "OpsMaintenance_1.0.0.0 " +
                  "TaskMonitor_1.0.0.0 " +
                  "OverdueTaskAcknowledgementMonitor_1.0.0.0 " +
                  "StatementEmailMonitor_1.0.0.0 " +
                  "TaskScheduler_1.0.0.0"
                  )]

  public class TaskProcessorFactory : ITaskProcessorFactory, IDisposable
  {
    public TaskProcessorFactory()
    {
      g.LogToMemory("TaskProcessorFactory Created");
    }

    public ITaskProcessor CreateTaskProcessor(string nameAndVersion)
    {
      g.LogToMemory("TaskProcessorFactory.CreateTaskProcessor: " + nameAndVersion);
      switch (nameAndVersion)
      {
        case "OpsDiagnostics_1.0.0.0":
          return new OpsDiagnostics();

        case "OpsMaintenance_1.0.0.0":
          return new OpsMaintenance();

        case "TaskMonitor_1.0.0.0":
          return new TaskMonitor();

        case "OverdueTaskAcknowledgementMonitor_1.0.0.0":
          return new OverdueTaskAcknowledgementMonitor();

        case "StatementEmailMonitor_1.0.0.0":
          return new StatementEmailMonitor();
        
        case "TaskScheduler_1.0.0.0":
          return new TaskScheduler();
      }

      throw new Exception("Invalid task processor name and version requested '" + nameAndVersion + "'.");
    }

    ~TaskProcessorFactory()
    {
      Dispose();
    }

    public void Dispose()
    {
      g.LogToMemory("TaskProcessorFactory Destructor");
    }
  }
}
