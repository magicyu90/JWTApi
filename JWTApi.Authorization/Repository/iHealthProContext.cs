using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using JWTApi.Authorization.Models;


namespace JWTApi.Authorization.Repository
{
    public partial class iHealthProContext : DbContext
    {
        public iHealthProContext()
            : base("name=iHealthProContext")
        {
        }

        public virtual DbSet<DoctorAccount> DoctorAccounts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DoctorAccount>()
                .Property(e => e.LT)
                .IsUnicode(false);

            modelBuilder.Entity<DoctorAccount>()
                .Property(e => e.OpenID)
                .IsUnicode(false);
        }
    }
}
