﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class StoxDataEntities : DbContext
    {
        public StoxDataEntities()
            : base("name=StoxDataEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
    
        public virtual int VFS_DIV(Nullable<int> yearReport)
        {
            var yearReportParameter = yearReport.HasValue ?
                new ObjectParameter("YearReport", yearReport) :
                new ObjectParameter("YearReport", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("VFS_DIV", yearReportParameter);
        }
    }
}
