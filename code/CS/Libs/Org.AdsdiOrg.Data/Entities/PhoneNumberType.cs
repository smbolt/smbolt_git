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
    
    [DbMap(DbElement.Table, "Adsdi_Org", "", "PhoneNumberType")]
    public partial class PhoneNumberType
    {
        public PhoneNumberType()
        {
            this.PersonPhoneNumbers = new HashSet<PersonPhoneNumber>();
        }
    
        public string PhoneNumberTypeCode { get; set; }
        public string PhoneNumberTypeValue { get; set; }
        public string PhoneNumberTypeDesc { get; set; }
    
        public virtual ICollection<PersonPhoneNumber> PersonPhoneNumbers { get; set; }
    }
}
