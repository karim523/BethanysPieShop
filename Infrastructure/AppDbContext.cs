using BethanysPieShop.InventoryManagement.Domain.General;
using BethanysPieShop.InventoryManagement.Domain.OrderManagment;
using BethanysPieShop.InventoryManagement.Domain.ProductManagement;
using BethanysPieShop.InventoryManagement.Domain.SupplierManagement;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class AppDbContext:DbContext
    {
        //private readonly string _connectionString;

        //public AppDbContext()
        //{
        //    _connectionString = @"data source=KARIMKESHTA;Database=BethanysPieShop;MultipleActiveResultSets=True;Integrated Security=True;trusted_connection=True;TrustServerCertificate=True;";
        //}
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(_connectionString,
        //        dbOptions => dbOptions.MigrationsHistoryTable("__EFMigrationsHistory"));
        //}
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Price> Prices { get; set; }
        public DbSet<OrderPurchase> OrderPurchases { get; set;}
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }

    }
}
