using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS.Configuration
{
  public enum TaskExecutionType
  {
    NotSet = 0,
    RunOnFrequency = 1,
    RunImmediate = 2,
    RunImmediateAndOnFrequency = 3,
    RunOnceAt = 4,
    RunAtAndOnFrequency = 5
  }

  public enum IntervalType
  {
    NotSet = 0,
    DailyInterval = 1,
    SingleSpan = 2
  }

  public enum HolidayActions
  {
    NotSet = 0,
    RunOnHoliday = 1,
    SkipOnHoliday = 2,
    SlideForward = 3,
    SlideBackward = 4
  }

  public enum PeriodContexts
  {
    NotSet = 0,
    Day = 1,
    Week = 2,
    Month = 3,
    Quarter = 4,
    Year = 5
  }
}
