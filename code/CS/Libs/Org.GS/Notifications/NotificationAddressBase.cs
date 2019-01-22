using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS.Notifications
{
  public class NotificationAddressBase
  {
    public NotificationAddressType NotificationAddressType {
      get;
      set;
    }
    public string NotificationAddress {
      get;
      set;
    }

    public NotificationAddressBase()
    {
      this.NotificationAddressType = NotificationAddressType.NotSet;
      this.NotificationAddress = String.Empty;
    }

    public NotificationAddressBase(NotificationAddressType notificationAddressType)
    {
      this.NotificationAddressType = notificationAddressType;
    }

    public NotificationAddressBase(NotificationAddressType notificationAddressType, string notificationAddress)
    {
      this.NotificationAddressType = notificationAddressType;
      this.NotificationAddress = notificationAddress;
    }
  }
}
