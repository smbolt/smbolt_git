using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using Org.GS;

namespace Org.GS.Code
{
  [ObfuscationAttribute(Exclude = true)]
  public enum ProfileStatus
  {
    NotSet,
    Active,
    Disabled
  }

  [ObfuscationAttribute(Exclude = true)]
  public enum InclusionResult
  {
    IncludedByDefault,
    IncludedFileMatch,
    IncludedByExtension,
    IncludedExtensionExclusionSpec,
    ExcludedByExtension,
    ExcludedBySpec
  }

  [ObfuscationAttribute(Exclude = true, ApplyToMembers = true)]
  [XMap(XType = XType.Element)] 
  public class Profile
  {
    [XMap(XType = XType.Element, WrapperElement = "VariableSet", CollectionElements = "Variable", UseKeyValue = true)]
    public VariableSet VariableSet { get; set; }

    [XMap(IsKey=true)]
    public string Name { get; set; }
    public string NameLower { get { return (this.Name.IsNotBlank() ? this.Name.ToLower() : String.Empty); } }

    [XMap(XType=XType.Element, WrapperElement="MappingControlSet", CollectionElements="MappingControl")]
    public MappingControlSet MappingControlSet { get; set; }

    [XMap(XType=XType.Element, WrapperElement="OpsControlSet", CollectionElements="OpsControl")]
    public OpsControlSet OpsControlSet { get; set; }

    [XMap(DefaultValue="Active")]
    public ProfileStatus ProfileStatus { get; set; }

    public DateTime RunDateTime { get; set; }

    public Profile()
    {
      this.Name = String.Empty;
      this.MappingControlSet = new MappingControlSet();
      this.OpsControlSet = new OpsControlSet();
      this.ProfileStatus = ProfileStatus.NotSet;
      this.VariableSet = new VariableSet();
      this.RunDateTime = DateTime.Now;
    }
        
    public Profile(string name)
    {
      this.Name = name;
      this.MappingControlSet = new MappingControlSet();
      this.OpsControlSet = new OpsControlSet();
      this.ProfileStatus = ProfileStatus.NotSet;
      this.VariableSet = new VariableSet();
      this.RunDateTime = DateTime.Now;
    }

    public Profile(string name, string source)
    {
      this.Name = name;
      this.MappingControlSet = new MappingControlSet();
      this.OpsControlSet = new OpsControlSet();
      this.ProfileStatus = ProfileStatus.NotSet;
      this.VariableSet = new VariableSet();
      this.RunDateTime = DateTime.Now;
    }
  }
}
