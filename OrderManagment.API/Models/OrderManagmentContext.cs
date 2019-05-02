using OrderManagment.API.Mappings;
using OrderManagment.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace OrderManagment.API.Models
{
    public class OrderManagmentContext : DbContext
    {
        public OrderManagmentContext(): base("name=OrderManagment")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<OrderManagmentContext,
              OrderManagment.API.Migrations.Configuration>());
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
     

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CategoryMapping());
            modelBuilder.Configurations.Add(new CustomerMapping());
            modelBuilder.Configurations.Add(new ProductMapping());
            modelBuilder.Configurations.Add(new OrderMapping());
        }
    }
}