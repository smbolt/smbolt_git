using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Threading.Tasks;
using Org.WSO;
using Org.GS;

namespace Org.WinSvc
{
  public class WebServiceEngine : TransactionEngine, IDisposable
  {
    private ServiceBase _serviceBase;

    public WebServiceEngine(ServiceBase serviceBase)
    {
      _serviceBase = serviceBase;
    }

    public override XElement ProcessTransaction(WsMessage message)
    {
      try
      {
        Message = message;

        var requestProcessorFactory = _serviceBase.GetRequestProcessorFactory(TransactionHeader.ProcessorNameAndVersion);
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
      catch (Exception ex)
      {
        string messageXml = Message != null ? Message.GetXml().ToString() : "Message is null";
        throw new Exception("An exception occurred during the ProcessTransaction method." + messageXml, ex);
      }
    }
  }
}
