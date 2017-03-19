using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Reflection;
using Org.GS;

namespace Org.GS.Configuration
{
  [ObfuscationAttribute(Exclude = true, ApplyToMembers = true)]
  [XMap(XType = XType.Element)]
  public class SmtpProfile
  {
    [XMap(IsKey = true)]
    public string Name { get; set; }

    [XMap]
    public string Host { get; set; }

    [XMap]
    public string Port { get; set; }

    [XMap]
    public string User { get; set; }

    [XMap]
    public string Password { get; set; }

    [XMap]
    public bool PickUpFromIIS { get; set; }

    [XMap]
    public string ReplyToAddress { get; set; }

    public SmtpProfile()
    {
      this.Name = String.Empty;
      this.Host = String.Empty;
      this.Port = String.Empty;
      this.User = String.Empty;
      this.Password = String.Empty;
      this.ReplyToAddress = String.Empty;
    }
  }
}
