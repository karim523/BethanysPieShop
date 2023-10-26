using BethanysPieShop.InventoryManagement.Domain.SupplierManagement;
using System.Net;
using System.Numerics;

namespace BethanysPieShop.InventoryManagement.Tests
{
    public class SupplierTest
    {
        [Fact]
        public void Supplier_ShouldNotNull()
        {
            var supplier = Supplier.Create("Ahmed", "1stOmerAbnAlKhtab", 01234455555);
            supplier.Object.SetEmail("Ahmed@gmail.com");

            Assert.NotNull(supplier.Object);

            Assert.Equal("Ahmed", supplier.Object.Name);

            Assert.Equal("1stOmerAbnAlKhtab", supplier.Object.Adress);

            Assert.Equal(01234455555, supplier.Object.Phone);

            Assert.Equal("Ahmed@gmail.com", supplier.Object.Email);

        }
        [Fact]
        public void Supplier_WhenAddInvalidName_ShouldReturnNull()
        {
            var supplier1 = Supplier.Create("", "1stOmerAbnAlKhtab", 01234455555);
            Assert.Null(supplier1.Object);
        }
        [Fact]
        public void Supplier_WhenAddInvalidAddress_ShouldReturnNull()
        {
            var supplier2 = Supplier.Create("Ahmed", "", 01234455555);
            Assert.Null(supplier2.Object);
        }
        [Fact]
        public void Supplier_WhenAddInvalidPhone_ShouldReturnNull()
        {
            var supplier3 = Supplier.Create("Ahmed", "1stOmerAbnAlKhtab", 0);
            Assert.Null(supplier3.Object);
        }
        [Fact]
        public void Supplier_WhenAddInvalidEmail_ShouldReturnNotNull()
        {
            var supplier4 = Supplier.Create("Ahmed", "1stOmerAbnAlKhtab", 01234455555);

            supplier4.Object.SetEmail("vvv@gmail.com");

            Assert.NotNull(supplier4.Object);
        }
        [Fact]
        public void UpdateSupplier_ShouldReturnTrue()
        {
            var supplier = Supplier.Create("Ahmed", "1stOmerAbnAlKhtab", 0123445555);

            supplier.Object.SetEmail("Ahmed@gmail.com");

            var result = supplier.Object.UpdateSupplier("Karim", "3stOmerAbnAlKhtab", 0124444444, "Karim@gmail.com");

            Assert.True(result.IsSucces);

            Assert.Equal("Karim", supplier.Object.Name);

            Assert.Equal("3stOmerAbnAlKhtab", supplier.Object.Adress);

            Assert.Equal(0124444444, supplier.Object.Phone);

        }
        [Fact]
        public void UpdateSupplier_WhenAddInvalidName_ShouldRetuenFalse()
        {
            var supplier = Supplier.Create("Ahmed", "1stOmerAbnAlKhtab", 01234455555);
            supplier.Object.SetEmail("Ahmed@gmail.com");

            Assert.False(supplier.Object.UpdateSupplier("", "3stOmerAbnAlKhtab", 0124444444, "Karim@gmail.com").IsSucces);
        }
        [Fact]
        public void UpdateSupplier_WhenAddInvalidAddress_ShouldRetuenFalse()
        {
            var supplier = Supplier.Create("Ahmed", "1stOmerAbnAlKhtab", 01234455555);
            supplier.Object.SetEmail("Ahmed@gmail.com");
            
            Assert.False(supplier.Object.UpdateSupplier("Karim", "", 0124444444, "Karim@gmail.com").IsSucces);
        }
        [Fact]
        public void UpdateSupplier_WhenAddInvalidPhone_ShouldRetuenFalse()
        {
            var supplier = Supplier.Create("Ahmed", "1stOmerAbnAlKhtab", 01234455555);
            supplier.Object.SetEmail("Ahmed@gmail.com");
       
            Assert.False(supplier.Object.UpdateSupplier("Karim", "3stOmerAbnAlKhtab", -1111111, "Karim@gmail.com").IsSucces);
        }
        [Fact]
        public void UpdateSupplier_WhenAddInvalidValues_ShouldRetuenFalse()
        {
            var supplier = Supplier.Create("Ahmed", "1stOmerAbnAlKhtab", 01234455555);
            supplier.Object.SetEmail("Ahmed@gmail.com");

            Assert.False(supplier.Object.UpdateSupplier("", "", -1111, "").IsSucces);
        }
    }
}