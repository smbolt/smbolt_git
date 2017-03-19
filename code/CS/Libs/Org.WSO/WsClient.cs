using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.WSO
{
  public static class WsClient
  {
    public static object InvokeServiceCall_LockObject = new object();
    public static WsMessage InvokeServiceCall(WsParms wsParms, WsMessage requestMessage)
    {
      lock (InvokeServiceCall_LockObject)
      {
        try
        {
          string endpoint = wsParms.ConfigWsSpec.WebServiceEndpoint;
          int sendTimeoutSeconds = wsParms.SendTimeoutSeconds;
          WsMessage responseMessage = SimpleServiceMessaging.SendMessage(requestMessage, endpoint, sendTimeoutSeconds);
          return responseMessage;
        }
        catch (Exception ex)
        {
          throw new Exception("An exception occurred in WsClient.InvokeServiceCall.", ex);
        }
      }
    }
  }
}
