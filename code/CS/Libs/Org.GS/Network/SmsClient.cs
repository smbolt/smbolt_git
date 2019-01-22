using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TPL = System.Threading.Tasks;
using System.Threading;
using Org.GS;
using Org.GS.Configuration;

namespace Org.GS.Network
{
  public class SmsClient
  {
    public void SendSmsMessage(CdyneSmsParms smsParms)
    {
      TPL.Task.Factory.StartNew(() =>
      {
        try
        {
          if (smsParms.SuppressSmsSend)
            return;

          CdyneSmsAdapter smsAdapter = new CdyneSmsAdapter();

          string message = "ADO " + smsParms.Severity.ToString().Substring(0, 1).ToUpper() + " " +
                           smsParms.AlertID + " " +
                           DateTime.Now.ToString("yyyyMMdd-HHmmss") + " " +
                           smsParms.AlertCode.PadLeft(3, '0').Substring(0, 3) + " " +
                           smsParms.IPAddress.PadRight(15).Substring(0, 15) + " " +
                           smsParms.AppName.PadRight(18).Substring(0, 18) + " " +
                           smsParms.ClassName.PadRight(18).Substring(0, 18) + " " +
                           smsParms.MethodName.PadRight(18).Substring(0, 18) + " " +
                           smsParms.LocationCode.PadLeft(4, '0').Substring(0, 4) + " " +
                           smsParms.Message;

          message = message.Replace(@"\", @"/");

          if (smsParms.UseHardCodedParms)
          {
            message = message.Substring(0, 6) + "(NoSvcCfg) " + message.Substring(6, message.Length - 6);
            if (message.Length > 450)
              message = message.Substring(0, 450);

            // send using hard coded parameters (in the event the configured parameters are not available (last resort)
            smsAdapter.SendSms("14056502029", message, "66d133b4-08a0-4125-9fbf-b37e5abc767d", "http://sms2.cdyne.com/sms.svc/Soap");
          }
          else
          {
            if (message.Length > 450)
              message = message.Substring(0, 450);

            if (!smsParms.PhoneNumber.IsNumeric())
              smsParms.PhoneNumber = "14056502029";

            smsAdapter.SendSms(smsParms.PhoneNumber, message, smsParms.LicenseKey, smsParms.Endpoint);
          }
        }
        catch (Exception ex)
        {
          string exMessage = ex.Message;
        }
      });
    }
  }
}
