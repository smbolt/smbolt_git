using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Org.TSK
{
  public enum PauseMethod
  {
    Drain,
    Hold,
    Clear
  }

  public enum StopMethod
  {
    Force,
    Drain
  }

  public enum ServiceState
  {
    Running,
    Paused,
    Stopped
  }

  public enum StartDateOptions
  {
    UseMinValue,
    UseStartDateTime,
    UseStartTimeOnly
  }

  public enum EndDateOptions
  {
    UseMaxValue,
    UseEndDateTime,
    UseEndTimeOnly
  }
}
