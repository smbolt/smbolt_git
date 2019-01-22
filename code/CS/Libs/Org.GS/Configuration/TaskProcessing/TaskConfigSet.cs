using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Reflection;

namespace Org.GS.Configuration
{
  [ObfuscationAttribute(Exclude = true, ApplyToMembers = true)]
  [XMap(XType = XType.Element, CollectionElements = "TaskConfig", SequenceDuplicates = true)]
  public class TaskConfigSet : Dictionary<string, TaskConfig>
  {
    private string _mode;
    [XMap(IsKey = true)]
    public string Mode
    {
      get {
        return _mode;
      }
      set {
        _mode = value;
      }
    }

    [XMap(XType = XType.Element)]
    public TaskHolidayCalendar TaskHolidayCalendar {
      get;
      set;
    }

    public TaskConfigSet()
    {
      _mode = String.Empty;
      this.TaskHolidayCalendar = new TaskHolidayCalendar();
    }
  }
}
