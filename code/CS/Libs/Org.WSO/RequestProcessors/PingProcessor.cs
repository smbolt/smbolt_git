using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using System.Reflection;
using System.Xml.Linq;
using Org.GS;
using Org.GS.Logging;
using Org.WSO.Transactions;

namespace Org.WSO
{
  [Serializable]
  public class PingProcessor : RequestProcessorBase, IRequestProcessor, IDisposable
  {
    public override int EntityId {
      get {
        return 301;
      }
    }
    public static bool _isMapped = false;

    public PingProcessor()
    {
      if (!_isMapped)
      {
        if (XmlMapper.AddAssembly(Assembly.GetExecutingAssembly()))
          g.LogToMemory("PingProcessor MappingTypes");
        _isMapped = true;
      }
    }

    public override XElement ProcessRequest()
    {
      base.Initialize(MethodBase.GetCurrentMethod());
      var f = new ObjectFactory2();

      try
      {
        var request = f.Deserialize(base.TransactionEngine.TransactionBody) as PingRequest;
        var response = new PingResponse();
        response.TransactionStatus = TransactionStatus.Success;
        var transactionBody = f.Serialize(response);

        if (ServiceBase != null)
        {
          int code = 6019;
          string message = "PingProcessor web service request processing is complete (code " + code.ToString() + ").";
          ServiceBase.Logger.Log(message, code, this.EntityId);
          ServiceBase.NotifyHost(new IpdxMessage("TaskEngine", message));
        }

        return transactionBody;
      }
      catch(Exception ex)
      {
        int code = 6016;
        string errorMessage = "An exception occurred processing the PingProcessor.ProcessRequest web service request (code " + code.ToString() + ").";
        base.ServiceBase.Logger.Log(LogSeverity.SEVR, errorMessage, code, this.EntityId, ex);
        var errorResponse = new ErrorResponse();
        errorResponse.TransactionStatus = TransactionStatus.Error;
        errorResponse.Message = errorMessage;
        errorResponse.Exception = ex;
        var transactionBody = f.Serialize(errorResponse);
        return transactionBody;
      }
    }
  }
}
