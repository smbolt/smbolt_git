//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Org.GS;
using Org.DB;
namespace Org.BMR.Data.Entities
{
  using System;
  using System.Collections.Generic;

  [DbMap(DbElement.Table, "Adsdi_Org", "", "StatusUsage")]
  public partial class StatusUsage
  {
    public int StatusUsageId {
      get;
      set;
    }
    public string StatusListName {
      get;
      set;
    }
    public int StatusId {
      get;
      set;
    }
    public int SortId {
      get;
      set;
    }
    public Nullable<bool> Omit {
      get;
      set;
    }

    public virtual Status Status {
      get;
      set;
    }
  }
}
