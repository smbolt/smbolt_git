using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Reflection;

namespace Org.GS.Configuration
{
  [ObfuscationAttribute(Exclude = true, ApplyToMembers = true)]
  [XMap(XType = XType.Element, Name="TaskConfig")]
  public class TaskConfig
  {
    [XMap(Name="IsActive", DefaultValue = "True", IsExplicit = true)]
    public bool IsActive { get; set; }

    [XMap(Name="Name", IsKey = true, IsRequired = true)]
    public string TaskName { get; set; }

    [XMap(IsRequired = true)]
    public string ProcessorName { get; set; }

    [XMap(IsRequired = true)]
    public string ProcessorVersion { get; set; }

    [XMap(DefaultValue = "0")]
    public int ProcessorTypeId { get; set; }

    [XMap]
    public string AssemblyName { get; set; }

    [XMap]
    public string CatalogName { get; set; }

    [XMap]
    public string CatalogEntry { get; set; }

    [XMap]
    public string ObjectTypeName { get; set; }

    [XMap(DefaultValue = "False")]
    public bool IsLongRunning { get; set; }

    [XMap(DefaultValue = "False")]
    public bool RunUnitTask { get; set; }

    [XMap(DefaultValue = "False")]
    public bool TrackHistory { get; set; }

    [XMap(DefaultValue = "True")]
    public bool AllowConcurrent { get; set; }

    [XMap(Name = "TaskSchedule", CollectionElements = "TaskScheduleElement", WrapperElement = "TaskSchedule")]
    public TaskSchedule TaskSchedule { get; set; }

    [XMap(CollectionElements = "TaskParm", WrapperElement = "TaskParmSet")]
    public TaskParmSet TaskParmSet { get; set; }

    public bool IsConfiguredAsActive { get; set; }

    public TaskConfig()
    {
      this.IsActive = false;
      this.TaskName = String.Empty;
      this.ProcessorName = String.Empty;
      this.ProcessorVersion = String.Empty;
      this.ProcessorTypeId = 0;
      this.AssemblyName = String.Empty;
      this.CatalogName = String.Empty;
      this.CatalogEntry = String.Empty;
      this.ObjectTypeName = String.Empty;
      this.IsConfiguredAsActive = true;
      this.IsLongRunning = false;
      this.RunUnitTask = false;
      this.TrackHistory = false;
      this.TaskParmSet = new TaskParmSet();
      this.TaskSchedule = new TaskSchedule();      
    }
  }
}
