using BethanysPieShop.InventoryManagement.Domain.General;
using BethanysPieShop.InventoryManagement.NotificationContext;
using System.Text;

namespace BethanysPieShop.InventoryManagement.Domain.ProductManagement
{
    public partial class Product:Notifiable
    {
        private int id;
        private string name = string.Empty;
        private string? description;

        private int maxAmountInStock = 100;
        private int minAmountInStock = 5;

        public int MaxAmountInStock
        { 
            get { return maxAmountInStock; } 
            private set
            {
                if (value <= minAmountInStock) throw new ArgumentOutOfRangeException("MaxAmountInStock is Invalid");
                maxAmountInStock = value;
            }
        }
        public int MinAmountInStock
        {
            get { return minAmountInStock; }
            private set
            {
                if (value >= maxAmountInStock) throw new ArgumentOutOfRangeException("MinAmountInStock is Invalid");
                minAmountInStock = value;
            }
        }
        public int Id 
        {
            get { return id; }
            private set
            {
                if (value <= 0)  throw new ArgumentOutOfRangeException("Id is Invalid");
                id = value;
            }
        }
        public string Name
        {
            get { return name; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value) )
                {
                    throw new ArgumentNullException("Name is Invalid");
                }
                else
                {
                    name = value.Length > 50 ? value[..50] : value;
                }
            }
        }
        public string? Description
        {
            get { return description; }
            set
            {
                if (value == null)
                {
                    description = string.Empty;
                }
                else
                {
                    description = value.Length > 250 ? value[..250] : value;
                }
            }
        }
       
        public UnitType UnitType { get; set; }
        public Price Price { get; set; }
        public int AmountInStock { get; protected set; }
        public bool IsBelowStockThreshold { get; protected set; }

        public Product()
        {

        }
        public Product( string name, Price price,UnitType unitType,int maxAmount, int minAmount, int amountInStock=0, string description = null)
        {
            Description = description;
            SetName(name);
            UnitType = unitType;
            Price= price;
            SetMaxAmountInStock(maxAmount);
            SetMinAmountInStock( minAmount);
            SetAmountInStock( amountInStock);
            UpdateLowStock();
        }
        public Product(int id,string name, Price price, UnitType unitType, int amountInStock = 0)
        {
            Id = id;
            Name = name;
            UnitType = unitType;
            Price = price;
            SetAmountInStock(amountInStock);
            UpdateLowStock();
        }

        public virtual bool UseProduct(int items)
        {
            if (items <= 0)
            {
                Log($"Items is Invaild");
                UpdateLowStock();
                return false;
            }
            
            else if (items <= AmountInStock)
            {
                AmountInStock -= items;

                UpdateLowStock();

                Log($"Amount in Stock updated. Now {AmountInStock} items in stock.");
                return true;
            }
            else
            {
                Log($"Not enough items on Stock for {CreateSimpleProductRepresentation()}. {AmountInStock} available but {items} is requested.");
                return false;
            }
          
        }     
        public virtual bool IncreaseStock(int amount)
        {
            if (amount<=0)
            {
                Log("Amount is Invalid");
                return false;
            }
            int newStock = AmountInStock + amount;
          
            if (newStock <= MaxAmountInStock)
            {
                AmountInStock += amount;
                return true;
            }
            else
            {
                AmountInStock = MaxAmountInStock ;
                Log($"{CreateSimpleProductRepresentation} stock overflow. {newStock - AmountInStock}" +
                    $"item(s) ordered that could not be stored.");
            }
            UpdateLowStock();
            return true;
        }
      
        public string DisplayDetailsShort()
        {
            return $"{Id}.{Name} \n {AmountInStock} items in stock";
        }
        public virtual string DisplayDetailsFull()
        {
            StringBuilder sb = new();
            sb.Append($"{Id} {Name} \n{Description}\n{Price}\n{AmountInStock} item(s) in stock ");

            if (IsBelowStockThreshold)
            {
                sb.Append($"\n!! STOCK LOW!!");
            }
            return sb.ToString();
        }

        private bool SetAmountInStock(int amountInStock)
        {
            if (amountInStock < 0) 
            {
                AddNotification(new Notification("AmountInStock is Invalid"));
                return false;
            }
            AmountInStock= amountInStock;
            return true;
        }
        private bool SetMaxAmountInStock(int maxAmountInStock)
        {
            if (maxAmountInStock < 0 || maxAmountInStock <= MinAmountInStock) 
            {
                AddNotification(new Notification("MaxAmountInStock is Invalid"));
                return false;
            }
            MaxAmountInStock = maxAmountInStock;
            return true;
        } 
        private bool SetMinAmountInStock(int minAmountInStock)
        {
            if (minAmountInStock < 0 || minAmountInStock>=MaxAmountInStock)
            {
                AddNotification(new Notification("MinAmountInStock is Invalid"));
                return false;
            }
            MinAmountInStock = minAmountInStock;
            return true;
        }
        private bool SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name) || name.Length < 3)
            {
                AddNotification(new Notification("Name is Invalid"));
                return false;
            }
            Name = name;
            return true;
        }
    }

}
