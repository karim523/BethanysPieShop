using BethanysPieShop.InventoryManagement.Domain.General;
using BethanysPieShop.InventoryManagement.Domain.OrderManagment;
using BethanysPieShop.InventoryManagement.Domain.ProductManagement;

namespace BethanysPieShop.InventoryManagement.Tests
{
    public class PurchaseOrderTests
    {
        [Theory]
        [InlineData(1)]
        public void AddPurchaseOrder_WhenAddListOrderItemIsDuplicateAmountOrderSmallerThanAmountInStock_ShouldThrowException(int supplireId)
        {
            var ex = Assert.Throws<ArgumentException>(() => new OrderPurchase(supplireId, DateTime.Now,
                new List<OrderItem>()
                {
                     new OrderItem( 20 , Product.Create( "Sugar", Price.Create(10, Currency.Euro).Object , UnitType.perKg, 80,2,11).Object),
                     new OrderItem( 20 ,Product.Create( "Sugar", Price.Create(10, Currency.Euro).Object, UnitType.perKg, 80,2,11).Object)
                }));
            Assert.Contains($"The Product Id 0 is Duplicated", ex.Message);
            
        }
        [Theory]
        [InlineData(-1)]
        public void AddPurchaseOrder_WhenAddInvalidValueSupplierId_ShouldThrowException(int supplireId)
        {
            var ex = Assert.Throws<ArgumentException>(() => new OrderPurchase(supplireId, DateTime.Now,
                new List<OrderItem>()
                {
                     new OrderItem( 20 ,Product.Create("Sugar",  Price.Create(10, Currency.Euro).Object, UnitType.perKg, 80,2,11).Object),
                     new OrderItem( 20 ,Product.Create("Eggs",  Price.Create(10, Currency.Euro).Object, UnitType.perKg, 80,2,11).Object)
                }));

            Assert.Contains($"Supplier Id must not be Zero or Negative", ex.Message);

        }
        [Theory]
        [InlineData]
        public void AddPurchaseOrder_WhenAddInvalidValueOrderDate_ShouldThrowException()
        {

            var ex2 = Assert.Throws<ArgumentException>(() => new OrderPurchase(1, DateTime.Now.AddDays(2),
            new List<OrderItem>()
            {
                new OrderItem( 20 ,Product.Create("Sugar",  Price.Create(10, Currency.Euro).Object, UnitType.perKg,  80,2,11).Object),
                new OrderItem( 20 ,Product.Create("Eggs",  Price.Create(10, Currency.Euro).Object, UnitType.perKg,  80,2,11).Object)
            }));

            Assert.Contains($"Order Date must not be less than Today", ex2.Message);

        }
        [Fact]
        public void AddPurchaseOrder_WhenAddListOrderItemIsNotDuplicateAmountOrderSmallerThanAmountInStock_ShouldFulfilledBeTrue()
        {
            OrderPurchase order = new(1, DateTime.Now,
                new List<OrderItem>()
                {
                   new OrderItem( 20 ,Product.Create("Sugar",  Price.Create(10, Currency.Euro).Object, UnitType.perKg,  80,2,11).Object),
                   new OrderItem( 10 ,Product.Create("Eggs",  Price.Create(10, Currency.Euro).Object, UnitType.perKg,  80,2,11).Object)
                });
            Assert.True(order.Fulfilled);
        }
        [Fact]
        public void AddPurchaseOrder_ShouldFulfilledBeFalse()
        {
            OrderPurchase order = new(1, DateTime.Now,
                new List<OrderItem>()
                {
                    new OrderItem( 90 ,Product.Create("Sugar",  Price.Create(10, Currency.Euro).Object, UnitType.perKg,  80,2,11).Object),
                    new OrderItem( 10 ,Product.Create("Eggs",  Price.Create(10, Currency.Euro).Object, UnitType.perKg,  80,2,11).Object)
                });
            Assert.False(order.Fulfilled);
        }

        [Theory]
        [InlineData(0)]
        public void AddPurchaseOrder_WhenAddListOrderItemIsNotDuplicateAndAmountEqualZero_ShouldThrowExcption(int amountOrdered)
        {
            var ex = Assert.Throws<ArgumentException>(() =>
            new OrderItem(amountOrdered, Product.Create("Sugar", Price.Create(10, Currency.Euro).Object, UnitType.perKg, 80, 2, 11).Object));

            Assert.Equal(nameof(amountOrdered), ex.ParamName);



            //var ex2 = Assert.Throws<ArgumentException>(() =>
            //new OrderItem(amount2, Product.Create("Sugar",  Price.Create(10, Currency.Euro).Object, UnitType.perKg,  80,2,11).Object));

            //Assert.Contains("AmountOrdered is Invalid", ex2.Message);


        }
    }
}