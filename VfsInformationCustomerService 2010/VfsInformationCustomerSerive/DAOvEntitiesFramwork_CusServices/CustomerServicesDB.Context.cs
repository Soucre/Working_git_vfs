﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAOvEntitiesFramwork_CusServices
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class VfsCustomerServiceEntities : DbContext
    {
        public VfsCustomerServiceEntities()
            : base("name=VfsCustomerServiceEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ContentParameter> ContentParameters { get; set; }
        public virtual DbSet<ContentTemplate> ContentTemplates { get; set; }
        public virtual DbSet<ContentTemplateAttachement> ContentTemplateAttachements { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<CustomerType> CustomerTypes { get; set; }
        public virtual DbSet<ExtensionMessage> ExtensionMessages { get; set; }
        public virtual DbSet<ExtensionMessageLog> ExtensionMessageLogs { get; set; }
        public virtual DbSet<GroupCustomer> GroupCustomers { get; set; }
        public virtual DbSet<GroupDetail> GroupDetails { get; set; }
        public virtual DbSet<IncomingMessageContent> IncomingMessageContents { get; set; }
        public virtual DbSet<IncomingMessageContentSent> IncomingMessageContentSents { get; set; }
        public virtual DbSet<MessageCommand> MessageCommands { get; set; }
        public virtual DbSet<MessageContent> MessageContents { get; set; }
        public virtual DbSet<MessageContentAttachement> MessageContentAttachements { get; set; }
        public virtual DbSet<MessageContentSent> MessageContentSents { get; set; }
        public virtual DbSet<MessageContentSentAttachement> MessageContentSentAttachements { get; set; }
        public virtual DbSet<ServiceType> ServiceTypes { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<UserLogin> UserLogins { get; set; }
    }
}
