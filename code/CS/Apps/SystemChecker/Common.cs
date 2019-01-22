using System;
using System.Collections.Generic;
using System.Text;

namespace SystemChecker
{
  public enum TokenInformationClass
  {
    TokenUser = 1,
    TokenGroups,
    TokenPrivileges,
    TokenOwner,
    TokenPrimaryGroup,
    TokenDefaultDacl,
    TokenSource,
    TokenType,
    TokenImpersonationLevel,
    TokenStatistics,
    TokenRestrictedSids,
    TokenSessionId,
    TokenGroupsAndPrivileges,
    TokenSessionReference,
    TokenSandBoxInert,
    TokenAuditPolicy,
    TokenOrigin,
    TokenElevationType,
    TokenLinkedToken,
    TokenElevation,
    TokenHasRestrictions,
    TokenAccessInformation,
    TokenVirtualizationAllowed,
    TokenVirtualizationEnabled,
    TokenIntegrityLevel,
    TokenUiAccess,
    TokenMandatoryPolicy,
    TokenLogonSid,
    MaxTokenInfoClass
  }

  enum TokenElevationType
  {
    TokenElevationTypeDefault = 1,
    TokenElevationTypeFull,
    TokenElevationTypeLimited
  }

  public static class Common
  {
    public static string crlf = Environment.NewLine;
    public static string crlf2 = Environment.NewLine + Environment.NewLine;

    public static bool IsBlank(this string value)
    {
      if (value == null)
        return true;

      if (value.Trim().Length == 0)
        return true;

      return false;
    }

    public static bool IsNotBlank(this string value)
    {
      return !IsBlank(value);
    }

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

    public static bool IsNotNumeric(this string value)
    {
      return !IsNumeric(value);
    }

    public static int ToInt32(this string value)
    {
      if (value == null)
        return 0;

      if (value.Trim().Length == 0)
        return 0;

      value = value.Trim();
      int negationValue = 1;

      if (value.StartsWith("-"))
      {
        value = value.Substring(1);
        negationValue = -1;
      }
      else
      {
        if (value.EndsWith("-"))
        {
          value = value.Substring(0, value.Length - 1);
          negationValue = -1;
        }
      }

      if (value.IsNotNumeric())
        return 0;

      return Int32.Parse(value) * negationValue;
    }

    public static string ToReport(this Exception value)
    {
      StringBuilder sb = new StringBuilder();

      Exception ex = value;
      bool moreExceptions = true;
      int level = 0;

      while (moreExceptions)
      {
        sb.Append("Level:" + level.ToString() + " Type=" + ex.GetType().ToString() + Environment.NewLine +
                  "Message: " + ex.Message + Environment.NewLine +
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
  }
}
