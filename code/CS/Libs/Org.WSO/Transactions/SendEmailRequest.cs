using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Org.GS;

namespace Org.WSO.Transactions
{
  [XMap(XType = XType.Element)]
  public class SendEmailRequest : TransactionBase
  {
    [XMap]
    public List<string> ToAddresses { get; set; }
    [XMap]
    public List<string> CcAddresses { get; set; }
    [XMap]
    public List<string> BccAddresses { get; set; }
    [XMap]
    public string FromAddress { get; set; }
    [XMap]
    public string Subject { get; set; }
    public string Body { get; set; }

    public SendEmailRequest()
    {
      this.ToAddresses = new List<string>();
      this.CcAddresses = new List<string>();
      this.BccAddresses = new List<string>();
      this.FromAddress = String.Empty;
      this.Subject = String.Empty;
      this.Body = String.Empty;
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
  }
}