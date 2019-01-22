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
    
    [DbMap(DbElement.Table, "Org_Software", "", "SoftwareRepository")]
    public partial class SoftwareRepository
    {
        public SoftwareRepository()
        {
            this.SoftwareVersions = new HashSet<SoftwareVersion>();
        }
    
        public int RepositoryId { get; set; }
        public int SoftwareStatusId { get; set; }
        public string RepositoryRoot { get; set; }
    
        public virtual SoftwareStatu SoftwareStatu { get; set; }
        public virtual ICollection<SoftwareVersion> SoftwareVersions { get; set; }
    }
}
