using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Org.GS
{
  public class EmailConfiguration
  {
    private string _smtpHost;
    public string SmtpHost
    {
      get {
        return _smtpHost;
      }
      set {
        _smtpHost = value;
      }
    }

    private int _smtpPort;
    public int SmtpPort
    {
      get {
        return _smtpPort;
      }
      set {
        _smtpPort = value;
      }
    }

    private string _smtpUser;
    public string SmtpUser
    {
      get {
        return _smtpUser;
      }
      set {
        _smtpUser = value;
      }
    }

    private string _smtpPassword;
    public string SmtpPassword
    {
      get {
        return _smtpPassword;
      }
      set {
        _smtpPassword = value;
      }
    }

    private bool _pickUpFromIIS;
    public bool PickUpFromIIS
    {
      get {
        return _pickUpFromIIS;
      }
      set {
        _pickUpFromIIS = value;
      }
    }

    private bool _suppressEmailSend;
    public bool SuppressEmailSend
    {
      get {
        return _suppressEmailSend;
      }
      set {
        _suppressEmailSend = value;
      }
    }

    public EmailConfiguration()
    {
      _smtpHost = String.Empty;
      _smtpPort = 0;
      _smtpUser = String.Empty;
      _smtpPassword = String.Empty;
      _pickUpFromIIS = false;
      _suppressEmailSend = false;
    }
  }
}
