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

namespace Org.Software.Data.Entities
{
    using System;
    using System.Collections.Generic;
    
    [DbMap(DbElement.Table, "Org_Software", "", "OrgStatu")]
    public partial class OrgStatu
    {
        public OrgStatu()
        {
            this.Organizations = new HashSet<Organization>();
        }
    
        public int OrgStatusId { get; set; }
        public string OrgStatusAbbr { get; set; }
        public string OrgStatusValue { get; set; }
    
        public virtual ICollection<Organization> Organizations { get; set; }
    }
}
