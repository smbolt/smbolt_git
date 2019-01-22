using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS.Notifications
{
  public enum NotificationMessageType
  {
    NotSet,
    EmailNotification,
    SmsNotification
  }

  public enum NotificationAddressType
  {
    NotSet,
    EmailToAddress,
    EmailCcAddress,
    EmailBccAddress,
    EmailFromAddress,
    SmsToNumber,
    SmsFromNumber
  }
}
