using BethanysPieShop.InventoryManagement.Domain.ProductManagement;
using System.Text;

namespace BethanysPieShop.InventoryManagement.Domain.OrderManagment
{
    public class OrderSale
    {
        private readonly List<OrderItem> _items;
        //private readonly List<Product> _products;
        public OrderSale()
        {
            _items = new List<OrderItem>();
            //_products = new List<Product>();
        }
 
        public int Id { get; private set; }
        public DateTime OrderDate { get; private set; }
        public IReadOnlyList<OrderItem> OrderItems
        {
            get
            {
                return this._items;
            }
        }
        //public IReadOnlyList<Product> Products {
        //    get
        //    {
        //        return this._products;
        //    }
        //}

        public bool Fulfilled { get; set; } = false;
        //public Order()
        //{
        //    Id = new Random().Next(9999999);
        //    int numberOfseconds = new Random().Next(100);
        //    OrderFulfilmentDate = DateTime.Now.AddSeconds(numberOfseconds);

        //    _items = new List<OrderItem>();
        //}
        public string ShowOrderDetails()
        {
            StringBuilder orderDetails = new StringBuilder();

            orderDetails.AppendLine($"Order Id:{Id}");
            orderDetails.AppendLine($"Order fulfilment date :{OrderDate.ToShortTimeString()}");

            if ( _items !=null)
            {
                foreach ( OrderItem item in _items )
                {
                    orderDetails.AppendLine($"{item.Product.Id}. {item.Product.Name}: {item.AmountOrdered }");
                }
            }
            return orderDetails.ToString();
        }

        public bool AddOrder(Product product,int amountOrdered)
        {           
            //var result = product.UseProduct(amountOrdered);
            //if (!result )
            //{
            //    return false;
            //}
            //OrderItem item = new OrderItem() 
            //{
            //    AmountOrdered=amountOrdered,
            //    OrdedProduct= product
            //};
            //_items.Add(item);
            //OrderDate = DateTime.Now;
            //Fulfilled = true;
            return true;
        }
    }
}
