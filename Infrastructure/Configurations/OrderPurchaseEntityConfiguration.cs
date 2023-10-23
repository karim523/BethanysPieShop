using BethanysPieShop.InventoryManagement.Domain.OrderManagment;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class OrderPurchaseEntityConfiguration : IEntityTypeConfiguration<OrderPurchase>
    {
        public void Configure(EntityTypeBuilder<OrderPurchase> builder)
        {
            builder.ToTable("OrderPurchase")
                .HasKey(p => p.Id);
          
            builder.Property(p=>p.Id).ValueGeneratedOnAdd();
           
            builder.Property(p=>p.OrderDate).IsRequired();
           
            builder.Property(p=>p.Fulfilled).IsRequired();
        
            builder.Property(p=>p.SupplierId).IsRequired();

        }
    }
}
