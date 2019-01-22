using System;
using System.ComponentModel.Composition;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using Org.TP;
using Org.GS;

namespace Org.ShareFile.Tasks
{
  [Export(typeof(ITaskProcessorFactory))]
  [ExportMetadata("Name", "ShareFileProcessors")]
  [ExportMetadata("Version", "1.0.0.0")]
  [ExportMetadata("Processors",
                  "ShareFileUtility_1.0.0.0 "
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
        case "ShareFileUtility_1.0.0.0":
          return new ShareFileUtility();

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
