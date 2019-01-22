using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.TOCR
{
  public static class ExtensionMethods
  {
    public static string ToCheckText(this string value)
    {
      if (value == null)
        return String.Empty;

      if (value.IsBlank())
        return String.Empty;
      
      string alphaTokens = value.ToUpper().ToAlphaTokens(2);
      int payToken = alphaTokens.IndexOf(" PAY ", 10);
      if (payToken > -1)
        alphaTokens = alphaTokens.Substring(0, payToken).Trim();

      int dollarsToken = alphaTokens.IndexOf(" DOLLAR");
      if (dollarsToken > -1)
        alphaTokens = alphaTokens.Substring(0, dollarsToken);

      alphaTokens = alphaTokens.Replace(" DATE ", " ");
      alphaTokens = alphaTokens.Replace(" THE ", " ");
      if (alphaTokens.StartsWith("THE "))
        alphaTokens = alphaTokens.Substring(3).Trim();

      if (alphaTokens.EndsWith(" DATE"))
        alphaTokens = alphaTokens.Substring(0, alphaTokens.Length - 5).Trim();

      alphaTokens = alphaTokens.Replace("DOLLARS", " ");

      return alphaTokens;
    }
  }
}
