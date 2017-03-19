using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS.Logging
{
  public enum LogSeverity
  {
    DIAG = 1,
    AUDT = 2,
    TRAC = 3,
    INFO = 4,
    WARN = 10,
    MINR = 11,
    MAJR = 20,
    SEVR = 30
  }

  public enum LogMethod
  {
    NotSet,
    None,
    LocalFile,
    Database,
    NoLogging
  }

  public enum LogFileFrequency
  {
    NotSet,
    NotApplicable,
    Continuous,
    Hourly,
    Daily
  }

  public enum LogFileSizeManagementMethod
  {
    NotSet,
    TotalBytes,
    Aging,
    NotManagedByThisApp
  }

  public enum LogFileSizeManagementAgent
  {
    NotSet,
    TaskEngine,
    Logger,
    None
  }

  public enum LogContextState
  {
    NotSet,
    Initial,
    Normal
  }
}
