using BethanysPieShop.InventoryManagement.Domain.General;
namespace BethanysPieShop.InventoryManagement.Tests
{
    public class PriceTests
    {
        [Fact]
        public void Price_ShouldPriceNotNull()
        {
            Price price = new(12, Currency.Dollar);

            Assert.NotNull(price);
        }
        [Theory]
        [InlineData(0)]
        public void Price_AddPriceItemSmallerThanZero_ShouldItemPriceIsInvalid(int itemPrice)
        {
         

            var ex = Assert.Throws<ArgumentException>(() =>new Price(itemPrice,Currency.Dollar));

            Assert.Contains("ItemPrice is invalid", ex.Message);
        }
        [Theory]
        [InlineData(-1)]
        public void Price_AddPriceItemEqualZero_ShouldItemPriceIsInvalid(int itemPrice)
        {

            var ex = Assert.Throws<ArgumentException>(() => new Price(itemPrice, Currency.Dollar));
            
            Assert.Contains("ItemPrice is invalid", ex.Message);
        }
    }
}
