using BethanysPieShop.InventoryManagement.Domain.General;
namespace BethanysPieShop.InventoryManagement.Tests
{
    public class PriceTests
    {
        [Fact]
        public void Price_ShouldPriceNotNull()
        {
           var createResult = Price.Create(12, Currency.Dollar);

            Assert.NotNull(createResult.Object);
        }
        [Fact]
        public void Price_AddPriceItemSmallerThanZero_ShouldItemPriceIsInvalid()
        {
            var createResult = Price.Create(-222, Currency.Dollar);

            Assert.Null(createResult.Object);
        }
        [Fact]
        public void Price_AddPriceItemEqualZero_ShouldItemPriceIsInvalid()
        {

            var createResult =Price.Create(0, Currency.Dollar);

            Assert.Null(createResult.Object);
        }
    }
}
