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

  [DbMap(DbElement.Table, "Adsdi_Org", "", "RelationshipType")]
  public partial class RelationshipType
  {
    public RelationshipType()
    {
      this.RelatedOrgs = new HashSet<RelatedOrg>();
      this.RelatedPersons = new HashSet<RelatedPerson>();
    }

    public string RelationshipTypeCode {
      get;
      set;
    }
    public string RelationshipTypeDesc {
      get;
      set;
    }

    public virtual ICollection<RelatedOrg> RelatedOrgs {
      get;
      set;
    }
    public virtual ICollection<RelatedPerson> RelatedPersons {
      get;
      set;
    }
  }
}
