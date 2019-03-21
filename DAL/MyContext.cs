using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class MyContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Registration> Registrations { get; set; }
        public DbSet<UserAccount> UserAccounts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Registration>()
                .HasKey(x => x.RegistrationId);

            modelBuilder.Entity<UserAccount>()
                .HasKey(x => x.UserAccountId);

            modelBuilder.Entity<User>()
                .HasRequired(x => x.Registration)
                .WithRequiredPrincipal(x => x.User)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<User>()
                .HasRequired(x => x.UserAccount)
                .WithRequiredPrincipal(x => x.User)
                .WillCascadeOnDelete(true);

            base.OnModelCreating(modelBuilder);
        }
    }
}
