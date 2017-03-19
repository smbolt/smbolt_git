using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using Org.WSO;
using Org.WSO.Transactions;
using Org.GS;

namespace Org.Dx.Business
{
  [Export(typeof(IMessageFactory))]
  [ExportMetadata("Name", "Org.Dx.Business.MessageFactory")]
  [ExportMetadata("Version", "1.0.0.0")]
  [ExportMetadata("Transactions",
                  "ExcelExtract_1.0.0.0 "
                  )]
  public class MessageFactory : MessageFactoryBase, IMessageFactory
  {
    public MessageFactory() { }

    public WsMessage CreateRequestMessage(WsParms wsParms)
    {
      try
      {
        WsMessage requestMessage = base.InitWsMessage(wsParms);
        TransactionBase trans = null;

        switch (wsParms.TransactionName)
        {
          case "ExcelExtract":
            trans = Build_ExcelExtract(wsParms);
            break;

          default:
            trans = base.CreateTransaction(wsParms);
            break;
        }

        if (trans == null)
          throw new Exception("MessageFactory is not able to create web service request message for transaction '" + wsParms.TransactionName + "'.");

        trans.Name = wsParms.TransactionName;
        trans.Version = wsParms.TransactionVersion;
        requestMessage.TransactionBody = this.ObjectFactory.Serialize(trans);
        return requestMessage;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred attempting to create the request message.", ex);
      }
    }

    private TransactionBase Build_ExcelExtract(WsParms wsParms)
    {
      ExcelExtractRequest trans = new ExcelExtractRequest();

      var fullPathParm = wsParms.ParmSet.Where(p => p.ParameterName == "FullPath").FirstOrDefault();
      if (fullPathParm != null)
        trans.FullPath = fullPathParm.ParameterValue.ToString();

      var worksheetsToInclude = wsParms.ParmSet.Where(p => p.ParameterName == "WorksheetsToInclude").FirstOrDefault();
      if (worksheetsToInclude != null)
        trans.WorksheetsToInclude = (List<string>) worksheetsToInclude.ParameterValue;

      return trans;
    }
  }
}
