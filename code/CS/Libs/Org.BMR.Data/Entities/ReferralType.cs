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
    
    [DbMap(DbElement.Table, "Adsdi_Org", "", "ReferralType")]
    public partial class ReferralType
    {
        public ReferralType()
        {
            this.Referrals = new HashSet<Referral>();
        }
    
        public string ReferralTypeCode { get; set; }
        public string ReferralTypeDesc { get; set; }
    
        public virtual ICollection<Referral> Referrals { get; set; }
    }
}
