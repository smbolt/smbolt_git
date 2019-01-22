using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using Org.WSO;
using Org.WSO.Transactions;
using Org.GS;
using Org.GS.Logging;

namespace Org.WS
{
  public class MainWebService : ServiceBase, ISimpleService
  {
    public static event EventHandler<WsTransaction> TransactionReceived;  
    public override int EntityId { get { return 305; } }

    public MainWebService()
    {
      base.EntityId = this.EntityId;
      XmlMapper.AddAssembly(Assembly.GetAssembly(typeof(MainSvcEngine)));
    }

    public string SendMessage(string messageString)
    {
      try
      {
        PreTransactionProcessing(messageString);

        using (var engine = new MainSvcEngine(this))
        {
          ReceivedMessage.AddPerfInfoEntry("After engine creation / before ProcessTransaction");
          XElement result = engine.ProcessTransaction(ReceivedMessage);
          var responseMessage = PostTransactionProcessing(result);
          Logger.ClearClientProperties();
          return responseMessage;
        }
      }
      catch (Exception ex)
      {
        string message = "An exception occurred during web service execution." + g.crlf2 + base.ServiceInfo + g.crlf2 + ex.ToReport() + g.crlf2 + base.ReceivedMessageString;
        base.ProcessNotifications(g.AppInfo.AppName + " Exception Occurred in Web Service Execution - Code 6118", message);
        Logger.Log(LogSeverity.SEVR, message, 6118, this.EntityId, ex);
        Logger.ClearClientProperties();
        WsMessage errorMessage = MessageFactory.CreateErrorMessage(ReceivedMessage, ResponseMessage, ex, base.OrgId);
        errorMessage.MessageHeader.ReceiverRespondDateTime = MessageFactory.GetWebServiceDateTime();
        XElement errorMessageXml = errorMessage.GetXml();
        string encryptedErrorMessage = base.Encryptor.EncryptString(errorMessageXml.ToString());
        return encryptedErrorMessage;
      }
    }
  }
}
