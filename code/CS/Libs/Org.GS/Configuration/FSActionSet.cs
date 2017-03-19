using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Reflection;
using Org.GS;

namespace Org.GS.Configuration
{
  [ObfuscationAttribute(Exclude = true, ApplyToMembers = true)]
  [XMap(CollectionElements = "FSActionGroup", XType = XType.Element)] 
  public class FSActionSet : Dictionary<string, FSActionGroup>
  {
    [XMap(XType = XType.Element, ClassName = "ConfigDictionary", Name = "Variables")]
    public ConfigDictionary Variables { get; set; }

    private Dictionary<string, string> _variables;
    
    [XMap]
    public string Src { get; set; }

    [XMap]
    public string Dst { get; set; }

    [XMap(IsRequired = false, DefaultValue = "False")]
    public bool UseActionSetVariables { get; set; }

    [XMap(MyParent = true)]
    public ProgramConfig ProgramConfig { get; set; }

    public bool ContinueProcessing { get; set; }

    [XParm(Name = "parent", ParmSource = XParmSource.Parent, AttrName = "", Required = false)]
    public FSActionSet(ProgramConfig parent)
    {
      this.ProgramConfig = parent;
      this.UseActionSetVariables = false;
      this.ContinueProcessing = true;
      this.Src = String.Empty;
      this.Dst = String.Empty;
    }

    public void Initialize()
    {
      this.ContinueProcessing = true;

      foreach (var fsActionGroup in this.Values)
      {
        fsActionGroup.ContinueProcessing = true;

        if (fsActionGroup.FSActionSet == null)
          fsActionGroup.FSActionSet = this;

        foreach (var fsAction in fsActionGroup.Values)
        {
          if (fsAction.FSActionGroup == null)
            fsAction.FSActionGroup = fsActionGroup;

          if (fsAction.FSActionSet == null)
            fsAction.FSActionSet = this;
        }
      }
    }

    public string ResolveVariables(string value)
    {
      string originalConfigValue = value;

      if (_variables == null)
      {
        _variables = new Dictionary<string, string>();
        foreach (var di in this.Variables)
        {
          if (!_variables.ContainsKey(di.Key))
            _variables.Add(di.Key, di.Value);
        }
      }

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
        if (!_variables.ContainsKey(variableName))
          return value;

        value = value.Replace("$" + variableName + "$", _variables[variableName]);

        variableCount++;
        if (variableCount > 10)
          throw new Exception("Greater than 10 variables found in configuration value '" + originalConfigValue + "'.");

      } while (value.IndexOf("$") > -1);

      return value;
    }
  }
}
