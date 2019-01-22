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
  [XMap(XType = XType.Element, Name = "TaskDateTime")]
  public class TaskDateTime
  {
    [XMap(Format = "MM/dd/yyyy")]
    public DateTime? StartDate { get; set; }

    [XMap(Format = @"hh\:mm")]
    public TimeSpan? StartTime { get; set; }

    [XMap(Format = "MM/dd/yyyy")]
    public DateTime? EndDate { get; set; }

    [XMap(Format = @"hh\:mm")]
    public TimeSpan? EndTime { get; set; }

    [XMap]
    public IntervalType IntervalType { get; set; }

    public TaskDateTime()
    {
      this.StartDate = null;
      this.StartTime = null;
      this.EndDate = null;
      this.EndTime = null;
      this.IntervalType = IntervalType.NotSet;
    }
  }
}
