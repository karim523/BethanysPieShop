using System.Text;

namespace BethanysPieShop.InventoryManagement.Domain.OrderManagment
{
    public class OrderPurchase
    {
        private readonly List<OrderItem> _items;
   
        public int Id { get; private set; }
        public DateTime OrderDate { get; private set; }
        public bool Fulfilled { get;  set; } = false;
        public IReadOnlyList<OrderItem> OrderItems
        {
            get
            {
                return this._items.AsReadOnly();
            }
        }
        public int SupplierId { get; private set; }
        private OrderPurchase()
        {

        }
        public OrderPurchase(int supplierId, DateTime orderDate, List<OrderItem> orderItems)
        {
            if(supplierId <= 0)
            {
                throw new ArgumentException("Supplier Id must not be Zero or Negative");
            }
            if (orderDate > DateTime.Now)
            {
                throw new ArgumentException("Order Date must not be less than Today ");
            }
            if (orderItems == null || !orderItems.Any())
            {
                throw new ArgumentException("Order Items must not be Null");
            }
            SupplierId = supplierId;            
           
            OrderDate = orderDate;        
           
            _items = new List<OrderItem>();

            AddListOrderItems(orderItems);
        }
        public string ShowOrderDetails()
        {
            StringBuilder orderDetails = new StringBuilder();

            orderDetails.AppendLine($"Order Id:{Id}");
            orderDetails.AppendLine($"Order fulfilment date :{OrderDate.ToShortTimeString()}");

          
                foreach (OrderItem item in _items)
                {
                    orderDetails.AppendLine($"{item.Product.Id}. {item.Product.Name}: {item.AmountOrdered}");
                }
            
            return orderDetails.ToString();
        }
        public void AddListOrderItems(List<OrderItem> orderItem)
        {
            foreach (var item in orderItem)
            {
                if (_items.Any(i => i.Product.Id == item.Product.Id || (i.Product.Name == item.Product.Name)))
                {
                   throw new ArgumentException($"The Product Id {item.Product.Id} is Duplicated");
                }
                
                var result = item.Product.IncreaseStock(item.AmountOrdered);

                if (!result)
                {
                    throw new ArgumentException("The Purchase Failed");
                }
                _items.Add(item);
            }
        }
    }
}