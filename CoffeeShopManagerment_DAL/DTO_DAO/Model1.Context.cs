﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CoffeeShopManagerment_DAL.DTO_DAO
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class coffeemanagerDBEntities : DbContext
    {
        public coffeemanagerDBEntities()
            : base("name=coffeemanagerDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<dateTable> dateTables { get; set; }
        public virtual DbSet<Staff> Staffs { get; set; }
        public virtual DbSet<Schedule> Schedules { get; set; }
    }
}
