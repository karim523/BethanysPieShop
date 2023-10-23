//using BethanysPieShop.InventoryManagement.Contracts;
//using BethanysPieShop.InventoryManagement.Domain.General;

//namespace BethanysPieShop.InventoryManagement.Domain.ProductManagement
//{
//    public class BulkProduct : Product,ISaveable
//    {
//        public BulkProduct(int id, string name, string? description, Price price, 
//            int maxAccountInStock) : base(id, name, description, price, UnitType.perKg, maxAccountInStock)
//        {}

//        public override void IncreaseStock()
//        {
//           AmountInStock++;
//        }
//        public string ConvertToStringForSaving()
//        {
//            return $"{Id};{Name};{Description};{maxItemInStock};{Price.ItemPrice};" +
//               $"{(int)Price.Currency};{(int)UnitType};3;";
//        }
//    }
//}
