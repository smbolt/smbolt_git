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
    
    [DbMap(DbElement.Table, "Adsdi_Org", "", "Country")]
    public partial class Country
    {
        public Country()
        {
            this.PoliticalUnits = new HashSet<PoliticalUnit>();
        }
    
        public int CountryCode { get; set; }
        public string CountryCodeA2 { get; set; }
        public string CountryCodeA3 { get; set; }
        public string CountryName { get; set; }
    
        public virtual ICollection<PoliticalUnit> PoliticalUnits { get; set; }
    }
}
