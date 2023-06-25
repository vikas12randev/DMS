using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using TT.Deliveries.Data.Models;

namespace TT.Delieveries.Persistence.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<AccessWindow> AccessWindows { get; set; }

        public DbSet<Delivery> Deliveries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            modelBuilder.Entity<Delivery>()
              .ToTable("Delivery");

            modelBuilder.Entity<AccessWindow>()
                        .HasNoKey();

        }
    }
}

