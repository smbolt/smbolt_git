using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS.Notifications
{
  public class EmailNotificationMessage : NotificationMessageBase
  {
    public bool IsBodyHtml {
      get;
      set;
    }

    public EmailNotificationMessage()
    {
      this.IsBodyHtml = false;
    }
  }
}
