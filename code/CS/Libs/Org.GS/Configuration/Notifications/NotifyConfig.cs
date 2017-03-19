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
  public class NotifyConfig
  {
    public AppConfig AppConfig { get; set; }
    public ProgramConfig ProgramConfig { get; set; }
    public NotifyConfigSet NotifyConfigSet { get; set; }

    public int NotifyConfigId { get; set; }

    [XMap(IsKey = true, IsRequired = true)]
    public string Name { get; set; }

    [XMap]
    public string SupportEmail { get; set; }

    [XMap]
    public string SupportPhone { get; set; }

    [XMap(DefaultValue="True")]
    public bool IsActive { get; set; }

    [XMap(DefaultValue="True")]
    public bool SendEmail { get; set; }

    [XMap(DefaultValue = "False")]
    public bool SendSms { get; set; }

    public string CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public string ModifiedBy { get; set; }
    public DateTime? ModifiedOn { get; set; }


    [XMap(XType = XType.Element, CollectionElements = "SmtpProfile")]
    public SmtpProfileSet SmtpProfileSet { get; set; }

    [XMap(XType = XType.Element, CollectionElements = "NotifyGroup", WrapperElement = "NotifyGroupSet")]
    public NotifyGroupSet NotifyGroupSet { get; set; }

    [XMap(XType = XType.Element, CollectionElements = "NotifyEvent", WrapperElement = "NotifyEventSet")]
    public NotifyEventSet NotifyEventSet { get; set; }

    public Dictionary<int, NotifyEventGroup> NotifyEventGroups;
    public List<int> NotifyEventGroupsIds { get { return Get_NotifyEventGroupIds(); } }

    public bool IsEmpty { get { return this.IsThisEmpty(); } }
    public int TotalActiveEmailAddresses { get { return Get_TotalActiveEmailAddresses(); } }
    public int TotalActiveSmsNumbers { get { return Get_TotalActiveSmsNumbers(); } }

    [XParm(Name = "parent", ParmSource = XParmSource.Parent)]
    public NotifyConfig(NotifyConfigSet parent)
    {
      this.NotifyConfigSet = parent;
      if (this.NotifyConfigSet != null)
        this.ProgramConfig = this.NotifyConfigSet.ProgramConfig;
      this.Name = String.Empty;
      this.SupportEmail = String.Empty;
      this.SupportPhone = String.Empty;
      this.IsActive = true;
      this.SendEmail = true;
      this.SendSms = false;
      this.CreatedBy = String.Empty;
      this.CreatedOn = DateTime.MinValue;
      this.ModifiedBy = null;
      this.ModifiedOn = null;
      this.SmtpProfileSet = new SmtpProfileSet();
      this.NotifyGroupSet = new NotifyGroupSet();
      this.NotifyEventSet = new NotifyEventSet();
      this.NotifyEventGroups = new Dictionary<int, NotifyEventGroup>();
    }

    public NotifyConfig()
    {
      this.Name = String.Empty;
      this.SupportEmail = String.Empty;
      this.SupportPhone = String.Empty;
      this.IsActive = true;
      this.SendEmail = true;
      this.SendSms = false;
      this.CreatedBy = String.Empty;
      this.CreatedOn = DateTime.MinValue;
      this.ModifiedBy = null;
      this.ModifiedOn = null;
      this.SmtpProfileSet = new SmtpProfileSet();
      this.NotifyGroupSet = new NotifyGroupSet();
      this.NotifyEventSet = new NotifyEventSet();
      this.NotifyEventGroups = new Dictionary<int, NotifyEventGroup>();
    }

    public void SetParent(NotifyConfigSet NotifyConfigSet)
    {
      this.NotifyConfigSet = NotifyConfigSet;
      if (this.NotifyConfigSet != null)
        this.ProgramConfig = this.NotifyConfigSet.ProgramConfig;
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

    private List<int> Get_NotifyEventGroupIds()
    {
      var groupIds = new List<int>();

      if (this.NotifyEventGroups == null)
        return groupIds;

      foreach (var notifyEventGroup in this.NotifyEventGroups.Values)
      {
        if (!groupIds.Contains(notifyEventGroup.NotifyGroupID))
          groupIds.Add(notifyEventGroup.NotifyGroupID);
      }

      return groupIds;
    }

    private int Get_TotalActiveEmailAddresses()
    {
      int activeEmailAddressCount = 0;
      foreach (var group in this.NotifyGroupSet.Values)
      {
        if (group.IsActive)
        {
          foreach (var person in group)
          {
            if (person.IsActive && person.IsEmailActive && person.EmailAddress.IsNotBlank())
              activeEmailAddressCount++;
          }
        }
      }
      return activeEmailAddressCount;
    }

    private int Get_TotalActiveSmsNumbers()
    {
      int activeSmsNumberCount = 0;
      foreach (var group in this.NotifyGroupSet.Values)
      {
        if (group.IsActive)
        {
          foreach (var person in group)
          {
            if (person.IsActive && person.IsSmsActive && person.SmsNumber.IsNotBlank())
              activeSmsNumberCount++;
          }
        }
      }
      return activeSmsNumberCount;
    }

    public string GetReport()
    {
      StringBuilder sb = new StringBuilder();
      sb.Append("Notify Config: " + this.Name + g.crlf +
                  "  Support Email: " + this.SupportEmail + g.crlf +
                  "  Support Phone: " + this.SupportPhone + g.crlf +
                  "  Is Active:     " + this.IsActive + g.crlf +
                  "  Send Email:    " + this.SendEmail + g.crlf +
                  "  Send SMS:      " + this.SendSms + g.crlf2);
      foreach (NotifyEvent notifyEvent in this.NotifyEventSet.Values)
      {
        sb.Append("  Notify Event: " + notifyEvent.Name + g.crlf +
                  "    Is Active: " + notifyEvent.IsActive + g.crlf +
                  "    Default Subject: " + notifyEvent.DefaultSubject + g.crlf2);
        foreach (NotifyGroupReference notifyEventGroup in notifyEvent)
        {
          NotifyGroup notifyGroup = this.NotifyGroupSet[notifyEventGroup.NotifyGroupName];
          sb.Append("    Notify Group: " + notifyGroup.Name + g.crlf +
                    "     Is Active: " + notifyGroup.IsActive + g.crlf);
          foreach (NotifyPerson notifyPerson in notifyGroup)
          {
            string emailActiveStr = notifyPerson.IsEmailActive ? "(Y)" : "(N)";
            string smsActiveStr = notifyPerson.IsSmsActive ? "(Y)" : "(N)";
            sb.Append("      Name: " + notifyPerson.Name + g.crlf +
                      "        Is Active: " + notifyPerson.IsActive + g.crlf +
                      "        " + emailActiveStr + "Email Address: " + notifyPerson.EmailAddress + g.crlf +
                      "        " + smsActiveStr + "SMS Number:    " + notifyPerson.SmsNumber + g.crlf);
          }
          sb.Append(g.crlf);
        }
        sb.Append(g.crlf);
      }
      return sb.ToString();
    }
  }
}
