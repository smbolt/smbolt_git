using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS
{
  [Serializable]
  public class NotifyMessage
  {
    public string Message { get; set; }
    public string Subject { get; set; }
    public string EventName { get; set; }

    public NotifyMessage(string message, string subject, string eventName)
    {
      this.Message = message;
      this.Subject = subject;
      this.EventName = eventName;
    }
  }
}
