using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Org.GS;

namespace Org.GS.Code
{
  [ObfuscationAttribute(Exclude = true, ApplyToMembers = true)]
  [XMap(XType = XType.Element, CollectionElements="Profile")] 
  public class ProfileSet : Dictionary<string, Profile>
  {
    [XMap(XType = XType.Element, WrapperElement = "VariableSet", CollectionElements = "Variable", UseKeyValue = true)] 
    public VariableSet VariableSet{ get; set; }

    public ProfileSet()
    {
      this.VariableSet = new VariableSet();
    }

    public void AutoInit()
    {
      this.ResolveVariables();
    }

    public void ResolveVariables()
    {
      foreach (var profile in this.Values)
      {
        profile.RunDateTime = DateTime.Now;
        string yyyymmddhhss = profile.RunDateTime.ToString("yyyyMMdd-HHmmss");

        foreach (var mc in profile.MappingControlSet.Values)
        {
          mc.Source = ReplaceVariables(mc.Source);
          mc.Destination = ReplaceVariables(mc.Destination);
          mc.Destination = mc.Destination.Replace("#YYYYMMDD-HHMMSS#", yyyymmddhhss);
        }

        foreach(var oc in profile.OpsControlSet.Values)
        {
          oc.Host = ReplaceVariables(oc.Host);
          oc.ServiceName = ReplaceVariables(oc.ServiceName);
        }
      }
    }

    private string ReplaceVariables(string value)
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

        foreach(var profile in this.Values)
        {
          if(profile.VariableSet.ContainsKey(variableName))
            value = value.Replace("$" + variableName + "$", profile.VariableSet[variableName]);

          else

          if (!this.VariableSet.ContainsKey(variableName))
            value = value.Replace("$" + variableName + "$", this.VariableSet[variableName]);

          else
            return value;
        }

        variableCount++;
        if (variableCount > 10)
          throw new Exception("Greater than 10 variables found in configuration value '" + originalValue + "'.");

      } while (value.IndexOf("$") > -1);

      return value;
    }
  }
}
