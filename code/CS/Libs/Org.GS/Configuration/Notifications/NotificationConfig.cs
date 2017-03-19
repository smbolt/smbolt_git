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
  public class NotificationConfig
  {
    public AppConfig AppConfig { get; set; }
    public ProgramConfig ProgramConfig { get; set; }
    public NotificationConfigSet NotificationConfigSet { get; set; }

    [XMap(IsKey = true, IsRequired = true)]
    public string Name { get; set; }

    [XMap]
    public string SupportEmail { get; set; }

    [XMap]
    public string SupportPhone { get; set; }

    [XMap(DefaultValue="True")]
    public bool IsActive { get; set; }

    [XMap(XType = XType.Element, CollectionElements = "SmtpProfile")]
    public SmtpProfileSet SmtpProfileSet { get; set; }

    [XMap(XType = XType.Element, CollectionElements = "NotifyGroup", WrapperElement = "NotifyGroupSet")]
    public NotifyGroupSet NotifyGroupSet { get; set; }

    [XMap(XType = XType.Element, CollectionElements = "NotifyEvent", WrapperElement = "NotifyEventSet")]
    public NotifyEventSet NotifyEventSet { get; set; }

    public bool IsEmpty
    {
      get { return this.IsThisEmpty(); }
    }

    [XParm(Name = "parent", ParmSource = XParmSource.Parent)]
    public NotificationConfig(NotificationConfigSet parent)
    {
      this.NotificationConfigSet = parent;
      this.ProgramConfig = this.NotificationConfigSet.ProgramConfig;
      this.Name = String.Empty;
      this.SupportEmail = String.Empty;
      this.SupportPhone = String.Empty;
      this.IsActive = true;
      this.SmtpProfileSet = new SmtpProfileSet();
      this.NotifyGroupSet = new NotifyGroupSet();
      this.NotifyEventSet = new NotifyEventSet();
    }

    public bool IsThisEmpty()
    {
      if (this.SupportEmail.IsNotBlank())
        return false;

      if (this.SupportPhone.IsNotBlank())
        return false;

      if (this.SmtpProfileSet.Count > 0)
        return false;

      if (this.NotifyGroupSet.Count > 0)
        return false;

      if (this.NotifyEventSet.Count > 0)
        return false;

      return true;            
    }
  }
}
