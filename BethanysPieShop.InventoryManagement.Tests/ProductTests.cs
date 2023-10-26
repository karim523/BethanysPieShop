using BethanysPieShop.InventoryManagement.Domain.General;
using BethanysPieShop.InventoryManagement.Domain.ProductManagement;

namespace BethanysPieShop.InventoryManagement.Tests
{
    public class ProductTests
    {
        [Fact]
        public void product_ShouldProductNotNull()
        {
           var createResult= Product.Create("Sugar", Price.Create(10, Currency.Euro).Object, UnitType.perKg, 80, 2, 11);
            
            Assert.NotNull(createResult.Object);

            Assert.Equal("Sugar", createResult.Object.Name);

            Assert.Equal(11, createResult.Object.AmountInStock);

            Assert.Equal(UnitType.perKg, createResult.Object.UnitType);

            Assert.Equal(10, createResult.Object.Price.ItemPrice);

            Assert.Equal(Currency.Euro, createResult.Object.Price.Currency);

        }
        [Fact]
        public void Product_InvalidProductIdAndAmountInStockAndProductName( )
        {
            var createResult = Product.Create("", Price.Create(10, Currency.Euro).Object, UnitType.perKg, 100, 2, 100);

            Assert.Null(createResult.Object);

            var createResult1 = Product.Create("", Price.Create(10, Currency.Euro).Object, UnitType.perKg, 100, 2, 0);

            Assert.Null(createResult1.Object);          
        }
        [Fact]
        public void UseProduct_Reduces_AmountInStock()
        {
            //Arrange
            var createResult = Product.Create("Sugar", Price.Create(10, Currency.Euro).Object, UnitType.perKg, 100, 2, 100);

            //Act
            createResult.Object.UseProduct(20);

            //Assert
            Assert.Equal(80, createResult.Object.AmountInStock);
            Assert.False(createResult.Object.IsBelowStockThreshold);

        }
        [Fact]
        public void UseProduct_ItemsHigherThanStock_NoChangetoStock()
        {
            //Arrange
            var createResult = Product.Create("Sugar", Price.Create(10, Currency.Euro).Object, UnitType.perKg, 100, 2, 100);

            //Act
            createResult.Object.UseProduct(120);

            //Assert
            Assert.Equal(100, createResult.Object.AmountInStock);
            Assert.False(createResult.Object.IsBelowStockThreshold);

        }
        [Fact]
        public void UseProduct_ItemsTheSameAsAmountInStock_ShouldAmoutInStockEqualZero()
        {
            //Arrange
            var createResult = Product.Create("Sugar", Price.Create(10, Currency.Euro).Object, UnitType.perKg, 100, 2, 100);


            //Act
            createResult.Object.UseProduct(100);

            //Assert
            Assert.Equal(0, createResult.Object.AmountInStock);
            Assert.True(createResult.Object.IsBelowStockThreshold);
        }
        [Fact]
        public void UseProduct_ItemsEqualZero_ShouldNoChangeInAmoutInStock()
        {
            //Arrange
            var createResult = Product.Create("Sugar", Price.Create(10, Currency.Euro).Object, UnitType.perKg, 100, 2, 100);

            //Act
            var result = createResult.Object.UseProduct(0);

            //Assert
            Assert.Equal(100, createResult.Object.AmountInStock);

            Assert.False(createResult.Object.IsBelowStockThreshold);

            Assert.False(result);
          
        }
        [Fact]
        public void UseProduct_Reduces_AmountInStock_StockBelowThresholdShouldBeTrue()
        {
            //Arrange
            var createResult = Product.Create("Sugar", Price.Create(10, Currency.Euro).Object, UnitType.perKg, 100, 2, 100);

            //Act
            createResult.Object.UseProduct(99);
                
            //Assert
            Assert.True(createResult.Object.IsBelowStockThreshold);
        }


        [Fact]
        public void IncreaseStock_AddValueForAmountInStockSmallerThanMaxItemInStockAndAmountInStockEqualMaxItemInStock_ShouldResultTheSameAsValueForMaxItemInStock()
        {
            //Arrange
            var createResult = Product.Create("Sugar", Price.Create(10, Currency.Euro).Object, UnitType.perKg, 100, 2,100);

            //Act
            createResult.Object.IncreaseStock(20);         

            //Assert
            Assert.Equal(100,createResult.Object.AmountInStock);
        }
        [Fact]
        public void IncreaseStock_AddValueForAmountInStockBigeerThanMaxItemInStock_ShouldResultEqualMaxAmountInStock()
        {
            //Arrange
            var createResult = Product.Create("Sugar", Price.Create(10, Currency.Euro).Object, UnitType.perKg, 100, 2);

            //Act
            createResult.Object.IncreaseStock(300);

            //Assert
            Assert.Equal(100, createResult.Object.AmountInStock);
        }
        [Fact]
        public void IncreaseStock_AddValueForAmountInStockBigeerThanMaxItemInStock_IsBelowStockThresholdShouldBeFalse()
        {
            //Arrange
            var createResult = Product.Create("Sugar", Price.Create(10, Currency.Euro).Object, UnitType.perKg, 80, 2);

            //Act
            createResult.Object.IncreaseStock(300);

            //Assert
            Assert.False(createResult.Object.IsBelowStockThreshold);
        }
        [Fact]
        public void IncreaseStock_AddValueForAmountInStockSmallerThanMaxItemInStock_ShouldResultEqualValueOfIncrease()
        {
            //Arrange
            var createResult = Product.Create("Sugar", Price.Create(10, Currency.Euro).Object, UnitType.perKg, 80, 2, 11);

            //Act
            createResult.Object.IncreaseStock(50);

            //Assert
            Assert.Equal(61, createResult.Object.AmountInStock);
        }
        //[Fact]
        //public void IncreaseStock_AddValueForAmountInStockSmallerThanMaxItemInStock_ShouldResultEqualValueOfMaxAmount()
        //{
        //    //Arrange
        //    var createResult = Product.Create("Sugar", Price.Create(10, Currency.Euro).Object, UnitType.perKg, 80, 2, 11);
        //    //Act
        //    createResult.Object.IncreaseStock(50);

        //    //Assert
        //    Assert.Equal(100, createResult.Object.AmountInStock);
        //}

        //[Fact]
        //public void UpdateLowStock_WhenValueAmountInStockSmallerThanTen_ShouldIsBelowStockThresholdIsTrue()
        //{
        //    Product product = new Product(1, "Sugar", new Price(10, Currency.Euro), UnitType.perKg, 100);


        //    product.UpdateLowStock();

        //    Assert.True(product.IsBelowStockThreshold);
        //}
        //[Fact]
        //public void UpdateLowStock_WhenValueAmountInStockBiggerThanTen_ShouldIsBelowStockThresholdIsFalse()
        //{
        //    Product product = new Product(1, "Sugar", new Price(10, Currency.Euro), UnitType.perKg  , 100);



        //    product.UpdateLowStock();

        //    Assert.False(product.IsBelowStockThreshold);
        //}
        //[Fact]
        //public void DisplayDetailsFull_WhenIsBelowStockThresholdIsFalse_ShouldShowAllData()
        //{
        //    Product product = new Product(1, "Sugar", new Price(10, Currency.Euro), UnitType.perKg, 100);


        //    Assert.Equal($"{product.Id} {product.Name} \n{product.Description}\n{product.Price}\n{product.AmountInStock} item(s) in stock ", product.DisplayDetailsFull());
        //} 
        //[Fact]
        //public void DisplayDetailsFull_WhenIsBelowStockThresholdIsTrue_ShouldShowAllData()
        //{
        //    Product product = new Product(1, "Sugar", new Price(10, Currency.Euro), UnitType.perKg,3);

        //    product.IncreaseStock(20);

        //    Assert.Equal($"{product.Id} {product.Name} \n{product.Description}\n" +
        //        $"{product.Price}\n{product.AmountInStock} item(s) in stock \n!! STOCK LOW!!", 
        //        product.DisplayDetailsFull());
        //}
        //[Fact]
        //public void DisplayDetailsShort_ShouldShowShortData()
        //{
        //    Product product = new Product(1, "Sugar", new Price(10, Currency.Euro), UnitType.perKg, 100);


        //    Assert.Equal($"{product.Id}.{product.Name} \n {product.AmountInStock} items in stock", product.DisplayDetailsShort());
        //}
    }
}