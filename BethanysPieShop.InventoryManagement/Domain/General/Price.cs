using BethanysPieShop.InventoryManagement.Domain.SupplierManagement;
using BethanysPieShop.InventoryManagement.NotificationContext;

namespace BethanysPieShop.InventoryManagement.Domain.General
{
    public class Price
    {
        public int Id {  get;private set; } 
        public double ItemPrice {  get;private set; }
        public Currency Currency { get;private set; }
        //public Price(double itemPrice, Currency currency)
        //{
        //    SetItemPrice ( itemPrice);
        //    Currency = currency;
        //}
        private Price()
        {
            
        }
        public static Result<Price> Create(double itemPrice, Currency currency)
        {
            var result = Result<Price>.Create();
            if (itemPrice <= 0)
            {
                result.AddError("ItemPrice is invalid");
            }
            if (result.IsSucces)
            {
                return Result<Price>.Scucsse(new Price
                {
                    ItemPrice = itemPrice,
                    Currency = currency
                });
            }
            return result;
        }
        public override string ToString()
        {
            return $"{ItemPrice} {Currency}";
        }
        private void SetItemPrice(double price)
        {
            if (price <= 0)
            {
                throw new ArgumentException("ItemPrice is invalid");
            }
            ItemPrice = price;
        }
    }
}
