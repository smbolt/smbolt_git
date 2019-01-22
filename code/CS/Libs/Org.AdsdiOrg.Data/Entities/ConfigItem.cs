//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Org.DB;
using Org.GS;
namespace Org.AdsdiOrg.Data.Entities
{
  using System;
  using System.Collections.Generic;

  [DbMap(DbElement.Table, "Adsdi_Org", "", "ConfigItem")]
  public partial class ConfigItem
  {
    public int ConfigItemId {
      get;
      set;
    }
    public int ModuleCode {
      get;
      set;
    }
    public string ConfigItemTypeCode {
      get;
      set;
    }
    public int ConfigItemPriority {
      get;
      set;
    }
    public string Category {
      get;
      set;
    }
    public string ConfigItemKey {
      get;
      set;
    }
    public string ConfigItemValue {
      get;
      set;
    }
    public int StatusId {
      get;
      set;
    }

    public virtual ConfigItemType ConfigItemType {
      get;
      set;
    }
    public virtual Module Module {
      get;
      set;
    }
    public virtual Status Status {
      get;
      set;
    }
  }
}
