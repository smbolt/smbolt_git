using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Reflection;
using Org.GS.Dynamic;
using Org.GS.UI;

namespace Org.GS.Configuration
{
  [ObfuscationAttribute(Exclude = true, ApplyToMembers = true)]
  [XMap(XType = XType.Element, KeyName="ConfigName")]
  public class ProgramConfig
  {
    [XMap(MyParent = true)]
    public ProgramConfigSet ProgramConfigSet {
      get;
      set;
    }

    [XMap]
    public string ConfigName {
      get;
      set;
    }

    [XMap(CollectionElements="CIGroup", WrapperElement="CISet")]
    public CISet CISet {
      get;
      set;
    }

    [XMap(CollectionElements="COSet")]
    public COSet COSet {
      get;
      set;
    }

    [XMap(CollectionElements="ConfigList", WrapperElement="ConfigListSet")]
    public ConfigListSet ConfigListSet {
      get;
      set;
    }

    [XMap(CollectionElements="ConfigDictionary", WrapperElement="ConfigDictionarySet")]
    public ConfigDictionarySet ConfigDictionarySet {
      get;
      set;
    }

    [XMap(CollectionElements = "FSActionGroup", WrapperElement = "FSActionSet")]
    public FSActionSet FSActionSet {
      get;
      set;
    }

    [XMap(XType = XType.Element)]
    public UIState UIState {
      get;
      set;
    }

    [XMap(CollectionElements = "TaskConfigSet", WrapperElement = "TaskConfigurations")]
    public TaskConfigurations TaskConfigurations {
      get;
      set;
    }

    [XMap(XType = XType.Element, CollectionElements="NotifyConfig", WrapperElement="NotifyConfigSet")]
    public NotifyConfigSet NotifyConfigSet {
      get;
      set;
    }

    [XMap(CollectionElements = "ProgramFunctionSet", WrapperElement = "ProgramFunctionControl")]
    public ProgramFunctionControl ProgramFunctionControl {
      get;
      set;
    }

    [XMap(XType = XType.Element)]
    public DynamicControl DynamicControl {
      get;
      set;
    }

    [XParm(ParmSource = XParmSource.Parent, Name = "parent")]
    [XParm(ParmSource = XParmSource.Attribute, Name = "configName", AttrName="ConfigName" )]
    public ProgramConfig(ProgramConfigSet parent, string configName)
    {
      this.ProgramConfigSet = parent;
      this.ConfigName = configName;
      this.DynamicControl = new DynamicControl();
      this.CISet = new CISet(this);
      this.COSet = new COSet(this);
      this.ConfigListSet = new ConfigListSet(this);
      this.ConfigDictionarySet = new ConfigDictionarySet(this);
      this.FSActionSet = new FSActionSet(this);
      this.UIState = new UIState();
      this.TaskConfigurations = new TaskConfigurations(this);
      this.NotifyConfigSet = new NotifyConfigSet(this);
      this.ProgramFunctionControl = new ProgramFunctionControl();
    }
  }
}
