using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS.Notifications
{
  public class SmsNotificationAddress : NotificationAddressBase
  {    
    public SmsNotificationAddress()
    {
    }

    public SmsNotificationAddress(NotificationAddressType notificationAddressType)
      : base(notificationAddressType) { }

    public SmsNotificationAddress(NotificationAddressType notificationAddressType, string notificationAddress)
      : base(notificationAddressType, notificationAddress) { }
  }
}
