using Accounts.Data.Classes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounts
{
    class AccountsContext : DbContext
    {
        public AccountsContext()
        {
        }

        public AccountsContext( DbContextOptions<AccountsContext> options ) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"data source=(LocalDb)\MSSQLLocalDB;initial catalog=Accounts2021;integrated security=True;MultipleActiveResultSets=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder )
        {
            modelBuilder.Entity<Item>().Ignore( e => e.Entity );

            modelBuilder.Entity<User>().ToTable( "Users" );
            modelBuilder.Entity<Organization>().ToTable( "Organizations" );
        }

        public DbSet<User>         Users         { get; set; }
        public DbSet<Organization> Organizations { get; set; }
    }
}
