using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Org.GS;

namespace Org.GS.Configuration
{
  [ObfuscationAttribute(Exclude = true, ApplyToMembers = true)]
  [XMap(XType = XType.Element, Name = "TaskScheduleElement")]
  public class TaskScheduleElement
  {
    [XMap(IsRequired = true, DefaultValue = "True")]
    public bool IsActive {
      get;
      set;
    }

    [XMap(Name = "ExecutionType", IsRequired = true)]
    public TaskExecutionType TaskExecutionType {
      get;
      set;
    }

    [XMap(DefaultValue = "0")]
    public decimal? FrequencySeconds {
      get;
      set;
    }

    [XMap(DefaultValue = "False")]
    public bool IsClockAligned {
      get;
      set;
    }

    [XMap(XType = XType.Element, Name = "TaskDateTime")]
    public TaskDateTime TaskDateTime {
      get;
      set;
    }

    [XMap(XType = XType.Element, Name = "TaskCalendar")]
    public TaskCalendar TaskCalendar {
      get;
      set;
    }

    [XMap(XType = XType.Element, Name = "TaskRunControl")]
    public TaskRunControl RunControl {
      get;
      set;
    }

    public TaskScheduleElement()
    {
      this.IsActive = true;
      this.TaskExecutionType = TaskExecutionType.NotSet;
      this.TaskDateTime = new TaskDateTime();
      this.TaskCalendar = new TaskCalendar();
      this.RunControl = new TaskRunControl();
    }
  }
}
