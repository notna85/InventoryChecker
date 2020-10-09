using InventoryChecker.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryChecker.DAL
{
    //The database context class for the database
    public class FreezerContext : DbContext
    {
        public FreezerContext(DbContextOptions<FreezerContext> options) : base(options)
        {
        }
        public DbSet<Category> Category { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductAmount> ProductAmount { get; set; }
        public DbSet<StorageType> StorageType { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductAmount>()
                .HasKey(pa => new { pa.Product, pa.StorageType }); //Creates a composite primary key for that entity
        }
    }
}
