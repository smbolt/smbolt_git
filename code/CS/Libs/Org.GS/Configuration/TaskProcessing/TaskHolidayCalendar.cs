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
  [XMap(XType = XType.Element, Name = "TaskHolidayCalendar")]
  public class TaskHolidayCalendar
  {
    [XMap(DefaultValue = "")]
    public string Values { get; set; }

    public TaskHolidayCalendar()
    {
      this.Values = String.Empty;
    }
  }
}
