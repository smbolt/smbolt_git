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
    
    [DbMap(DbElement.Table, "Org_Software", "", "Organization")]
    public partial class Organization
    {
        public Organization()
        {
            this.AppLogs = new HashSet<AppLog>();
            this.Organization1 = new HashSet<Organization>();
        }
    
        public int OrgId { get; set; }
        public Nullable<int> ParentOrgId { get; set; }
        public int OrgStatusId { get; set; }
        public int OrgTypeId { get; set; }
        public string OrgName { get; set; }
        public string OrgDescription { get; set; }
    
        public virtual ICollection<AppLog> AppLogs { get; set; }
        public virtual ICollection<Organization> Organization1 { get; set; }
        public virtual Organization Organization2 { get; set; }
        public virtual OrgStatu OrgStatu { get; set; }
        public virtual OrgType OrgType { get; set; }
    }
}