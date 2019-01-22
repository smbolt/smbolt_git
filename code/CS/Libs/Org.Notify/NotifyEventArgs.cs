using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.Notify
{
  public enum NotifyEventType
  {
    NotificationRequested,
    NotificationNotExecuted,
    NotificationCompleted,
    NotificationFailed,
    AllNotificationsFinished
  }

  public class NotifyEventArgs
  {
    public NotifyEventType NotifyEventType { get; private set; }
    public bool IsSynchronous { get; private set; }

    public NotifyEventArgs(NotifyEventType notifyEventType, bool isSynchronous)
    {
      this.NotifyEventType = notifyEventType;
      this.IsSynchronous = isSynchronous;
    }
  }
}
