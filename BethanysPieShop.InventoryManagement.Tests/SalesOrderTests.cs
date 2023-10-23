using BethanysPieShop.InventoryManagement.Domain.General;
using BethanysPieShop.InventoryManagement.Domain.OrderManagment;
using BethanysPieShop.InventoryManagement.Domain.ProductManagement;

namespace BethanysPieShop.InventoryManagement.Tests
{
    public class SalesOrderTests
    {
    
        [Fact]
        public void AddSalesOrder_WhenAmountOrderSmallerThanAmountInStock_ShouldAmountInStockBeChangedAndShouldFulfilledBeTrue()
        {
            Product product = new Product(1, "Sugar", new Price(10, Currency.Euro), UnitType.perKg, 100);

            OrderSale order = new OrderSale();

            order.AddOrder(product, 20);

            Assert.Equal(80, product.AmountInStock);
          
            Assert.True(order.Fulfilled);
        }
        
        [Fact]
        public void AddSalesOrder_WhenAmountOrderBiggerThanAmountInStock_ShouldFulfilledBeFalse()
        {
            Product product = new Product(1, "Sugar", new Price(10, Currency.Euro), UnitType.perKg, 100);

            OrderSale order = new OrderSale();

            order.AddOrder(product, 120);
         
            Assert.Equal(100, product.AmountInStock);

            Assert.False(order.Fulfilled);
        }
    }
}
