using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.Net.Mail;
using Org.GS.Configuration;
using Org.GS;

namespace Org.GS.Notifications
{
  public class EmailNotificationProcessor : INotificationProcessor, IDisposable
  {
    public async Task<TaskResult> SendMessageAsync(SmtpParms smtpParms, EmailNotificationMessage emailMessage)
    {
      var taskResult = new TaskResult("SendMessage");

      try
      {
        if (!smtpParms.SuppressEmailSend)
        {
          string emailAddresses = String.Empty;

          using (var smtpClient = new SmtpClient(smtpParms.SmtpServer, smtpParms.SmtpPort))
          {
            if (smtpParms.PickUpFromIIS)
              smtpClient.DeliveryMethod = SmtpDeliveryMethod.PickupDirectoryFromIis;

            if (smtpParms.UseSmtpCredentials)
            {
              System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(smtpParms.SmtpUserID, smtpParms.SmtpPassword);
              smtpClient.Credentials = credentials;
            }

            var mailMessage = new System.Net.Mail.MailMessage();

            foreach (var address in emailMessage.NotificationAddresses)
            {
              switch (address.NotificationAddressType)
              {
                case NotificationAddressType.EmailToAddress:
                  mailMessage.To.Add(address.NotificationAddress);
                  emailAddresses += "TO(" + address.NotificationAddress + ") ";
                  break;
                case NotificationAddressType.EmailFromAddress:
                  mailMessage.From = new MailAddress(address.NotificationAddress);
                  emailAddresses += "FROM(" + address.NotificationAddress + ") ";
                  break;
                case NotificationAddressType.EmailCcAddress:
                  mailMessage.CC.Add(address.NotificationAddress);
                  emailAddresses += "CC(" + address.NotificationAddress + ") ";
                  break;
                case NotificationAddressType.EmailBccAddress:
                  mailMessage.Bcc.Add(address.NotificationAddress);
                  emailAddresses += "BCC(" + address.NotificationAddress + ") ";
                  break;
              }
            }

            emailAddresses = emailAddresses.Trim();

            mailMessage.Subject = emailMessage.NotificationMessageSubject;
            mailMessage.Body = emailMessage.NotificationMessageBody;
            mailMessage.IsBodyHtml = emailMessage.IsBodyHtml;
            System.Threading.Thread.Sleep(3000);
            await smtpClient.SendMailAsync(mailMessage).ContinueWith(r =>
            {
              if (r != null)
              {
                switch(r.Status)
                {
                  case TaskStatus.RanToCompletion:
                    taskResult.Data = "ADDRESSES:" + emailAddresses + " SUBJECT:" + mailMessage.Subject.PadTo(50).Trim();
                    taskResult.Success("Email sent successfully.");
                    break;

                  case TaskStatus.Faulted:
                    taskResult.Data = "ADDRESSES:" + emailAddresses + " SUBJECT:" + mailMessage.Subject.PadTo(50).Trim();
                    taskResult.Failed("Asynchronous send of email failed - task status is '" + r.Status.ToString() + "'.");
                    if (r.Exception != null)
                    {
                      taskResult.Exception = r.Exception;
                    }
                    break;

                  default:
                    taskResult.Data = "ADDRESSES:" + emailAddresses + " SUBJECT:" + mailMessage.Subject.PadTo(50).Trim();
                    taskResult.Failed("Asynchronous send of email failed - task status is '" + r.Status.ToString() + "'.");
                    break;
                }
              }
            });
          }
        }

        return taskResult;
      }
      catch (Exception ex)
      {
        return taskResult.Failed("An exception occurred sending the email message.", ex);
      }
    }


    public TaskResult SendMessage(SmtpParms smtpParms, EmailNotificationMessage emailMessage)
    {
      var taskResult = new TaskResult("SendMessage");

      try
      {
        if (!smtpParms.SuppressEmailSend)
        {
          using (var smtpClient = new SmtpClient(smtpParms.SmtpServer, smtpParms.SmtpPort))
          {
            if (smtpParms.PickUpFromIIS)
              smtpClient.DeliveryMethod = SmtpDeliveryMethod.PickupDirectoryFromIis;

            if (smtpParms.UseSmtpCredentials)
            {
              System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(smtpParms.SmtpUserID, smtpParms.SmtpPassword);
              smtpClient.Credentials = credentials;
            }

            var mailMessage = new System.Net.Mail.MailMessage();

            foreach (var address in emailMessage.NotificationAddresses)
            {
              switch (address.NotificationAddressType)
              {
                case NotificationAddressType.EmailToAddress:
                  mailMessage.To.Add(address.NotificationAddress);
                  break;
                case NotificationAddressType.EmailFromAddress:
                  mailMessage.From = new MailAddress(address.NotificationAddress);
                  break;
                case NotificationAddressType.EmailCcAddress:
                  mailMessage.CC.Add(address.NotificationAddress);
                  break;
                case NotificationAddressType.EmailBccAddress:
                  mailMessage.Bcc.Add(address.NotificationAddress);
                  break;
              }
            }

            mailMessage.Subject = emailMessage.NotificationMessageSubject;
            mailMessage.Body = emailMessage.NotificationMessageBody;
            mailMessage.IsBodyHtml = emailMessage.IsBodyHtml;
            smtpClient.Send(mailMessage);
          }
        }

        return taskResult.Success("Email sent successfully");
      }
      catch (Exception ex)
      {
        return taskResult.Failed("An exception occurred sending the email message.", ex);
      }
    }

    public void Dispose()
    {

    }
  }
}
