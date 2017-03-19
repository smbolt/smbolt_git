using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Threading;
using Org.GS;
using Org.GS.Configuration;

namespace Org.GS.Network
{
  public class EmailClient
  {
    public void SendEmailMessage(EmailMessage emailMessage, ConfigSmtpSpec smtpSpec)
    {
      Task.Factory.StartNew(() =>
      {
        try
        {
          MailMessage mailMsg = new MailMessage();

          foreach (string toAddress in emailMessage.ToAddresses)
            mailMsg.To.Add(toAddress);

          foreach (string ccAddress in emailMessage.CcAddresses)
            mailMsg.CC.Add(ccAddress);

          foreach (string bccAddress in emailMessage.BccAddresses)
            mailMsg.Bcc.Add(bccAddress);

          mailMsg.From = new MailAddress(emailMessage.FromAddress);
          mailMsg.Subject = emailMessage.Subject;
          mailMsg.Body = emailMessage.Body;                    

          SmtpClient smtpClient = new SmtpClient(smtpSpec.SmtpServer, Int32.Parse(smtpSpec.SmtpPort));

          if (smtpSpec.PickUpFromIIS)
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.PickupDirectoryFromIis;

          if (!smtpSpec.AllowAnonymous)
          {
            System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(smtpSpec.SmtpUserID, smtpSpec.SmtpPassword);
            smtpClient.Credentials = credentials;
          }

          smtpClient.Send(mailMsg);
        }
        catch (Exception ex)
        {
          string message = ex.Message;
        }
      });
    }
  }
}
