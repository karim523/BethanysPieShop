using BethanysPieShop.InventoryManagement.Domain.ProductManagement;

namespace BethanysPieShop.InventoryManagement.Domain.OrderManagment
{
    public class OrderItem
    {            

        public int Id{ get; private set; }
        public int AmountOrdered {get;private set;}
        public Product Product { get; private set; }
        private OrderItem()
        {
            
        }
        public OrderItem( int amountOrdered, Product ordedProduct)
        {
            SetAmountOrdered(amountOrdered);
            Product = ordedProduct;
        }
        public override string ToString()
        {
            return $"Product Id:{Product.Id} - Name: {Product.Name} - Amount Ordered: {AmountOrdered}";
        }
        private void SetAmountOrdered( int amountOrdered )
        {
            if (amountOrdered <= 0) throw new ArgumentOutOfRangeException("AmountOrdered is Invalid");

           AmountOrdered = amountOrdered;
        }
    }
}
