using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Org.GS.Network
{
  public class EmailMessage
  {
    public List<string> ToAddresses {
      get;
      set;
    }
    public List<string> CcAddresses {
      get;
      set;
    }
    public List<string> BccAddresses {
      get;
      set;
    }
    public string FromAddress {
      get;
      set;
    }
    public string Subject {
      get;
      set;
    }
    public string Body {
      get;
      set;
    }
    public bool IsBodyHtml {
      get;
      set;
    }


    public EmailMessage()
    {
      this.ToAddresses = new List<string>();
      this.CcAddresses = new List<string>();
      this.BccAddresses = new List<string>();
      this.FromAddress = String.Empty;
      this.Subject = String.Empty;
      this.Body = String.Empty;
      this.IsBodyHtml = false;
    }

    public void AddToAddress(string toAddress)
    {
      this.ToAddresses.Add(toAddress);
    }

    public void AddCcAddress(string ccAddress)
    {
      this.CcAddresses.Add(ccAddress);
    }

    public void AddBccAddress(string bccAddress)
    {
      this.BccAddresses.Add(bccAddress);
    }

    public void ReplaceText(Dictionary<string, string> emailParms)
    {
      foreach(var kvp in emailParms)
      {
        this.Body = this.Body.Replace(kvp.Key, kvp.Value);
      }
    }
  }
}
