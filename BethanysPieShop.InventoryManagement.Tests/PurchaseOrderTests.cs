using BethanysPieShop.InventoryManagement.Domain.General;
using BethanysPieShop.InventoryManagement.Domain.OrderManagment;
using BethanysPieShop.InventoryManagement.Domain.ProductManagement;

namespace BethanysPieShop.InventoryManagement.Tests
{
    public class PurchaseOrderTests
    {
        [Fact]
        public void AddPurchaseOrder_WhenAddListOrderItemIsDuplicateAmountOrderSmallerThanAmountInStock_ShouldFulfilledBeFalse()
        {
            OrderPurchase order = new(1, DateTime.Now,
                new List<OrderItem>()
                {
                     new OrderItem( 20 ,new Product(1, "Sugar", new Price(10, Currency.Euro), UnitType.perKg, 80)),
                     new OrderItem( 20 ,new Product(1, "Sugar", new Price(10, Currency.Euro), UnitType.perKg, 80))
                });
            Assert.False(order.Fulfilled);
        }
        [Fact]
        public void AddPurchaseOrder_WhenAddListOrderItemIsNotDuplicateAmountOrderSmallerThanAmountInStock_ShouldFulfilledBeTrue()
        {
            OrderPurchase order = new(1, DateTime.Now,
                new List<OrderItem>()
                {
                    new OrderItem( 20 ,new Product(1, "Sugar", new Price(10, Currency.Euro), UnitType.perKg, 80)),
                    new OrderItem(10 ,new Product(2, "Eggs", new Price(9, Currency.Euro), UnitType.perBox, 60))
                });
            Assert.True(order.Fulfilled);
        }
        [Fact]
        public void AddPurchaseOrder_ShouldFulfilledBeFalse()
        {
            OrderPurchase order = new(1, DateTime.Now,
                new List<OrderItem>()
                {
                     new OrderItem(90 ,new Product(1, "Sugar", new Price(10, Currency.Euro), UnitType.perKg, 80)),
                     new OrderItem(10 ,new Product(2, "Eggs", new Price(9, Currency.Euro), UnitType.perBox, 60))
                });
            Assert.False(order.Fulfilled);
        }

        [Theory]
        [InlineData(-1, 0)]
        public void AddPurchaseOrder_WhenAddListOrderItemIsNotDuplicateAndAmountEqualZero_ShouldFulfilledBeFalse(int amount, int amount2)
        {
            OrderPurchase order = new(1,DateTime.Now,
                new List<OrderItem>()
                {
                      new OrderItem( amount ,new Product(1, "Sugar", new Price(10, Currency.Euro), UnitType.perKg, 80)),
                      new OrderItem( amount2 ,new Product(2, "Eggs", new Price(9, Currency.Euro), UnitType.perBox, 60))
                });

            var ex = Assert.Throws<ArgumentOutOfRangeException>(() =>
            new OrderItem(amount, new Product(1, "Sugar", new Price(10, Currency.Euro), UnitType.perKg, 80)));

            var ex2 = Assert.Throws<ArgumentOutOfRangeException>(() =>
            new OrderItem(amount2, new Product(2, "Eggs", new Price(9, Currency.Euro), UnitType.perBox, 60)));

            Assert.Contains("AmountOrdered is Invalid", ex.Message);

            Assert.Contains("AmountOrdered is Invalid", ex2.Message);

            Assert.False(order.Fulfilled);

        }
    }
}