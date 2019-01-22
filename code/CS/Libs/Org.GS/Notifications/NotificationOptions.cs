using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS.Notifications
{
  public class NotificationOptions
  {
    public bool ThrowExceptions {
      get;
      set;
    }
    public bool WaitForResults {
      get;
      set;
    }
    public string Subject {
      get;
      set;
    }
    public string Message {
      get;
      set;
    }
    public int Code {
      get;
      set;
    }

    public NotificationOptions()
    {
      this.ThrowExceptions = false;
      this.WaitForResults = false;
      this.Subject = "No subject supplied";
      this.Message = "Empty message";
      this.Code = -1;
    }
  }
}
