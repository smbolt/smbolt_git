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

  [DbMap(DbElement.Table, "Adsdi_Org", "", "RelatedOrg")]
  public partial class RelatedOrg
  {
    public int OrgId {
      get;
      set;
    }
    public int RelatedOrgId {
      get;
      set;
    }
    public string RelationshipTypeCode {
      get;
      set;
    }
    public Nullable<System.DateTime> RelationshipBeginDateTime {
      get;
      set;
    }
    public Nullable<System.DateTime> RelationshipEndDateTime {
      get;
      set;
    }
    public int StatusId {
      get;
      set;
    }

    public virtual RelationshipType RelationshipType {
      get;
      set;
    }
    public virtual Organization Organization {
      get;
      set;
    }
    public virtual Organization Organization1 {
      get;
      set;
    }
  }
}
