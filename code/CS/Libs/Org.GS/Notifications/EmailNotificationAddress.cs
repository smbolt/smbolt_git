using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS.Notifications
{
  public class EmailNotificationAddress : NotificationAddressBase
  {
    public EmailNotificationAddress()
    {
    }

    public EmailNotificationAddress(NotificationAddressType notificationAddressType)
      : base(notificationAddressType) { }

    public EmailNotificationAddress(NotificationAddressType notificationAddressType, string notificationAddress)
      : base(notificationAddressType, notificationAddress) { }
  }
}
