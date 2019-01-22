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
    
    [DbMap(DbElement.Table, "Adsdi_Org", "", "OrderDetail")]
    public partial class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public double LineItemDiscount { get; set; }
        public decimal DiscountedPrice { get; set; }
        public int StatusId { get; set; }
    
        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
        public virtual Status Status { get; set; }
    }
}
