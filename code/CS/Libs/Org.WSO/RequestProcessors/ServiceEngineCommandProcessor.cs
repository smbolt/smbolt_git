using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using System.Reflection;
using System.Xml.Linq;
using System.ServiceProcess;
using System.DirectoryServices;
using System.Management;
using Org.GS;
using Org.WSO.Transactions;

namespace Org.WSO
{
  [Serializable]
  public class ServiceEngineCommandProcessor : RequestProcessorBase, IRequestProcessor
  {
    private TransactionStatus _summaryStatus = TransactionStatus.Success;

    public override XElement ProcessRequest()
    {
      try
      {
        DateTime beginRequestDT = DateTime.Now;
        ServiceEngineCommandResponse commandResponse = new ServiceEngineCommandResponse();

        base.Initialize(MethodBase.GetCurrentMethod());

        ObjectFactory2 f = new ObjectFactory2();
        var commandRequest = f.Deserialize(base.TransactionEngine.TransactionBody) as ServiceEngineCommandRequest;

        var taskResult = new TaskResult();

        foreach (WsCommand command in commandRequest.WsCommandSet)
        {
          try
          {
            if (command.BeforeWaitMilliseconds > 0)
              System.Threading.Thread.Sleep(command.BeforeWaitMilliseconds);

            switch (command.WsCommandName)
            {
              case WsCommandName.FlushAppDomains:
                taskResult = this.FlushAppDomains();
                break;
            }

            if (command.AfterWaitMilliseconds > 0)
              System.Threading.Thread.Sleep(command.AfterWaitMilliseconds);

            WsCommand commandResult = new WsCommand();
            commandResult.WsCommandName = command.WsCommandName;
            commandResult.TaskResultStatus = taskResult.TaskResultStatus;
            commandResult.Message = taskResult.Message;
            commandResult.SetDuration(taskResult.BeginDateTime, DateTime.Now);
            commandResult.BeforeWaitMilliseconds = command.BeforeWaitMilliseconds;
            commandResult.AfterWaitMilliseconds = command.AfterWaitMilliseconds;
            commandResult.Parms = command.Parms;

            if (taskResult.Object != null)
              commandResult.ObjectWrapper = new ObjectWrapper(taskResult.Object);

            commandResponse.WsCommandSet.Add(commandResult);
            TransactionStatus commandStatus = g.ToEnum<TransactionStatus>(g.TranslateStatus(taskResult.TaskResultStatus), TransactionStatus.NotSet);
            UpdateSummaryStatus(commandStatus);
          }
          catch (Exception ex)
          {
            WsCommand commandResult = new WsCommand();
            commandResult.WsCommandName = command.WsCommandName;
            commandResult.TaskResultStatus = TaskResultStatus.Failed;
            commandResult.Message = ex.ToReport();
            commandResult.SetDuration(taskResult.BeginDateTime, DateTime.Now);
            commandResult.BeforeWaitMilliseconds = command.BeforeWaitMilliseconds;
            commandResult.AfterWaitMilliseconds = command.AfterWaitMilliseconds;
            commandResult.Parms = command.Parms;

            commandResponse.WsCommandSet.Add(commandResult);
            TransactionStatus commandStatus = g.ToEnum<TransactionStatus>(g.TranslateStatus(taskResult.TaskResultStatus), TransactionStatus.NotSet);
            UpdateSummaryStatus(commandStatus);
            break;
          }
        }

        commandResponse.TransactionStatus = _summaryStatus;

        if (_summaryStatus == TransactionStatus.Success)
          commandResponse.Message = "All commands were processed successfully.";
        else
          commandResponse.Message = "One or more commands did not complete successfully.";

        DateTime endRequestDT = DateTime.Now;
        commandResponse.WsCommandSet.SetDuration(beginRequestDT, endRequestDT);

        //base.WriteSuccessLog("0000", "000");

        XElement responseElement = f.Serialize(commandResponse);
        return responseElement;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred when attempting to process the WsCommandRequest.", ex);
      }
    }

    private void UpdateSummaryStatus(TransactionStatus commandStatus)
    {
      if (commandStatus != TransactionStatus.Success)
        _summaryStatus = TransactionStatus.Failed;
    }

    private TaskResult FlushAppDomains()
    {
      if (this.ServiceBase == null)
        return new TaskResult("FlushAppDomains", "The ServiceBase reference of the ServiceEngineCommandProcessor is null.",
                              TaskResultStatus.Failed);

      return this.ServiceBase.FlushAppDomains();
    }
  }
}
