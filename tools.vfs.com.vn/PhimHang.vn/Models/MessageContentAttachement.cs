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
    
    public partial class MessageContentAttachement
    {
        public int MessageContentAttachementID { get; set; }
        public string AttachementDocument { get; set; }
        public string AttachementDescription { get; set; }
        public Nullable<int> MessageContentID { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    
        public virtual MessageContent MessageContent { get; set; }
    }
}
