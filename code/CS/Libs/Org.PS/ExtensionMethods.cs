using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Org.PS
{
  public static class ExtensionMethods
  {
    public static string Report(this ServiceController s)
    {
      if (s == null)
        return "ServiceController is null";

      return s.ServiceName + "  Status: " + s.Status.ToString();
    }
  }
}
