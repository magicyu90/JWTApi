using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using JWTApi.Authorization.Models;

namespace JWTApi.Authorization.Repository
{
    public class HugoContext : DbContext
    {
        public HugoContext()
            : base("name=HugoContext")
        {
        }
        public virtual DbSet<ClientModel> Clients { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

    }
}