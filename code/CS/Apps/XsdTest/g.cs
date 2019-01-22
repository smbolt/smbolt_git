using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Org.XsdTest
{
  public class g
  {
    public static string crlf = Environment.NewLine;
    public static string crlf2 = Environment.NewLine + Environment.NewLine;

    [DebuggerStepThrough]
    public static string GetActionFromEvent(object sender)
    {
      var pi = sender.GetType().GetProperty("Tag");
      if (pi == null)
        return String.Empty;

      object value = pi.GetValue(sender);
      if (value == null)
        return String.Empty;

      return value.ToString().Trim();
    }

    [DebuggerStepThrough]
    public static string BlankString(int length)
    {
      StringBuilder sb = new StringBuilder();
      for (int i = 0; i < length; i++)
        sb.Append(" ");

      return sb.ToString();
    }
  }
}
