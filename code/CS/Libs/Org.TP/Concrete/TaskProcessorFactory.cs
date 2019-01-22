using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using Org.TP;
using Org.GS;

namespace Org.TP.TaskProcessors
{
  [Export(typeof(ITaskProcessorFactory))]
  [ExportMetadata("Name", "CommonProcessors")]
  [ExportMetadata("Version", "1.0.0.0")]
  [ExportMetadata("Processors",
                  "Dummy_1.0.0.0 " +
                  "Diagnostics_1.0.0.0 "
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
        case "Dummy_1.0.0.0":
          return new Dummy();

        case "Diagnostics_1.0.0.0":
          return new Diagnostics();
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
