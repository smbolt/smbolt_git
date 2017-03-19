using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Org.GS;
using Org.GS.Notifications;

namespace Org.GS.Configuration
{
  [ObfuscationAttribute(Exclude = true, ApplyToMembers = true)]
  [XMap(XType = XType.Element, CollectionElements = "NotifyConfig", SequenceDuplicates = true)]
  public class NotifyConfigSet : Dictionary<string, NotifyConfig>
  {
    public ProgramConfig ProgramConfig { get; set; }

    public int NotifyConfigSetId { get; set; }

    [XMap]
    public string Name { get; set; }

    public bool IsActive { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public string ModifiedBy { get; set; }
    public DateTime? ModifiedOn { get; set; }

    public NotifyConfigSet()
    {
      Initialize();
    }

    public NotifyConfigSet(ProgramConfig programConfig)
    {
      this.ProgramConfig = programConfig;
      Initialize();
    }

    private void Initialize()
    {
      this.NotifyConfigSetId = 0;
      this.Name = null;
      this.IsActive = true;
      this.CreatedBy = String.Empty;
      this.CreatedOn = DateTime.MinValue;
      this.ModifiedBy = null;
      this.ModifiedOn = null;
    }

    public NotifyConfig GetConfigForTaskResult(TaskResult taskResult)
    {
      foreach (var notifyConfig in this.Values)
      {
        if (notifyConfig.IsActive)
        {
          foreach (var notifyEvent in notifyConfig.NotifyEventSet.Values)
          {
            if (notifyEvent.IsActive)
            {
              if (notifyEvent.Name == taskResult.NotificationEventName)
                return notifyConfig;
            }
          }
        }
      }

      foreach (var notifyConfig in this.Values)
      {
        if (notifyConfig.IsActive)
        {
          foreach (var notifyEvent in notifyConfig.NotifyEventSet.Values)
          {
            if (notifyEvent.IsActive)
            {
              if (notifyEvent.Name == taskResult.VariableNotificationEventName)
                return notifyConfig;
            }
          }
        }
      }

      return null;
    }

    public NotifyConfig GetConfigForNamedEvent(string eventName)
    {
      foreach (var notifyConfig in this.Values)
      {
        if (notifyConfig.IsActive)
        {
          foreach (var notifyEvent in notifyConfig.NotifyEventSet.Values)
          {
            if (notifyEvent.IsActive)
            {
              if (notifyEvent.Name == eventName)
                return notifyConfig;
            }
          }
        }
      }

      return null;
    }

    public bool ContainsConfigForTaskResult(TaskResult taskResult)
    {
      foreach (var notifyConfig in this.Values)
      {
        if (notifyConfig.IsActive)
        {
          foreach (var notifyEvent in notifyConfig.NotifyEventSet.Values)
          {
            if (notifyEvent.IsActive)
            {
              if (notifyEvent.Name == taskResult.NotificationEventName || notifyEvent.Name == taskResult.VariableNotificationEventName)
                return true;
            }
          }
        }
      }

      return false;
    }

    public bool ContainsConfigForNamedEvent(string namedEvent)
    {
      foreach (var notifyConfig in this.Values)
      {
        if (notifyConfig.IsActive)
        {
          foreach (var notifyEvent in notifyConfig.NotifyEventSet.Values)
          {
            if (notifyEvent.IsActive)
            {              
              if (notifyEvent.Name == namedEvent)
                return true;
            }
          }
        }
      }

      return false;
    }

    public NotifyEvent GetNotifyEventForTaskResult(TaskResult taskResult)
    {
      // get specifically named NotifyEvent
      foreach (var notifyConfig in this.Values)
      {
        if (notifyConfig.IsActive)
        {
          foreach (var notifyEvent in notifyConfig.NotifyEventSet.Values)
          {
            if (notifyEvent.IsActive)
            {
              if (notifyEvent.Name == taskResult.NotificationEventName)
              {
                return notifyEvent;
              }
            }
          }
        }
      }

      // get variable named ($TASKNAME$) NotifyEvent
      foreach (var notifyConfig in this.Values)
      {
        if (notifyConfig.IsActive)
        {
          foreach (var notifyEvent in notifyConfig.NotifyEventSet.Values)
          {
            if (notifyEvent.IsActive)
            {
              if (notifyEvent.Name == taskResult.VariableNotificationEventName)
              {
                return notifyEvent;
              }
            }
          }
        }
      }

      return null;
    }

    public NotifyEvent GetNotifyEventForNamedEvent(string eventName)
    {
      foreach (var notifyConfig in this.Values)
      {
        if (notifyConfig.IsActive)
        {
          foreach (var notifyEvent in notifyConfig.NotifyEventSet.Values)
          {
            if (notifyEvent.IsActive)
            {
              if (notifyEvent.Name == eventName)
              {
                return notifyEvent;
              }
            }
          }
        }
      }

      return null;
    }

    public string GetNotifyConfigsReport()
    {
      StringBuilder sb = new StringBuilder();

      foreach (var notifyConfig in this.Values)
      {
        sb.Append(notifyConfig.GetReport());
      }

      return sb.ToString();
    }
  }
}
