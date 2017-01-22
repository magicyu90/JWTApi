using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using JWTApi.Resource.Models;

namespace JWTApi.Resource.Repository
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