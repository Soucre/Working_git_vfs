//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PhimHang.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class CustomerLog
    {
        public long Id { get; set; }
        public string CustomerId { get; set; }
        public Nullable<long> Total_Login { get; set; }
        public Nullable<long> Total_Download { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
    }
}
