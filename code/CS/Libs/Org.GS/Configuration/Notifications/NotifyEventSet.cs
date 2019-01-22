using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Reflection;
using Org.GS;
using Org.GS.Notifications;

namespace Org.GS.Configuration
{
  [ObfuscationAttribute(Exclude = true, ApplyToMembers = true)]
  [XMap(XType = XType.Element, CollectionElements = "NotifyEvent")]
  public class NotifyEventSet : Dictionary<string, NotifyEvent>
  {
    public List<int> EventIds {
      get {
        return Get_EventIds();
      }
    }

    public NotifyEvent GetNotifyEventForNotification(Notification notification)
    {
      string eventName = notification.EventName.Replace("$TASKNAME$", notification.TaskName);

      if (this.ContainsKey(eventName))
        return this[eventName];

      eventName = notification.EventName;

      if (this.ContainsKey(eventName))
        return this[eventName];

      return null;
    }


    public NotifyEvent GetNotifyEventForTaskResult(TaskResult taskResult)
    {
      string eventName = taskResult.NotificationEventName;
      if (this.ContainsKey(eventName))
      {
        var notifyEvent = this[eventName];
        if (notifyEvent.IsActive)
          return notifyEvent;
      }

      eventName = "*_" + taskResult.TaskResultStatus.ToString();
      if (this.ContainsKey(eventName))
      {
        var notifyEvent = this[eventName];
        if (notifyEvent.IsActive)
          return notifyEvent;
      }

      return null;
    }

    public NotifyEvent GetNotifyEvent(string eventName)
    {
      if (this.ContainsKey(eventName))
      {
        var notifyEvent = this[eventName];
        if (notifyEvent.IsActive)
          return notifyEvent;
      }

      return null;
    }

    private List<int> Get_EventIds()
    {
      var eventIds = new List<int>();

      foreach (var notifyEvent in this.Values)
      {
        if (!eventIds.Contains(notifyEvent.NotifyEventId))
          eventIds.Add(notifyEvent.NotifyEventId);
      }

      return eventIds;
    }
  }
}
