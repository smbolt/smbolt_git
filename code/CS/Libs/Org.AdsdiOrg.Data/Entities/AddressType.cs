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
    
    [DbMap(DbElement.Table, "Adsdi_Org", "", "AddressType")]
    public partial class AddressType
    {
        public AddressType()
        {
            this.PersonAddresses = new HashSet<PersonAddress>();
        }
    
        public string AddressTypeCode { get; set; }
        public string AddressTypeValue { get; set; }
        public string AddressTypeDesc { get; set; }
    
        public virtual ICollection<PersonAddress> PersonAddresses { get; set; }
    }
}
