﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Colmart
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ColmartDBContext : DbContext
    {
        public ColmartDBContext()
            : base("name=ColmartDBContext")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<tblCMSPages> tblCMSPages { get; set; }
        public virtual DbSet<tblCMSRoleTypes> tblCMSRoleTypes { get; set; }
        public virtual DbSet<tblCMSUserAccess> tblCMSUserAccess { get; set; }
        public virtual DbSet<tblCMSUsers> tblCMSUsers { get; set; }
        public virtual DbSet<tblPages> tblPages { get; set; }
        public virtual DbSet<tblProductCategories> tblProductCategories { get; set; }
        public virtual DbSet<tblProducts> tblProducts { get; set; }
        public virtual DbSet<tblProductSizes> tblProductSizes { get; set; }
        public virtual DbSet<tblProductSubCategories> tblProductSubCategories { get; set; }
        public virtual DbSet<tblRoleTypes> tblRoleTypes { get; set; }
        public virtual DbSet<tblUserAccess> tblUserAccess { get; set; }
        public virtual DbSet<tblUsers> tblUsers { get; set; }
    }
}