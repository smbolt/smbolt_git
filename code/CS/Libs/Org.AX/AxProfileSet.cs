using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Org.GS;

namespace Org.AX
{
  [ObfuscationAttribute(Exclude = true, ApplyToMembers = true)]
  [XMap(XType = XType.Element, CollectionElements = "AxProfile")]
  public class AxProfileSet : Dictionary<string, AxProfile>
  {
    [XMap(XType = XType.Element, WrapperElement = "VariableSet", CollectionElements = "Variable", UseKeyValue = true)]
    public VariableSet VariableSet { get; set; }

    public AxProfileSet()
    {
      this.VariableSet = new VariableSet();
    }

    public void ResolveVariables()
    {
      foreach (var axProfile in this.Values)
      {
        axProfile.RunDateTime = DateTime.Now;
        string yyyymmddhhss = axProfile.RunDateTime.ToString("yyyyMMdd-HHmmss");

        foreach (var axion in axProfile.Values)
        {
          axion.Src = ReplaceVariables(axion.Src, axProfile);
          axion.Tgt = ReplaceVariables(axion.Tgt, axProfile);
        }
      }
    }

    private string ReplaceVariables(string value, AxProfile profile)
    {
      string originalValue = value;

      if (value.IndexOf("$") < 0)
        return value;

      int variableCount = 0;

      do
      {
        int begPos = value.IndexOf("$");
        int endPos = value.IndexOf("$", begPos + 1);
        if (endPos == -1)
          throw new Exception("The configuration value '" + value + "' contains an unpaired dollar sign ($) - variable name cannot be determined.");

        int length = endPos - begPos - 1;
        if (length < 1)
          throw new Exception("The configuration value '" + value + "' contains a zero-length varaible name.");

        string variableName = value.Substring(begPos + 1, length);

        VariableSet variableSet = null;

        if (profile.VariableSet.ContainsKey(variableName))
          variableSet = profile.VariableSet;

        if (variableSet == null && this.VariableSet.ContainsKey(variableName))
          variableSet = this.VariableSet;

        if (variableSet == null)
          value = value.Replace("$" + variableName + "$", "!MISSING-VARIABLE:" + variableName + "!");
        else
          value = value.Replace("$" + variableName + "$", variableSet[variableName]);

        variableCount++;
        if (variableCount > 10)
          throw new Exception("Greater than 10 variables found in configuration value '" + originalValue + "'.");

      } while (value.IndexOf("$") > -1);

      return value;
    }
  }
}
