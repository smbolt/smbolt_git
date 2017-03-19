using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.TP;
using Org.GS;

namespace Org.PDF.Tasks
{
  [Export(typeof(ITaskProcessorFactory))]
  [ExportMetadata("Name", "PdfTaskProcessors")]
  [ExportMetadata("Version", "1.0.0.0")]
  [ExportMetadata("Processors",
                  "PdfSearch_1.0.0.0 "
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
        case "PdfSearch_1.0.0.0":
          return new PdfSearch();
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
