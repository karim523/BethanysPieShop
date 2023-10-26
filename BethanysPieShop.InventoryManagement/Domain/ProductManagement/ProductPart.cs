namespace BethanysPieShop.InventoryManagement.Domain.ProductManagement
{
    public partial class Product
    {
        public static int StockThreshold = 5;

        public void UpdateLowStock()
        {
            if (AmountInStock < MinAmountInStock)
            {
                IsBelowStockThreshold = true;
            }
            else
            {
                IsBelowStockThreshold = false;
            }
        }
        protected void Log(string massage)
        {
            Console.WriteLine(massage);
        }
        protected string CreateSimpleProductRepresentation()
        {
            return $"Product {Id} ({Name})";
        }
    }
}
