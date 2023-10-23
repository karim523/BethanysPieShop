using BethanysPieShop.InventoryManagement.Domain.SupplierManagement;

namespace BethanysPieShop.InventoryManagement.Tests
{
    public class SupplierTest
    {
        [Fact]
        public void Supplier_ShouldNotNull()
        {
            var supplier = new Supplier("Ahmed", "1stOmerAbnAlKhtab", 01234455555);
           supplier.SetEmail("Ahmed@gmail.com");
            Assert.Equal("Ahmed", supplier.Name);
            
            Assert.Equal("1stOmerAbnAlKhtab", supplier.Adress);
            
            Assert.Equal(01234455555, supplier.Phone);
            
            Assert.Equal("Ahmed@gmail.com", supplier.Email);
            
            Assert.NotNull(supplier);
        }
        [Fact]
        public void Supplier_WhenValuesAreInvalid_ShouldReturnError()
        {
            var name = "";
            var address = "";
            var email = "";
            var phone =0;
           
            var supplier1 = new Supplier(name, "1stOmerAbnAlKhtab", 01234455555 );
            
            supplier1.SetEmail("Ahmed@gmail.com");
            
            var supplier2 = new Supplier("Ahmed", address, 01234455555);
            
            supplier2.SetEmail("Ahmed@gmail.com");
            
            var supplier3 = new Supplier("Ahmed", "1stOmerAbnAlKhtab",phone);
            
            supplier3.SetEmail("Ahmed@gmail.com");
            
            var supplier4 = new Supplier("Ahmed", "1stOmerAbnAlKhtab", 01234455555);
            
            supplier4.SetEmail(email);
      
            Assert.False( supplier1.SetName(name));

            Assert.False(supplier2.SetAdress(address));

            Assert.False(supplier3.SetPhone(phone));

            Assert.True(supplier4.SetEmail(email));

        }
        [Fact]
        public void UpdateSupplier_ShouldReturnTrue()
        {
            var supplier = new Supplier("Ahmed", "1stOmerAbnAlKhtab", 0123445555);
            supplier.SetEmail("Ahmed@gmail.com");
            supplier.UpdateSupplier("Karim", "3stOmerAbnAlKhtab",0124444444, "Karim@gmail.com");
            
            Assert.True(supplier.UpdateSupplier("Karim", "3stOmerAbnAlKhtab", 0124444444, "Karim@gmail.com"));

            Assert.Equal("Karim", supplier.Name);
            
            Assert.Equal("3stOmerAbnAlKhtab", supplier.Adress);
            
            Assert.Equal(0124444444, supplier.Phone);
            
            Assert.Equal("Karim@gmail.com", supplier.Email);
        }
        [Fact]
        public void UpdateSupplier2()
        {
            var supplier = new Supplier("Ahmed", "1stOmerAbnAlKhtab", 01234455555);
            supplier.SetEmail("Ahmed@gmail.com");

            supplier.UpdateSupplier("", "", -1111, "");

            Assert.False(supplier.UpdateSupplier("", "", -1111, ""));
           
            Assert.False(supplier.UpdateSupplier("", "3stOmerAbnAlKhtab", 0124444444, "Karim@gmail.com"));
            
            Assert.False(supplier.UpdateSupplier("Karim", "", 0124444444, "Karim@gmail.com"));
            
            Assert.False(supplier.UpdateSupplier("Karim", "3stOmerAbnAlKhtab", -1111111, "Karim@gmail.com"));
            
            Assert.False(supplier.UpdateSupplier("Karim", "3stOmerAbnAlKhtab", 0124444444, ""));                   
        }
    }
}
