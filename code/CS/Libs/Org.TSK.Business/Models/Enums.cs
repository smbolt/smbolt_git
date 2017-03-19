using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.TSK.Business.Models
{
  public enum ProcessorType
  {
    TaskEngine = 0,
    NotSet = 99
  }

  public enum ExecutionStatus
  {
    NotStarted = 0,
    Initiated = 1,
    InProgress = 2,
    Completed = 3
  }

  public enum RunStatus
  {
    NotStarted = 0,
    Initiated = 1,
    Processing = 2,
    Success = 3,
    Warning = 4,
    Failed = 5,
    Canceled = 6
  }

  public enum Period
  {
    NotSet = 0,
    Day = 1,
    Week = 2,
    Month = 3,
    Quarter = 4,
    Year = 5
  }
}
