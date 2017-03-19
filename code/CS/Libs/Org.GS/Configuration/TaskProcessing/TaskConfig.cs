using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Reflection;

namespace Org.GS.Configuration
{
  [ObfuscationAttribute(Exclude = true, ApplyToMembers = true)]
  [XMap(XType = XType.Element, Name="Task")]
  public class TaskConfig
  {
    [XMap(Name="Active", DefaultValue = "True", IsExplicit = true)]
    public bool IsActive { get; set; }

    [XMap(Name="Name", IsKey = true, IsRequired = true)]
    public string TaskName { get; set; }

    [XMap(DefaultValue = "False")]
    public bool IsLongRunning { get; set; }

    [XMap(Name = "TaskSchedule", CollectionElements = "TaskScheduleElement", WrapperElement = "TaskSchedule")]
    public TaskSchedule TaskSchedule { get; set; }

    [XMap(CollectionElements = "TaskParm", WrapperElement = "TaskParmSet")]
    public TaskParmSet TaskParmSet { get; set; }

    public bool IsConfiguredAsActive { get; set; }

    public TaskConfig()
    {
      this.IsActive = false;
      this.TaskName = String.Empty;
      this.IsConfiguredAsActive = false;
      this.IsLongRunning = false;
      this.TaskParmSet = new TaskParmSet();
      this.TaskSchedule = new TaskSchedule();      
    }
  }
}
