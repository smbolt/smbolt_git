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
  [XMap(XType = XType.Element, Name = "TaskCalendar")]
  public class TaskCalendar
  {
    [XMap(IsRequired = true)]
    public string WeekdayControl { get; set; }

    [XMap(IsRequired = true)]
    public string OrdinalControl { get; set; }

    [XMap(IsRequired = true)]
    public string SpecificDays { get; set; }

    [XMap(IsRequired = true, Name="PeriodContext")]
    public PeriodContexts PeriodContexts { get; set; }

    [XMap(IsRequired = true, Name = "HolidayAction")]
    public HolidayActions HolidayActions { get; set; }

    public TaskCalendar()
    {
      this.WeekdayControl = "FFFFFFF";
      this.OrdinalControl = "FFFFFFFFFFF";
      this.SpecificDays = String.Empty;
      this.PeriodContexts = PeriodContexts.NotSet;
      this.HolidayActions = HolidayActions.RunOnHoliday;
    }   
  }
}
