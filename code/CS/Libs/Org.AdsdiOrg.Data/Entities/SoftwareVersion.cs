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
    
    [DbMap(DbElement.Table, "Adsdi_Org", "", "SoftwareVersion")]
    public partial class SoftwareVersion
    {
        public int SoftwareVersionId { get; set; }
        public int SoftwareModuleId { get; set; }
        public string SoftwareVersion1 { get; set; }
        public int SoftwarePlatformId { get; set; }
        public int StatusId { get; set; }
        public int RepositoryId { get; set; }
    
        public virtual SoftwareModule SoftwareModule { get; set; }
        public virtual SoftwareRepository SoftwareRepository { get; set; }
        public virtual Status Status { get; set; }
        public virtual SoftwarePlatform SoftwarePlatform { get; set; }
    }
}
