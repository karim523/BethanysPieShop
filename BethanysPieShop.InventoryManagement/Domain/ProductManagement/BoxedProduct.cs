//using BethanysPieShop.InventoryManagement.Contracts;
//using BethanysPieShop.InventoryManagement.Domain.General;
//using System.Text;

//namespace BethanysPieShop.InventoryManagement.Domain.ProductManagement
//{
//    public class BoxedProduct : Product, ISaveable, ILoggable,ICloneable
//    {
//        private int amountPerBox;
//        public int AmountPerBox
//        {
//            get { return amountPerBox; }
//            set { amountPerBox = value; }
//        }

//        public BoxedProduct(int id, string name, string? description, Price price,
//            int maxAccountInStock, int amountPerBox) : base(id, name, description, price, UnitType.perBox, maxAccountInStock)
//        {
//            AmountPerBox = amountPerBox;
//        }

//        public override void UseProduct(int items)
//        {
//            int smallestMultiple = 0;
//            int batchSize;
//            while (true)
//            {
//                smallestMultiple++;
//                if (smallestMultiple * AmountPerBox > items)
//                {
//                    batchSize = smallestMultiple * AmountPerBox;
//                    break;
//                }
//            }
//            base.UseProduct(batchSize);
//        }
//        public override void IncreaseStock()
//        {
//            AmountInStock += AmountPerBox;
//        }
//        public override void IncreaseStock(int amount)
//        {
//            int newStock= AmountInStock+amount*AmountPerBox;
//            if(newStock <= maxItemInStock)
//            {
//                AmountInStock += amount * AmountPerBox;                
//            }
//            else
//            {
//                AmountInStock =maxItemInStock;
//                Log($"{CreateSimpleProductRepresentation} stock overflow." +
//                    $" {newStock - AmountInStock} item(s) order that could not be stored.");
//            }
//            if(AmountInStock > StockThreshold)
//            {
//                IsBelowStockThreshold = false;
//            }
//        }
//        public override string DisplayDetailsFull()
//        {
//            StringBuilder sb = new();
//            sb.Append("Boxed Product \n");
//            sb.Append($"{Id} {Name} \n{Description}\n{Price}\n{AmountInStock} item(s) in stock ");

//            if (IsBelowStockThreshold)
//            {
//                sb.Append($"\n!! STOCK LOW!!");
//            }
//            return sb.ToString();

//        }

//        public string ConvertToStringForSaving()
//        {
//            return $"{Id};{Name};{Description};{maxItemInStock};{Price.ItemPrice};" +
//                $"{(int)Price.Currency};{(int)UnitType};1;{amountPerBox};";
//        }

//        public  object Clone()
//        {
//            return new BoxedProduct(0, this.Name,this.Description,new Price
//            { ItemPrice=this.Price.ItemPrice,Currency=this.Price.Currency},this.maxItemInStock,this.AmountPerBox);
//        }
//        void ILoggable.Log(string message)
//        {
//            Console.WriteLine(message); 
//        }
//        //public void UseBoxedProduct(int items)
//        //{
//        //    DecreaseStock(0, "test");
//        //    int smallestMultiple = 0;
//        //    int batchSize;
//        //    while (true)
//        //    {
//        //        smallestMultiple++;
//        //        if (smallestMultiple * AmountPerBox > items)
//        //        {
//        //            batchSize = smallestMultiple * AmountPerBox;
//        //            break;
//        //        }
//        //    }
//        //    UseProduct(batchSize);
//        //}
//    }
//}
