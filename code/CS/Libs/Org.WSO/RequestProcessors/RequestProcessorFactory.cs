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
using Org.WSO;
using Org.WSO.Transactions;
using Org.GS.AppDomainManagement;
using Org.GS;

namespace Org.WSO.RequestProcessors
{
  [Export(typeof(IRequestProcessorFactory))]
  [ExportMetadata("Name", "WsoRequestProcessorFactory")]
  [ExportMetadata("Version", "1.0.0.0")]
  [ExportMetadata("Processors", 
                  "PingProcessor_1.0.0.0 " + 
                  "ServiceEngineCommandProcessor_1.0.0.0 " + 
                  "WsCommandProcessor_1.0.0.0 "
                  )]
  [Serializable]
  public class RequestProcessorFactory : MarshalBase, IRequestProcessorFactory, IDisposable
  {
    public string Name { get { return "WsoRequestProcessorFactory"; } }

    public RequestProcessorFactory()
    {
      g.LogToMemory("RequestProcessorFactory Created");
    }

    public IRequestProcessor CreateRequestProcessor(string nameAndVersion)
    {
      g.LogToMemory("RequestProcessorFactory.CreateRequestProcessor: " + nameAndVersion);
      switch(nameAndVersion)
      {
        case "PingProcessor_1.0.0.0":
          return new PingProcessor();

        case "ServiceEngineCommandProcessor_1.0.0.0":
          return new ServiceEngineCommandProcessor();

        case "WsCommandProcessor_1.0.0.0":
          return new WsCommandProcessor();
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
