using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Mail;

namespace Org.GS
{
  public class EmailHelper : IDisposable
  {
    public void SendEmail(SmtpParmsSet smtpParmsSet, MailMessage mailMsg)
    {
      foreach (SmtpParms smtpParmsAttempt in smtpParmsSet)
      {
        try
        {
          if (smtpParmsAttempt.SuppressEmailSend)
            return;

          using (var smtpClient = new SmtpClient(smtpParmsAttempt.SmtpServer, smtpParmsAttempt.SmtpPort))
          {
            if (smtpParmsAttempt.PickUpFromIIS)
              smtpClient.DeliveryMethod = SmtpDeliveryMethod.PickupDirectoryFromIis;

            if (smtpParmsAttempt.UseSmtpCredentials)
            {
              System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(smtpParmsAttempt.SmtpUserID, smtpParmsAttempt.SmtpPassword);
              smtpClient.Credentials = credentials;
            }

            smtpClient.Send(mailMsg);
          }
        }
        catch (Exception ex)
        {
          throw new Exception("An exception occurred attempting to send an email.", ex); 
        }
      }
    }

    public void Dispose()
    {

    }
  }
}
