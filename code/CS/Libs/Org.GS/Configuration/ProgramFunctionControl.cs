using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Reflection;
using Org.GS;

namespace Org.GS.Configuration
{
  [ObfuscationAttribute(Exclude = true, ApplyToMembers = true)]
  [XMap(XType = XType.Element)]
  public class ProgramFunctionControl
  {
    [XMap(MyParent = true)]
    public ProgramConfig ProgramConfig {
      get;
      set;
    }

    [XMap]
    public string ConfigItemSource {
      get;
      set;
    }

    [XMap(XType = XType.Element, CollectionElements = "ProgramFunction", WrapperElement = "ProgramFunctionSet")]
    public ProgramFunctionSet ProgramFunctionSet {
      get;
      set;
    }


    [XMap(XType = XType.Element, CollectionElements = "ProgramRole", WrapperElement = "ProgramRoleSet")]
    public ProgramRoleSet ProgramRoleSet {
      get;
      set;
    }

    public ProgramFunctionControl()
    {
      ConfigItemSource = String.Empty;
      ProgramFunctionSet = new ProgramFunctionSet();
      ProgramRoleSet = new ProgramRoleSet();
    }

    private List<int> GetIntegerList(string delimitedIntegers)
    {
      List<int> integerList = new List<int>();
      char[] delim = new char[] { ',' };
      string[] ints = delimitedIntegers.Split(delim, StringSplitOptions.RemoveEmptyEntries);
      foreach (string s in ints)
      {
        if (s.IsNumeric())
          integerList.Add(Int32.Parse(s));
      }

      return integerList;
    }

    private string GetDelimitedIntegers(List<int> integerList)
    {
      string delimitedIntegers = String.Empty;

      foreach (int i in integerList)
      {
        if (delimitedIntegers.Length > 0)
          delimitedIntegers += "," + i.ToString().Trim();
        else
          delimitedIntegers += i.ToString().Trim();
      }

      return delimitedIntegers;
    }

    public List<string> GetProgramFunctionNamesForRoleName(string roleName)
    {
      var functionNames = new List<string>();

      var role = this.ProgramRoleSet.GetRoleByName(roleName);
      if (role == null)
        return functionNames;

      var functionNumbersAllowed = role.FunctionsAllowedList;
      foreach (int inheritedRole in role.InheritedRolesList)
      {
        if (this.ProgramRoleSet.ContainsKey(inheritedRole))
        {
          var programRole = this.ProgramRoleSet[inheritedRole];
          var inheritedFunctions = programRole.FunctionsAllowedList;
          foreach (int inheritedFunction in inheritedFunctions)
          {
            if (!functionNumbersAllowed.Contains(inheritedFunction))
              functionNumbersAllowed.Add(inheritedFunction);
          }
        }
      }

      functionNumbersAllowed.Sort();
      foreach (int functionNumber in functionNumbersAllowed)
      {
        if (this.ProgramFunctionSet.ContainsKey(functionNumber))
        {
          var programFunction = this.ProgramFunctionSet[functionNumber];
          if (!functionNames.Contains(programFunction.FunctionName))
            functionNames.Add(programFunction.FunctionName);
        }
      }

      return functionNames;
    }

    public bool AllowFunctionForUserGroups(string testFunction, List<string> groupsForUser)
    {
      if (testFunction == "PublicUse" || groupsForUser.Contains("UnlimitedUsers"))
        return true;

      foreach (var groupName in groupsForUser)
      {
        var allowedFunctions = this.GetProgramFunctionNamesForRoleName(groupName);
        if (allowedFunctions.Contains(testFunction))
          return true;
      }

      return false;
    }

    public bool IsValid()
    {
      foreach (var role in this.ProgramRoleSet.Values)
      {
        foreach (int functionNumber in role.FunctionsAllowedList)
        {
          if (!this.ProgramFunctionSet.ContainsKey(functionNumber))
            return false;
        }

        foreach (int roleNumber in role.InheritedRolesList)
        {
          if (!this.ProgramRoleSet.ContainsKey(roleNumber))
            return false;
        }
      }

      return true;
    }

    public string GetValidationReport()
    {
      StringBuilder sb = new StringBuilder();

      foreach (var role in this.ProgramRoleSet.Values)
      {
        foreach (int functionNumber in role.FunctionsAllowedList)
        {
          if (!this.ProgramFunctionSet.ContainsKey(functionNumber))
            sb.Append("Role number '" + role.RoleNumber.ToString() + "' role name '" + role.OrgRoleName + "' " +
                      "references function number '" + functionNumber.ToString() + "' which does not exist." + g.crlf);
        }

        foreach (int roleNumber in role.InheritedRolesList)
        {
          if (!this.ProgramRoleSet.ContainsKey(roleNumber))
            sb.Append("Role number '" + role.RoleNumber.ToString() + "' role name '" + role.OrgRoleName + "' " +
                      "references inherited role number '" + roleNumber.ToString() + "' which does not exist." + g.crlf);
        }
      }

      if (sb.Length == 0)
        return "Validation successful";

      return sb.ToString();
    }


  }
}
