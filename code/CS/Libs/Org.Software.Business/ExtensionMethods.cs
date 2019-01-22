using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.Software.Business
{
  public static class ExtensionMethods
  {
    public static string ToExpandedVersionString(this string value)
    {
      if (value == null)
        throw new Exception("Cannot build expanded version string from a null value.");

      List<string> tokens = value.Split(Constants.PeriodDelimiter).ToList();
      if (tokens.Count != 4)
        throw new Exception("Invalid version string '" + value + "' - must be in format '9.9.9.9'.");

      string expandedVersionString = String.Empty;

      foreach (var token in tokens)
      {
        if (token.IsNotNumeric())
          throw new Exception("Invalid version string '" + value + "' - non-numeric value found - must be in format '9.9.9.9'.");
        if (expandedVersionString.IsBlank())
          expandedVersionString += token.ToInt32().ToString("000000");
        else
          expandedVersionString += "." + token.ToInt32().ToString("000000");
      }

      return expandedVersionString;
    }
  }
}
