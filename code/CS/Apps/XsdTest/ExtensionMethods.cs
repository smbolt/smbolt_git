using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Reflection;

namespace Org.XsdTest
{
  public static class ExtensionMethods
  {
    public static string ToErrorReport(this XElement e, int indentChars, int maxWidth)
    {
      var sb = new StringBuilder();
      var sbLine = new StringBuilder();

      int level = 0;

      if (level == 0)
        sbLine.Append("<" + e.Name.LocalName);
      else
        sbLine.Append(g.BlankString(indentChars + 2 * level) + "<" + e.Name.LocalName);

      foreach (var attr in e.Attributes())
      {
        string attrFmt = attr.Name + "=\"" + attr.Value.Replace("\n", " ") + "\"";

        if (sbLine.Length + attrFmt.Length > maxWidth)
        {
          sb.Append(sbLine.ToString() + g.crlf);
          sbLine = new StringBuilder();
        }

        if (sbLine.Length == 0)
          sbLine.Append(g.BlankString(indentChars + (2 * level) + 6));
        else
          sbLine.Append(" "); 

        sbLine.Append(attrFmt);
      }

      
      if (sbLine.Length > 0)
        sb.Append(sbLine.ToString() + " >" + g.crlf);

      sb.Append(g.BlankString(indentChars + 2 * level) + "</" + e.Name.LocalName);

      string errorReport = sb.ToString();
      return errorReport;
    }



    [DebuggerStepThrough]
    public static string ToReport(this Exception value)
    {
      StringBuilder sb = new StringBuilder();

      Exception ex = value;
      bool moreExceptions = true;
      int level = 0;

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
    public static bool IsBlank(this object value)
    {
      if (value == null)
        return true;

      if (value.ToString().Trim().Length == 0)
        return true;

      return false;
    }

    [DebuggerStepThrough]
    public static bool IsNotBlank(this string value)
    {
      if (value == null)
        return false;

      if (value.Trim().Length == 0)
        return false;

      return true;
    }

    [DebuggerStepThrough]
    public static int ToInt32OrDefault(this string value, int defaultValue)
    {
      if (value == null)
        return defaultValue;

      if (value.Trim().Length == 0)
        return defaultValue;

      if (value.IsNotNumeric())
        return defaultValue;

      return Int32.Parse(value);
    }

    [DebuggerStepThrough]
    public static bool IsNumeric(this string value)
    {
      if (value == null)
        return false;

      if (value.Trim().Length == 0)
        return false;

      foreach (Char c in value)
        if (!Char.IsNumber(c))
          return false;

      return true;
    }

    [DebuggerStepThrough]
    public static bool IsNotNumeric(this string value)
    {
      if (value == null)
        return true;

      if (value.Trim().Length == 0)
        return true;

      foreach (Char c in value)
        if (!Char.IsNumber(c))
          return true;

      return false;
    }
  }
}
