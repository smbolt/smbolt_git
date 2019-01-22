using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Org.Software.Tasks.Transactions;
using System.ComponentModel.Composition;
using Org.WSO;
using Org.WSO.Transactions;
using Org.GS;

namespace Org.SoftwareTasks
{
  [Export(typeof(IMessageFactory))]
  [ExportMetadata("Name", "Org.SoftwareTasks.MessageFactory")]
  [ExportMetadata("Version", "1.0.0.0")]
  [ExportMetadata("Transactions",
                  "CheckForUpdates_1.0.0.0 " +
                  "DownloadSoftware_1.0.0.0 " +
                  "GetFrameworkVersions_1.0.0.0 "
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
          case "CheckForUpdates":
            trans = Build_CheckForUpdates(wsParms);
            break;

          case "DownloadSoftware":
            trans = Build_DownloadSoftware(wsParms);
            break;

          case "GetFrameworkVersions":
            trans = Build_GetFrameworkVersions(wsParms); 
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

    private TransactionBase Build_CheckForUpdates(WsParms wsParms)
    {
      CheckForUpdatesRequest trans = new CheckForUpdatesRequest();
      trans.OrgId = wsParms.OrgId;
      trans.DomainName = wsParms.DomainName;
      trans.MachineName = wsParms.MachineName;
      trans.UserName = wsParms.UserName;
      trans.ModuleCode = wsParms.ModuleCode;
      trans.ModuleName = wsParms.ModuleName;
      trans.CurrentVersion = wsParms.ModuleVersion;
      trans.CurrentPlatformString = g.SystemInfo.PlatformString;
      return trans;
    }

    private TransactionBase Build_DownloadSoftware(WsParms wsParms)
    {
      DownloadSoftwareRequest trans = new DownloadSoftwareRequest();
      trans.OrgId = wsParms.OrgId;
      trans.DomainName = wsParms.DomainName;
      trans.MachineName = wsParms.MachineName;
      trans.UserName = wsParms.UserName;
      trans.ModuleCode = wsParms.ModuleCode;
      trans.ModuleName = wsParms.ModuleName;
      trans.CurrentVersion = wsParms.ModuleVersion;
      trans.CurrentPlatformString = g.SystemInfo.PlatformString;
      return trans;
    }

    private TransactionBase Build_GetFrameworkVersions(WsParms wsParms)
    {
      GetFrameworkVersionsRequest trans = new GetFrameworkVersionsRequest();
      return trans;
    }
  }
}
