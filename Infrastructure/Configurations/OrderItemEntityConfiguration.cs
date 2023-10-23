using BethanysPieShop.InventoryManagement.Domain.OrderManagment;
using BethanysPieShop.InventoryManagement.Domain.ProductManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class OrderItemEntityConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("OrderItem")
                .HasKey(p => p.Id); 
           
            builder.Property(o=>o.Id).ValueGeneratedOnAdd();
            
            builder.Property(o=>o.AmountOrdered).IsRequired();

            builder.HasOne<OrderPurchase>()
               .WithMany(p=>p.OrderItems)
               .HasForeignKey("OrderPurchaseId")
               .IsRequired();

            builder.HasOne(x=>x.Product)
                .WithMany()
                .HasForeignKey("ProductId")
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
        }
    }
}