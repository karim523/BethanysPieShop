using BethanysPieShop.InventoryManagement.NotificationContext;

namespace BethanysPieShop.InventoryManagement.Domain.General
{
    public class Price:Notifiable
    {
        public int Id {  get;private set; } 
        public double ItemPrice {  get;private set; }
        public Currency Currency { get;private set; }
        public Price(double itemPrice, Currency currency)
        {
            SetItemPrice ( itemPrice);
            Currency = currency;
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
