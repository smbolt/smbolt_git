using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS.Notifications
{
  public class NotificationMessageBase
  {
    public NotificationMessageType NotificationMessageType {
      get;
      set;
    }
    public List<NotificationAddressBase> NotificationAddresses {
      get;
      set;
    }
    public string NotificationMessageSubject {
      get;
      set;
    }
    public string NotificationMessageBody {
      get;
      set;
    }

    public NotificationMessageBase()
    {
      this.NotificationMessageType = Notifications.NotificationMessageType.NotSet;
      this.NotificationAddresses = new List<NotificationAddressBase>();
      this.NotificationMessageSubject = String.Empty;
      this.NotificationMessageBody = String.Empty;
    }
  }
}
