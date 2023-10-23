//using BethanysPieShop.InventoryManagement.Contracts;
//using BethanysPieShop.InventoryManagement.Domain.General;

//namespace BethanysPieShop.InventoryManagement.Domain.ProductManagement
//{
//    public class RegularProduct : Product, ISaveable
//    {
//        public RegularProduct(int id, string name, string? description, Price price, 
//            UnitType unitType, int maxAccountInStock) : base(id, name, description, price, unitType, maxAccountInStock)
//        {
//        }
//        public override void IncreaseStock()
//        {
//            AmountInStock++;
//        }
//        public string ConvertToStringForSaving()
//        {
//            return $"{Id};{Name};{Description};{maxItemInStock};{Price.ItemPrice};" +
//                $"{(int)Price.Currency};{(int)UnitType};4;";
//        }

        
//    }
//}
