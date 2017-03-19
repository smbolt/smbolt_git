using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Org.GS
{
  public delegate void LoggingEventHandler(string LogMessage);
  public delegate void LoggingExceptionEventHandler(Exception ex);
  public delegate void IpdxEventHandler(IpdxMessage message);
}
