using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Org.DynamoDbService;

namespace DynamoDbServiceHost
{
  public static class ExtensionMethods
  {
    public static string ActionTag(this object o)
    {
      if (o == null)
        return String.Empty;

      var pi = o.GetType().GetProperty("Tag");

      if (pi == null)
        return String.Empty;

      var tagValue = pi.GetValue(o);

      if (tagValue == null)
        return String.Empty;

      return tagValue.ToString();
    }

    [DebuggerStepThrough]
    public static string ToReport(this Exception value)
    {
      StringBuilder sb = new StringBuilder();

      Exception ex = value;
      bool moreExceptions = true;
      int level = 0;


      var messageList = new List<string>();
      Exception ex2 = ex;
      messageList.Add(ex2.Message);
      while (ex2.InnerException != null)
      {
        ex2 = ex2.InnerException;
        messageList.Add(ex2.Message);
      }

      sb.Append("Exception Message Summary:" + g.crlf);
      for (int i = messageList.Count - 1; i > -1; i--)
      {
        sb.Append("[" + i.ToString("00") + "] " + messageList.ElementAt(i) + g.crlf);
      }

      sb.Append(g.crlf);

      string additionalInfo = String.Empty;

      string type = ex.GetType().Name;

      switch (type)
      {
        case "ReflectionTypeLoadException":
          var rlx = ex as ReflectionTypeLoadException;
          foreach (var lx in rlx.LoaderExceptions)
          {
            if (additionalInfo.IsBlank())
              additionalInfo = "*** TYPED EXCEPTION INFORMATION ***" + g.crlf + "From ReflectionTypeLoadException:" + g.crlf + lx.ToReport();
            else
              additionalInfo += g.crlf + lx.ToReport();
          }
          break;
      }

      if (additionalInfo.IsNotBlank())
        additionalInfo += "***" + g.crlf;

      while (moreExceptions)
      {
        if (ex.Message.StartsWith("!"))
          return ex.Message.Substring(1);

        sb.Append("Level:" + level.ToString() + " Type=" + ex.GetType().ToString() + Environment.NewLine +
                  "Message: " + ex.Message + Environment.NewLine + additionalInfo +
                  "StackTrace:" + ex.StackTrace + Environment.NewLine);

        if (ex.InnerException == null)
          moreExceptions = false;
        else
        {
          sb.Append(Environment.NewLine);
          ex = ex.InnerException;
          level++;
        }
      }

      string report = sb.ToString();
      return report;
    }

    [DebuggerStepThrough]
    public static bool IsBlank(this string s)
    {
      if (s == null)
        return true;

      if (s.Trim().Length == 0)
        return true;

      return false;
    }

    [DebuggerStepThrough]
    public static bool IsNotBlank(this string s)
    {
      return !s.IsBlank();
    }
  }
}
