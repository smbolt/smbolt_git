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

  [DbMap(DbElement.Table, "Adsdi_Org", "", "ScheduledTaskParameter")]
  public partial class ScheduledTaskParameter
  {
    public int ParameterId {
      get;
      set;
    }
    public Nullable<int> ScheduledTaskId {
      get;
      set;
    }
    public string ParameterSetName {
      get;
      set;
    }
    public string ParameterName {
      get;
      set;
    }
    public string ParameterValue {
      get;
      set;
    }
    public string DataType {
      get;
      set;
    }
    public int CreatedBy {
      get;
      set;
    }
    public System.DateTime CreatedDate {
      get;
      set;
    }
    public int ModifiedBy {
      get;
      set;
    }
    public System.DateTime ModifiedDate {
      get;
      set;
    }

    public virtual ScheduledTask ScheduledTask {
      get;
      set;
    }
  }
}
