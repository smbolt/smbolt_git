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
  [XMap(XType = XType.Element, CollectionElements = "AxAction")]
  public class AxProfile : Dictionary<string, AxAction>
  {
    [XMap(XType = XType.Element, WrapperElement = "VariableSet", CollectionElements = "Variable", UseKeyValue = true)]
    public VariableSet VariableSet {
      get;
      set;
    }

    [XMap(IsKey = true)]
    public string Name {
      get;
      set;
    }
    public string NameLower {
      get {
        return (this.Name.IsNotBlank() ? this.Name.ToLower() : String.Empty);
      }
    }

    [XMap(DefaultValue = "Active")]
    public ProfileStatus ProfileStatus {
      get;
      set;
    }

    public bool IsDryRun {
      get {
        return Get_IsDryRun();
      }
    }

    public DateTime RunDateTime {
      get;
      set;
    }

    public AxProfileSet AxProfileSet {
      get;
      set;
    }
    private ParmSet _parms;

    public AxProfile()
    {
      this.Name = String.Empty;
      this.ProfileStatus = ProfileStatus.NotSet;
      this.RunDateTime = DateTime.Now;
      this.VariableSet = new VariableSet();
      _parms = new ParmSet();
    }

    public AxProfile(string name)
    {
      this.Name = name;
      this.ProfileStatus = ProfileStatus.NotSet;
      this.RunDateTime = DateTime.Now;
      this.VariableSet = new VariableSet();
      _parms = new ParmSet();
    }

    public void Initialize()
    {
      ResolveVariables();
    }

    public void SetInitialParms(string[] args)
    {
      try
      {
        int i = 0;

        while (i < args.Length)
        {
          string arg = args[i].Trim();
          if (arg.StartsWith("-"))
          {
            if (i + 1 < args.Length)
            {
              string arg2 = args[i + 1].Trim();
              if (arg2.StartsWith("-"))
              {
                _parms.AddParm(arg, true);
                i++;
              }
              else
              {
                _parms.AddParm(arg, arg2);
                i += 2;
              }
            }
            else
            {
              if (arg.IsNotBlank())
              {
                _parms.AddParm(arg, true);
                i++;
              }
            }
          }
          else
          {
            if (arg.IsNotBlank())
            {
              _parms.AddParm(arg, true);
              i++;
            }
          }
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception has occurred while attempting to set the initial AxProfileParms from a string array", ex);
      }
    }

    private void ResolveVariables()
    {
      try
      {
        foreach (var axProfile in this.AxProfileSet.Values)
        {
          axProfile.RunDateTime = DateTime.Now;
          string yyyymmddhhss = axProfile.RunDateTime.ToString("yyyyMMdd-HHmmss");

          foreach (var axAction in axProfile.Values)
          {
            axAction.Src = ReplaceVariables(axAction.Src, axProfile);
            axAction.Tgt = ReplaceVariables(axAction.Tgt, axProfile);
          }
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to resolve the variables in the AxProfileSet.", ex);
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

        if (profile.VariableSet != null && profile.VariableSet.Count > 0)
        {
          if (profile.VariableSet.ContainsKey(variableName))
            variableSet = profile.VariableSet;
        }

        if (variableSet == null && this.AxProfileSet.VariableSet.ContainsKey(variableName))
          variableSet = this.AxProfileSet.VariableSet;

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

    private string ReplaceGlobalVariable(string value)
    {
      if (this.AxProfileSet == null)
        throw new Exception("The AxProfileSet reference is null.");

      if (this.AxProfileSet.VariableSet == null)
        throw new Exception("The AxProfileSet.VariableSet is null.");

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
        value = value.Replace("$" + variableName + "$", this.AxProfileSet.VariableSet[variableName]);

        variableCount++;
        if (variableCount > 10)
          throw new Exception("Greater than 10 variables found in configuration value '" + originalValue + "'.");

      } while (value.IndexOf("$") > -1);

      return value;
    }

    private bool Get_IsDryRun()
    {
      if (_parms == null || _parms.Count == 0)
        return false;

      return _parms.ParmExists("-dryrun");
    }
  }
}
