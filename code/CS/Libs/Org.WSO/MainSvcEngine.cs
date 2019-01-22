using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Xml.Linq;
using Org.GS;
using Org.WSO.Transactions;

namespace Org.WSO
{
  public class MainSvcEngine : TransactionEngine, IDisposable
  {
    private ServiceBase _serviceBase;

    public MainSvcEngine(ServiceBase serviceBase)
    {
      _serviceBase = serviceBase;
    }
    
    public override XElement ProcessTransaction(WsMessage message)
    {
      try
      {
        Message = message;

        IRequestProcessorFactory requestProcessorFactory = null;

        switch (_serviceBase.ComponentLoadMode)
        {
          case ComponentLoadMode.LocalCatalog:
            requestProcessorFactory = _serviceBase.GetRequestProcessorFactory(TransactionHeader.ProcessorNameAndVersion);
            break;

          case ComponentLoadMode.CentralCatalog:
            var catalogEntry = _serviceBase.GetCatalogEntry(TransactionHeader.TransactionName);
            string catalogName = _serviceBase.CatalogName;
            requestProcessorFactory = _serviceBase.GetRequestProcessorFactoryFromAppDomain(catalogEntry.AssemblyName, 
                                                   TransactionHeader.ProcessorNameAndVersion, catalogName, "STD", catalogEntry.ObjectTypeName);
            break;

          case ComponentLoadMode.NotSet:
            throw new Exception("The web service's 'ComponentLoadMode' is 'NotSet'.");
        }

        if (requestProcessorFactory == null)
          throw new Exception("RequestProcessorFactory for transaction '" + TransactionHeader.ProcessorNameAndVersion + "' not found."); 

        using (var requestProcessor = requestProcessorFactory.CreateRequestProcessor(TransactionHeader.ProcessorNameAndVersion))
        {
          if (requestProcessor == null)
            throw new Exception("RequestProcessor not created by RequestProcessorFactory (" + requestProcessorFactory.Name + ") " + 
                                "for transaction '" + TransactionHeader.TransactionName + "' version '" + TransactionHeader.TransactionVersion + "'."); 

          requestProcessor.SetBaseAndEngine(_serviceBase, this); 
          var transactionResult = requestProcessor.ProcessRequest();
          return transactionResult;
        }
      }
      catch(Exception ex)
      {
        string messageXml = Message != null ? Message.GetXml().ToString() : "Message is null";
        throw new Exception("An exception occurred during the ProcessTransaction method." + messageXml, ex); 
      }
    }

  }
}
