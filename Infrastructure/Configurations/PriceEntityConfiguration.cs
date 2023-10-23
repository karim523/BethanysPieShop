using BethanysPieShop.InventoryManagement.Domain.General;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class PriceEntityConfiguration : IEntityTypeConfiguration<Price>
    {
        public void Configure(EntityTypeBuilder<Price> builder)
        {
            builder.ToTable("Price");

            builder.Property(p=>p.ItemPrice).IsRequired();

            builder.Property(p=>p.Currency).IsRequired();


        }
    }
}
