using BethanysPieShop.InventoryManagement.Domain.OrderManagment;
using BethanysPieShop.InventoryManagement.Domain.SupplierManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class SupplierEntityConfiguration : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.ToTable("Supplier")
                .HasKey(s=>s.Id);

            builder.Property(s => s.Id).ValueGeneratedOnAdd();

            builder.Property(s => s.Name).IsRequired().HasMaxLength(100);
            
            builder.Property(s => s.Adress).IsRequired().HasMaxLength(100);

            builder.Property(s => s.Email).HasMaxLength(100);
            
            builder.Property(s => s.Phone).IsRequired().HasMaxLength(50);

            builder.HasMany<OrderPurchase>()
                .WithOne()
                .OnDelete(DeleteBehavior.Restrict);               
        }
    }
}