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
    
    [DbMap(DbElement.Table, "Org_Software", "", "SoftwareModule")]
    public partial class SoftwareModule
    {
        public SoftwareModule()
        {
            this.SoftwareVersions = new HashSet<SoftwareVersion>();
        }
    
        public int SoftwareModuleId { get; set; }
        public int SoftwareModuleCode { get; set; }
        public string SoftwareModuleName { get; set; }
        public int SoftwareModuleTypeId { get; set; }
        public int SoftwareStatusId { get; set; }
        public System.DateTime CreatedDateTime { get; set; }
        public int CreatedAccountId { get; set; }
        public Nullable<System.DateTime> ModifiedDateTime { get; set; }
        public Nullable<int> ModifiedAccountId { get; set; }
    
        public virtual SoftwareModuleType SoftwareModuleType { get; set; }
        public virtual SoftwareStatu SoftwareStatu { get; set; }
        public virtual ICollection<SoftwareVersion> SoftwareVersions { get; set; }
    }
}