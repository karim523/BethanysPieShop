using BethanysPieShop.InventoryManagement.Domain.General;
using BethanysPieShop.InventoryManagement.Domain.ProductManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class ProductEntityConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product")
                 .HasKey(p => p.Id); ;

            builder.Property(p=>p.Name).IsRequired().HasMaxLength(50);
         
            builder.Property(p=>p.Id).ValueGeneratedOnAdd();
            
            builder.Property(p=>p.UnitType).IsRequired();
            
            builder.Property(p=>p.MaxAmountInStock).IsRequired();
            
            builder.Property(p=>p.MinAmountInStock).IsRequired();

            builder.Property(p => p.IsBelowStockThreshold).IsRequired();

            builder.Property(p=>p.Description).HasMaxLength(250);
            
            builder.Property(p=>p.AmountInStock);

            builder.HasOne(p => p.Price)
                .WithOne()
                .HasForeignKey<Price>("ProductId")
                .IsRequired();


        }
    }
}
