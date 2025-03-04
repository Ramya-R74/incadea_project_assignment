using Microsoft.EntityFrameworkCore;
using Mini_ERP_System.Models;

namespace Mini_ERP_System.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<SalesOrder> SalesOrders { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define Foreign Key Relationships for SalesOrder
            modelBuilder.Entity<SalesOrder>()
                .HasOne(s => s.Customer)
                .WithMany()
                .HasForeignKey(s => s.CustomerId)
                .OnDelete(DeleteBehavior.Cascade); // If a customer is deleted, their orders are deleted.

            modelBuilder.Entity<SalesOrder>()
                .HasOne(s => s.Product)
                .WithMany()
                .HasForeignKey(s => s.ProductId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent deletion if a product is referenced in sales.

            modelBuilder.Entity<SalesOrder>()
                .HasOne(s => s.User)
                .WithMany()
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.SetNull); // If a user (salesperson) is deleted, set UserId to NULL.


            // Define Foreign Key Relationships for PurchaseOrder
            modelBuilder.Entity<PurchaseOrder>()
                .HasOne(p => p.Supplier)
                .WithMany()
                .HasForeignKey(p => p.SupplierId)
                .OnDelete(DeleteBehavior.Cascade); // Delete purchase orders if supplier is deleted

            modelBuilder.Entity<PurchaseOrder>()
                .HasOne(p => p.Product)
                .WithMany()
                .HasForeignKey(p => p.ProductId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent deletion if product is referenced in purchases

            modelBuilder.Entity<PurchaseOrder>()
                .HasOne(p => p.User)
                .WithMany()
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.SetNull); // Set UserId to NULL if the purchase officer is deleted

            base.OnModelCreating(modelBuilder);
        }
    }
}
