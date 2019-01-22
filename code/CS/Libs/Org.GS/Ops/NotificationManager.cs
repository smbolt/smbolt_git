using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS.Configuration;
using Org.GS.Network;
using Org.GS.Logging;

namespace Org.GS
{
  public class NotificationManager
  {
    public NotificationManager()
    {
    }

    public void SendAlert(string notificationConfigName, string appName, string className, string methodName, string locationCode, string alertCode, string message, Exception ex)
    {

      int secondOfDay = (DateTime.Now.Hour * 3600) + (DateTime.Now.Minute * 60) + DateTime.Now.Second;
      string timeWork = secondOfDay.ToString("00000") + DateTime.Now.Millisecond.ToString("000");
      string timeWork2 = timeWork.Substring(0, 4) + "-" + timeWork.Substring(4);
      string alertID = DateTime.Now.DayOfYear.ToString("000") + "-" + DateTime.Now.ToString("HHmmss") + "-" + DateTime.Now.ToString("fff");

      List<string> notificationGroups = new List<string>();
      List<string> emailAddresses = new List<string>();
      List<string> smsNumbers = new List<string>();

      if (g.AppConfig.IsLoaded)
      {
        NotifyConfig notifyConfig = null;
        var notifyConfigSet = g.AppConfig.ProgramConfigSet[g.AppConfig.ConfigName].NotifyConfigSet;
        if (notifyConfigSet != null && notifyConfigSet.ContainsKey(notificationConfigName))
          notifyConfig = notifyConfigSet[notificationConfigName];

        if (notifyConfig != null && notifyConfig.NotifyEventSet != null)
        {
          if (notifyConfig.NotifyEventSet.ContainsKey("ServiceError"))
          {
            notificationGroups = notifyConfig.NotifyEventSet["ServiceError"].GetAllGroupNames();
            emailAddresses = notifyConfig.NotifyGroupSet.GetAllEmailAddresses(notificationGroups);
            smsNumbers = notifyConfig.NotifyGroupSet.GetAllSmsNumbers(notificationGroups);
          }
        }
      }

      if (emailAddresses.Count == 0)
      {
        if (g.AppConfig.ContainsKey("DefaultEmailAddress1"))
          emailAddresses.Add(g.AppConfig.GetCI("DefaultEmailAddress1"));
        if (g.AppConfig.ContainsKey("DefaultEmailAddress2"))
          emailAddresses.Add(g.AppConfig.GetCI("DefaultEmailAddress2"));
      }

      if (smsNumbers.Count == 0)
      {
        if (g.AppConfig.ContainsKey("DefaultSmsNumber1"))
          smsNumbers.Add(g.AppConfig.GetCI("DefaultSmsNumber1"));
        if (g.AppConfig.ContainsKey("DefaultSmsNumber2"))
          smsNumbers.Add(g.AppConfig.GetCI("DefaultSmsNumber2"));
      }

      foreach (string emailAddress in emailAddresses)
      {
        EmailMessage emailMessage = new EmailMessage();
        emailMessage.FromAddress = "stephen.m.bolt@gmail.com";
        emailMessage.AddToAddress(emailAddress);
        string subject = "ADO C" + alertID + " " + appName + ":" + locationCode + " " + message;
        emailMessage.Subject = subject.PadRight(125).Substring(0, 125).Trim();
        emailMessage.Body = BuildEmailBody(alertCode, "No description", LogSeverity.MAJR, appName, className, methodName, locationCode, message, ex);
        new EmailClient().SendEmailMessage(emailMessage, g.DefaultConfigSmtpSpec);
      }

      bool useSmsAlerts = false;

      if (g.AppConfig.ContainsKey("UseSmsAlerts"))
        useSmsAlerts = g.AppConfig.GetBoolean("UseSmsAlerts");

      if (useSmsAlerts)
      {
        foreach (string smsNumber in smsNumbers)
        {
          CdyneSmsParms smsParms = new CdyneSmsParms();
          smsParms = new CdyneSmsParms();
          smsParms.AlertCode = alertCode;
          smsParms.AlertID = alertID;
          smsParms.UseHardCodedParms = true;
          smsParms.PhoneNumber = smsNumber;
          smsParms.IPAddress = NetworkHelper.GetCurrentIpAddress();
          smsParms.AppName = appName;
          smsParms.ClassName = className;
          smsParms.MethodName = methodName;
          smsParms.LocationCode = locationCode;
          smsParms.CustomerID = g.AppConfig.GetCI("CustomerID");
          if (ex != null)
            smsParms.Message = message + "; " + ex.Message;
          else
            smsParms.Message = "Test message";
          smsParms.Severity = LogSeverity.MAJR;

          if (g.AppConfig.ContainsKey("CdyneSmsLicenseKey") && g.AppConfig.ContainsKey("CdyneSmsEndPoint"))
          {
            smsParms.LicenseKey = g.AppConfig.GetCI("CdyneSmsLicenseKey");
            smsParms.Endpoint = g.AppConfig.GetCI("CdyneSmsEndPoint");
            smsParms.UseHardCodedParms = false;
          }

          if (g.AppConfig.ContainsKey("SuppressSmsSend"))
            smsParms.SuppressSmsSend = g.AppConfig.GetBoolean("SuppressSmsSend");

          new SmsClient().SendSmsMessage(smsParms);
        }
      }
    }

    private string BuildEmailBody(string alertCode, string alertDescription, LogSeverity severity, string appName,
                                  string className, string methodName, string locationCode, string message, Exception ex)
    {
      string exceptionString = String.Empty;
      if (ex != null)
        exceptionString = ex.ToString();

      System.TimeZone tz = TimeZone.CurrentTimeZone;
      TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(tz.StandardName);
      DateTime centralDateTime = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);

      string emailBody = "Alert from Org Online" + g.crlf2 +
                         "Server Date/Time  : " + DateTime.Now.ToString() + g.crlf +
                         "Central Date/Time : " + centralDateTime.ToString() + g.crlf +
                         "Alert Code        : " + alertCode + g.crlf +
                         "Alert Desc        : " + alertDescription + g.crlf +
                         "Server IP Address : " + NetworkHelper.GetCurrentIpAddress() + g.crlf +
                         "Application       : " + appName + g.crlf +
                         "Class             : " + className + g.crlf +
                         "Method            : " + methodName + g.crlf +
                         "Location Code     : " + locationCode + g.crlf +
                         "Severity          : " + severity.ToString() + g.crlf2 +
                         "Message           : " + g.crlf +
                         message + g.crlf2 + exceptionString;

      return emailBody;
    }
  }
}
