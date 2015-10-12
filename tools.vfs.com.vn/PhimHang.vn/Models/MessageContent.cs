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
    
    public partial class MessageContent
    {
        public MessageContent()
        {
            this.MessageCommands = new HashSet<MessageCommand>();
            this.MessageContentAttachements = new HashSet<MessageContentAttachement>();
        }
    
        public int MessageContentID { get; set; }
        public Nullable<int> ContentTemplateID { get; set; }
        public Nullable<int> ServiceTypeID { get; set; }
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public string Subject { get; set; }
        public string BodyContentType { get; set; }
        public string BodyEncoding { get; set; }
        public string BodyMessage { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> Status { get; set; }
        public string ServiceID { get; set; }
        public string CommandCode { get; set; }
        public string Request { get; set; }
        public string MoID { get; set; }
        public string ChargeYN { get; set; }
        public Nullable<short> TotalMessages { get; set; }
    
        public virtual ICollection<MessageCommand> MessageCommands { get; set; }
        public virtual ICollection<MessageContentAttachement> MessageContentAttachements { get; set; }
    }
}
