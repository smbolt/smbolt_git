using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using Org.WSO;
using Org.WSO.Transactions;
using Org.GS;

namespace Org.WSO
{
  [Export(typeof(IMessageFactory))]
  [ExportMetadata("Name", "Org.WSO.MessageFactory")]
  [ExportMetadata("Version", "1.0.0.0")]
  [ExportMetadata("Transactions",
                  "Ping_1.0.0.0 " +
                  "GetAssemblyReport_1.0.0.0 " +
                  "GetRunningTasksReport_1.0.0.0 " +
                  "WsCommand_1.0.0.0 " +
                  "Transaction2_1.0.0.0 "
                 )]
  public class MessageFactory : MessageFactoryBase, IMessageFactory
  {
    public MessageFactory() {}

    public WsMessage CreateRequestMessage(WsParms wsParms)
    {
      try
      {
        WsMessage requestMessage = base.InitWsMessage(wsParms);
        TransactionBase trans = base.CreateTransaction(wsParms);
        requestMessage.MessageBody.Transaction.Transaction = trans;

        if (trans == null)
          throw new Exception("MessageFactory is not able to create web service request message for transaction '" + wsParms.TransactionName + "'.");

        requestMessage.TransactionBody = this.ObjectFactory.Serialize(trans);
        return requestMessage;
      }
      catch(Exception ex)
      {
        throw new Exception("An exception occurred attempting to create the request message.", ex);
      }
    }

  }


}
