using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Org.GS;

namespace Org.GS.Math
{
  public static class MathExtensionMethods
  {
    //[DebuggerStepThrough]
    public static bool IsMathOperator(this string value)
    {
      if (value == null)
        return false;

      value = value.Trim();

      if (value == "+" || value == "-" || value == "*" || value == @"/" || value == "^")
        return true;

      return false;
    }

    //[DebuggerStepThrough]
    public static bool ContainsMathOperator(this string value)
    {
      if (value == null)
        return false;

      if (value.Contains("+") || value.Contains("-") || value.Contains("*") || value.Contains(@"/") || value.Contains("^"))
        return true;

      return false;
    }

    //[DebuggerStepThrough]
    public static bool ContainsHighLevelMathOperator(this string value)
    {
      if (value == null)
        return false;

      if (value.Contains("*") || value.Contains(@"/") || value.Contains("^"))
        return true;

      return false;
    }

    //[DebuggerStepThrough]
    public static bool ContainsLowLevelMathOperator(this string value)
    {
      if (value == null)
        return false;

      if (value.Contains("+") || value.Contains("-"))
        return true;

      return false;
    }

    //[DebuggerStepThrough]
    public static bool IsHighOrderMathOperator(this string value)
    {
      if (value == null)
        return false;

      value = value.Trim();

      if (value == @"/" || value == "^")
        return true;

      return false;
    }

    //[DebuggerStepThrough]
    public static bool IsLowOrderMathOperator(this string value)
    {
      if (value == null)
        return false;

      value = value.Trim();

      if (value == "+" || value == "-")
        return true;

      return false;
    }

    //[DebuggerStepThrough]
    public static bool IsMathOperator(this char value)
    {
      if (value == null)
        return false;

      if (value == '+' || value == '-' || value == '*' || value == '/' || value == '^')
        return true;

      return false;
    }

    //[DebuggerStepThrough]
    public static bool IsHighOrderMathOperator(this char value)
    {
      if (value == null)
        return false;

      if (value == '*' || value == '/' || value == '^')
        return true;

      return false;
    }

    //[DebuggerStepThrough]
    public static bool IsLowOrderMathOperator(this char value)
    {
      if (value == null)
        return false;

      if (value == '+' || value == '-')
        return true;

      return false;
    }

    //[DebuggerStepThrough]
    public static bool IsLegalVariableName(this string value)
    {
      if (value.Trim().Length == 0)
        return false;

      if (!value.IsAlphaNumeric())
        return false;

      if (value.Substring(0, 1).IsNumeric())
        return false;

      return true;
    }
  }
}
