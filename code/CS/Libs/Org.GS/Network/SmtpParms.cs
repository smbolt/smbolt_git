using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Org.GS.Configuration;

namespace Org.GS
{
  public enum EmailSendStatus
  {
    NotAttempted,
    Success,
    Failed
  }

  public class SmtpParms
  {
    public string SmtpServer {
      get;
      set;
    }
    public int SmtpPort {
      get;
      set;
    }
    public bool UseSmtpCredentials {
      get;
      set;
    }
    public string SmtpUserID {
      get;
      set;
    }
    public string SmtpPassword {
      get;
      set;
    }
    public bool SmtpEnableSSL {
      get;
      set;
    }
    public bool PickUpFromIIS {
      get;
      set;
    }
    public bool SuppressEmailSend {
      get;
      set;
    }
    public string EmailFromAddress {
      get;
      set;
    }
    public EmailSendStatus EmailSendStatus {
      get;
      set;
    }

    public SmtpParms()
    {
      this.SmtpServer = String.Empty;
      this.SmtpPort = 0;
      this.UseSmtpCredentials = false;
      this.SmtpUserID = String.Empty;
      this.SmtpPassword = String.Empty;
      this.PickUpFromIIS = false;
      this.SuppressEmailSend = false;
      this.SmtpEnableSSL = false;
      this.EmailFromAddress = String.Empty;
      this.EmailSendStatus = EmailSendStatus.NotAttempted;
    }

    public SmtpParms(ConfigSmtpSpec spec)
    {
      this.SmtpServer = spec.SmtpServer;
      this.SmtpPort = spec.SmtpPort.ToInt32();
      this.SmtpUserID = spec.SmtpUserID;
      this.SmtpPassword = spec.SmtpPassword;
      this.PickUpFromIIS = spec.PickUpFromIIS;
      this.SuppressEmailSend = false;
      this.SmtpEnableSSL = spec.EnableSSL;
      this.EmailFromAddress = spec.EmailFromAddress;
      this.EmailSendStatus = EmailSendStatus.NotAttempted;

      if (this.SmtpUserID.IsNotBlank() && this.SmtpPassword.IsNotBlank())
        this.UseSmtpCredentials = true;
      else
        this.UseSmtpCredentials = false;
    }
  }
}

