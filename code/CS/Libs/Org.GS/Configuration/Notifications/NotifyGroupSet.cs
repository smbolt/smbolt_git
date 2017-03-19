using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Reflection;

namespace Org.GS.Configuration
{
  [ObfuscationAttribute(Exclude = true, ApplyToMembers = true)]
  [XMap(XType = XType.Element, CollectionElements = "NotifyGroup")]
  public class NotifyGroupSet : Dictionary<string, NotifyGroup>
  {
    public List<string> GetAllEmailAddresses(List<string> notifyGroups)
    {
      List<string> emailAddresses = new List<string>();

      foreach (string group in notifyGroups)
      {
        if (this.ContainsKey(group))
        {
          foreach (NotifyPerson person in this[group])
          {
            if (person.IsEmailActive && person.EmailAddress.Trim().Length > 0)
            {
                if (!emailAddresses.Contains(person.EmailAddress.Trim()))
                    emailAddresses.Add(person.EmailAddress.Trim());
            }
          }
        }
      }

      return emailAddresses;
    }

    public List<string> GetAllSmsNumbers(List<string> notifyGroups)
    {
      List<string> smsNumbers = new List<string>();

      foreach (string group in notifyGroups)
      {
        if (this.ContainsKey(group))
        {
          foreach (NotifyPerson person in this[group])
          {
            if (person.IsSmsActive && person.SmsNumber.Trim().Length > 0)
            {
                if (!smsNumbers.Contains(person.SmsNumber.Trim()))
                  smsNumbers.Add(person.SmsNumber.Trim());
            }
          }
        }
      }

      return smsNumbers;
    }
  }
}
