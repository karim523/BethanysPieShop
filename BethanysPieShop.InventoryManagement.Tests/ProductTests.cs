using BethanysPieShop.InventoryManagement.Domain.General;
using BethanysPieShop.InventoryManagement.Domain.ProductManagement;

namespace BethanysPieShop.InventoryManagement.Tests
{
    public class ProductTests
    {
        [Fact]
        public void product_ShouldProductNotNull()
        {
            Product product = new Product(1, "Sugar", new Price(10, Currency.Euro), UnitType.perKg, 100 );
            
            Assert.NotNull(product);

            Assert.Equal("Sugar", product.Name);

            Assert.Equal(1, product.Id);

            Assert.Equal(100, product.AmountInStock);

            Assert.Equal(UnitType.perKg, product.UnitType);

            Assert.Equal(10, product.Price.ItemPrice);

            Assert.Equal(Currency.Euro, product.Price.Currency);

        }
        [Theory]
        [InlineData(-1,0, "")]
        public void Product_InvalidProductIdAndAmountInStockAndProductName(int productId,int amountInStock, string productName)
        {
           

            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => 
            new Product(productId, "Sugar", new Price(10, Currency.Euro), UnitType.perKg, 100));

            var ex2 = Assert.Throws<ArgumentNullException>(() =>
            new Product(1, productName, new Price(10, Currency.Euro), UnitType.perKg, 100));

            var ex3 = Assert.Throws<ArgumentOutOfRangeException>(() =>
            new Product(1, "Sugar", new Price(10, Currency.Euro), UnitType.perKg, amountInStock));

            Assert.Contains("Id is Invalid", ex.Message);
            
            Assert.Contains("Name is Invalid", ex2.Message);
            
            Assert.Contains("AmountInStock is Invalid", ex3.Message);
            
        }


        [Fact]
        public void UseProduct_Reduces_AmountInStock()
        {
            //Arrange
            Product product = new Product(1, "Sugar", new Price(10, Currency.Euro), UnitType.perKg,100);

            //Act
            product.UseProduct(20);

            //Assert
            Assert.Equal(80, product.AmountInStock);
            Assert.False(product.IsBelowStockThreshold);

        }
        [Fact]
        public void UseProduct_ItemsHigherThanStock_NoChangetoStock()
        {
            //Arrange
            Product product = new Product(1, "Sugar", new Price(10, Currency.Euro), UnitType.perKg,100);

            //Act
            product.UseProduct(120);

            //Assert
            Assert.Equal(100, product.AmountInStock);
            Assert.False(product.IsBelowStockThreshold);

        }
        [Fact]
        public void UseProduct_ItemsTheSameAsAmountInStock_ShouldAmoutInStockEqualZero()
        {
            //Arrange
            Product product = new Product(1, "Sugar", new Price(10, Currency.Euro), UnitType.perKg, 100);

            //Act
            product.UseProduct(100);

            //Assert
            Assert.Equal(0, product.AmountInStock);
            Assert.True(product.IsBelowStockThreshold);
        }
        [Fact]
        public void UseProduct_ItemsEqualZero_ShouldNoChangeInAmoutInStock()
        {
            //Arrange
            Product product = new Product(1, "Sugar", new Price(10, Currency.Euro), UnitType.perKg, 100);

            //Act
            var result= product.UseProduct(0);

            //Assert
            Assert.Equal(100, product.AmountInStock);
            Assert.False(product.IsBelowStockThreshold);
            Assert.False(result);
          
        }
        [Fact]
        public void UseProduct_Reduces_AmountInStock_StockBelowThresholdShouldBeTrue()
        {
            //Arrange
            Product product = new Product(1, "Sugar", new Price(10, Currency.Euro), UnitType.perKg, 100);

            //Act
            product.UseProduct(99);
                
            //Assert
            Assert.True(product.IsBelowStockThreshold);
        }


        [Fact]
        public void IncreaseStock_AddValueForAmountInStockSmallerThanMaxItemInStockAndAmountInStockEqualMaxItemInStock_ShouldResultTheSameAsValueForMaxItemInStock()
        {
            //Arrange
            Product product = new Product(1, "Sugar", new Price(10, Currency.Euro), UnitType.perKg, 100);

            //Act
            product.IncreaseStock(20);         

            //Assert
            Assert.Equal(100,product.AmountInStock);
        }
        [Fact]
        public void IncreaseStock_AddValueForAmountInStockBigeerThanMaxItemInStock_ShouldResultEqualMaxAmountInStock()
        {
            //Arrange
            Product product = new Product(1, "Sugar", new Price(10, Currency.Euro), UnitType.perKg);

            //Act
            product.IncreaseStock(300);

            //Assert
            Assert.Equal(100, product.AmountInStock);
        }
        [Fact]
        public void IncreaseStock_AddValueForAmountInStockBigeerThanMaxItemInStock_IsBelowStockThresholdShouldBeFalse()
        {
            //Arrange
            Product product = new Product(1, "Sugar", new Price(10, Currency.Euro), UnitType.perKg);

            //Act
            product.IncreaseStock(300);

            //Assert
            Assert.False( product.IsBelowStockThreshold);
        }
        [Fact]
        public void IncreaseStock_AddValueForAmountInStockSmallerThanMaxItemInStock_ShouldResultEqualValueOfIncrease()
        {
            //Arrange
            Product product = new Product(1, "Sugar", new Price(10, Currency.Euro), UnitType.perKg);

            //Act
            product.IncreaseStock(50);

            //Assert
            Assert.Equal(50, product.AmountInStock);
        }
        [Fact]
        public void IncreaseStock_AddValueForAmountInStockSmallerThanMaxItemInStock_ShouldResultEqualValueOfMaxAmount()
        {
            //Arrange
            Product product = new Product(1, "Sugar", new Price(10, Currency.Euro), UnitType.perKg,80);

            //Act
            product.IncreaseStock(50);

            //Assert
            Assert.Equal(100, product.AmountInStock);
        }

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