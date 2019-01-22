using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Threading.Tasks;
using System.Reflection;
using Org.WSO;
using Org.WSO.Transactions;
using Org.GS.Logging;
using Org.GS;

namespace Org.TSK
{
  public class TaskEngineRequestProcessor : RequestProcessorBase, IRequestProcessor, IDisposable
  {
    private static Func<TransactionBase, TaskResult> HostTransactionProcessor;

    public override int EntityId { get { return 399; } }
    public static bool _isMapped = false;

    public TaskEngineRequestProcessor()
    {
      if (!_isMapped)
      {
        XmlMapper.AddAssembly(Assembly.GetExecutingAssembly());
      }
      _isMapped = true;
    }

    public override XElement ProcessRequest()
    {
      base.Initialize(MethodBase.GetCurrentMethod());
      var f = new ObjectFactory2();
      TaskResult taskResult = null;

      TransactionBase response = null; 

      try
      {
        var request = f.Deserialize(base.TransactionEngine.TransactionBody) as TransactionBase;

        if (HostTransactionProcessor == null)
          throw new Exception("The TaskEngine HostTransactionProcessor method is null.");

        switch (request.Name)
        {
          case "PingRequest":
            taskResult = HostTransactionProcessor(request); 
            response = new PingResponse();
            break;

          case "GetAssemblyReportRequest":
            response = new GetAssemblyReportResponse();
            taskResult = HostTransactionProcessor(request);
            if (taskResult.TaskResultStatus == TaskResultStatus.Success)
              ((GetAssemblyReportResponse)response).AssemblyReport = taskResult.Data;
            else
              throw new Exception("An exception occurred while attempting to create the Assembly Report." + g.crlf + taskResult.NotificationMessage);
            break;

          case "GetRunningTasksReportRequest":
            response = new GetRunningTasksReportResponse();
            taskResult = HostTransactionProcessor(request);
            if (taskResult.TaskResultStatus == TaskResultStatus.Success)
              ((GetRunningTasksReportResponse)response).RunningTasksReport = taskResult.Data;
            else
              throw new Exception("An exception occurred while attempting to create the Running Tasks Report." + g.crlf + taskResult.NotificationMessage);

            break;

          default:
            throw new Exception("The TaskEngine web service request processor does not implement processing for request type '" + request.Name + "'."); 
        }
        
        var transactionBody = f.Serialize(response);

        return transactionBody;
      }
      catch (Exception ex)
      {
        int code = 6021;
        string errorMessage = "An exception occurred processing the TaskEngine web service request (code " + code.ToString() + ").";
        base.ServiceBase.Logger.Log(LogSeverity.SEVR, errorMessage, code, this.EntityId, ex);
        var errorResponse = new ErrorResponse();
        errorResponse.TransactionStatus = TransactionStatus.Error;
        errorResponse.Message = errorMessage;
        errorResponse.Exception = ex;
        var transactionBody = f.Serialize(errorResponse);
        return transactionBody;
      }
    }

    public void WireUpTransactionProcessor(Func<TransactionBase, TaskResult> hostTransactionProcessor)
    {
      HostTransactionProcessor = hostTransactionProcessor;
    }
  }
}
