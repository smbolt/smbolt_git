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
using Org.Software.Business;
using Org.Software.Business.Models;
using Org.WSO;
using Org.WSO.Transactions;
using Org.Software.Tasks.Transactions;
using Org.GS;

namespace Org.Software.Tasks.Concrete
{
  [Export(typeof(IRequestProcessorFactory))]
  [ExportMetadata("Name", "SoftwareUpdates")]
  [ExportMetadata("Version", "1.0.0.0")]
  [ExportMetadata("Processors",
                  "CheckForUpdatesProcessor_1.0.0.0 " +
                  "DownloadSoftwareProcessor_1.0.0.0 " +
                  "GetFrameworkVersionsProcessor_1.0.0.0 "
                 )]
  public class RequestProcessorFactory : IRequestProcessorFactory, IDisposable
  {
    public string Name {
      get;
      set;
    }

    public RequestProcessorFactory()
    {
      this.Name = "RequestProcessorFactory";
      g.LogToMemory("RequestProcessorFactory Created");
    }

    public IRequestProcessor CreateRequestProcessor(string nameAndVersion)
    {
      g.LogToMemory("RequestProcessorFactory.CreateRequestProcessor: " + nameAndVersion);
      switch(nameAndVersion)
      {
        case "CheckForUpdatesProcessor_1.0.0.0":
          return new CheckForUpdatesProcessor();

        case "DownloadSoftwareProcessor_1.0.0.0":
          return new DownloadSoftwareProcessor();

        case "GetFrameworkVersionsProcessor_1.0.0.0":
          return new GetFrameworkVersionsProcessor();
      }

      throw new Exception("Invalid request processor name and version requested '" + nameAndVersion + "'.");
    }

    ~RequestProcessorFactory()
    {
      Dispose();
    }

    public void Dispose()
    {
      g.LogToMemory("RequestProcessorFactory Destructor");
    }
  }
}
