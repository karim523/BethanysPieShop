using BethanysPieShop.InventoryManagement.Contracts;
using BethanysPieShop.InventoryManagement.Domain.General;
using BethanysPieShop.InventoryManagement.Domain.OrderManagment;
using BethanysPieShop.InventoryManagement.Domain.ProductManagement;

namespace BethanysPieShop.InventoryManagement
{
    internal class Utilities
    {
        private static List<Product> inventory = new();
        private static List<OrderSale> orders = new();

        internal static void InitializeStock()
        {
        //    var bp = new BoxedProduct(6, "Eggs", "Lorem ipsum",
        //            new Price { ItemPrice = 10, Currency = Currency.Euro }, 100, 6);
            
        //    bp.IncreaseStock(100);
        //    bp.UseProduct(10);
            
            ProductRepository productRepository = new ProductRepository();
            inventory =productRepository.LoadProductsFormFile();
            Console.ForegroundColor=ConsoleColor.Green;
            Console.WriteLine($"Loaded {inventory.Count} products!");
            Console.WriteLine("press enter to continue!");
            Console.ResetColor();
            Console.ReadLine();
        }
        internal static void ShowMainMenu()
        {
            Console.ResetColor();
            Console.Clear();
            Console.WriteLine("*************");
            Console.WriteLine("* Select an action");
            Console.WriteLine("*************");

            Console.WriteLine("1: Inventory management");
            Console.WriteLine("2: Order management");
            Console.WriteLine("3: Settings");
            Console.WriteLine("4: Save all data");
            Console.WriteLine("0: Close application");

            Console.WriteLine("Your Selection");

            string? userSelection = Console.ReadLine();
            switch (userSelection)
            {
                case "1":
                    ShowInventoryManagementMenu();
                    break;
                case "2":
                    ShowOrderManagementMenu();
                    break;
                case "3":
                    ShowSettingsMenu();
                    break;
                case "4":
                    SaveAllData();
                    break;
                case "0":
                    break;
                default:
                    Console.WriteLine("Invalid selection. please try again. ");
                    break;

            }
        }
        private static void ShowInventoryManagementMenu()
        {
            string? userSelection;
            do
            {
                Console.ResetColor();
                Console.Clear();
                Console.WriteLine("*************");
                Console.WriteLine("* Inventory management *");
                Console.WriteLine("*************");

                ShowAllProductsOverview();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("What do you want to do?");
                Console.ResetColor();

                Console.WriteLine("1: View details of product");
                Console.WriteLine("2: Add new product");
                Console.WriteLine("3: Clone product");
                Console.WriteLine("4: View products with low stock");
                Console.WriteLine("0: Back to main menu");

                Console.WriteLine("Your Selection");

                userSelection = Console.ReadLine();

                switch (userSelection)
                {
                    case "1":
                        ShowDetailsAndUseProduct();
                        break;
                    case "2":
                       // ShowCreatNewProduct();
                        break;

                    case "3":
                       ShowCloneExistingProduct();
                        break;

                    case "4":
                        ShowProductsLowStock();
                        break;

                    default:
                        Console.WriteLine("Invalid selection. please try again. ");
                        break;

                }
            }
            while (userSelection != "0");
            ShowMainMenu();
        }
        private static void ShowAllProductsOverview() 
        {
            foreach (var product in inventory)
            {
                Console.WriteLine(product.DisplayDetailsShort());
                Console.WriteLine();
            }
        }
        private static void ShowDetailsAndUseProduct() 
        {
            string? userSelection = string.Empty;

            Console.Write("Enter the Id of product: ");
            string? selectedProductId = Console.ReadLine();

            if (selectedProductId !=null)
            {
                Product? selectedProduct = inventory.Where(p=>p.Id==int.Parse
                (selectedProductId)).FirstOrDefault();

                if (selectedProduct != null)
                {
                    Console.WriteLine(selectedProduct.DisplayDetailsFull());

                    Console.WriteLine("\n What do you want to do ?");
                    Console.WriteLine("1: Use product");
                    Console.WriteLine("0 :Back to inventory overview");
 
                    Console.WriteLine("Your select");
                    userSelection = Console.ReadLine();
                    if (userSelection=="1")
                    {
                        Console.WriteLine("How many products do you want to use?");
                        int amount=int.Parse (Console.ReadLine()??"0");
                        selectedProduct.UseProduct(amount);
                        Console.ReadLine();
                    }
                }
            }
            else
            {
                Console.WriteLine("Non-existing product selected. pleasetry again. ");
            }
        }
        private static void ShowProductsLowStock()
        {
            List<Product> LowStockProducts = inventory.Where(p=>p.IsBelowStockThreshold).ToList();
            if (LowStockProducts.Count>0)
            {
                Console.WriteLine("the following items are low on stock, order more soon!");
                foreach (Product product in LowStockProducts) 
                {
                    Console.WriteLine(product.DisplayDetailsFull());
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("No items are currently low on stock!");
            }
            Console.ReadLine();
        }
        private static void ShowOpenOrderOverview()
        {
            ShowFulfilledOrder();
            if (orders.Count > 0)
            {
                Console.WriteLine("Open orders");
                foreach (OrderSale order in orders) 
                {
                    Console.WriteLine(order.ShowOrderDetails());
                    Console.WriteLine();
                }
            }
            else
            { 
                Console.WriteLine("There are no open orders");
            }
            Console.ReadLine();
        }
        private static void ShowChangeStockThreshold()
        {
            Console.WriteLine($"Enter the new stock threshold (current value :" +
                $"{Product.StockThreshold}). This applies to all products!");
            Console.Write("New value: ");
            int newValue=int.Parse( Console.ReadLine()??"0" );
            Product.StockThreshold = newValue;
            Console.WriteLine($"New stock threshold set to {Product.StockThreshold}");
            foreach (var product in inventory)
            {
               // product.UpdateLowStock();
            }
            Console.ReadLine();
        }
        private static void ShowFulfilledOrder()
        {
            Console.WriteLine("Checking Fulfilled Order");
            foreach (var order in orders)
            {
                if(!order.Fulfilled && order.OrderDate < DateTime.Now )
                {
                    foreach (var orderItem in order.OrderItems)
                    {
                        Product? selectedProduct=inventory
                            .Where(p=>p.Id == orderItem.Product.Id)
                            .FirstOrDefault();

                        if(selectedProduct != null)
                        {
                            selectedProduct.IncreaseStock(orderItem.AmountOrdered);
                        }
                        order.Fulfilled = true;
                    }
                }
                orders.RemoveAll (o=>o.Fulfilled);

                Console.WriteLine("Fulfilled orders checked ");
            }
        }
        private static void ShowAddNewOrder() 
        {
            OrderSale newOrder=new OrderSale();
            string? selectedProductId = string.Empty;

            Console.ForegroundColor=ConsoleColor.Yellow;
            Console.WriteLine("Creating new order");
            Console.ResetColor();
            do
            {
                ShowAllProductsOverview();
                Console.WriteLine("Which product do you want to order?(enter 0 to stop" +
                    "adding new products to the order) ");
                Console.WriteLine("Enter the Id of product: ");
                selectedProductId= Console.ReadLine();
                if (selectedProductId != "0")
                {
                    Product? selectedProduct=inventory.Where(p=>p.Id==int.Parse
                    (selectedProductId)).FirstOrDefault();
                    if (selectedProduct == null)
                    {
                        Console.Write(" How many do you want to order?:");
                        int amountOrdered = int.Parse(Console.ReadLine() ?? "0");

                        OrderItem orderItem = new OrderItem(amountOrdered,selectedProduct);            
                    }
                }
            } while (selectedProductId != "0");
            ShowMainMenu();
        }
        private static void SaveAllData()
        {
            ProductRepository productRepository = new ProductRepository();
            List<ISaveable>saveables= new List<ISaveable>();
            foreach(var item in inventory)
            {
                if(item is ISaveable)
                {
                    saveables.Add(item as ISaveable);

                }
            }
                productRepository.SaveToFile(saveables);
            Console.ReadLine();
            ShowMainMenu();
        }
        private static void ShowSettingsMenu()
        {
            string? userSelection ;
            do
            {
                Console.ResetColor();
                Console.Clear();
                Console.WriteLine("***********");
                Console.WriteLine("*Setting*");
                Console.WriteLine("***********");

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("What do you want to do ?");
                Console.ResetColor();

                Console.WriteLine("1: Change stock threshold");
                Console.WriteLine("0: Back to main menu");

                Console.WriteLine("Your selection");

                userSelection = Console.ReadLine();

                switch (userSelection)
                {
                    case "1":
                        ShowChangeStockThreshold();
                        break;
                    default:
                        Console.WriteLine("Invalid selection. please try again. ");
                        break;
                }
            } while (userSelection != "0");
            ShowMainMenu();
        }
        private static void ShowOrderManagementMenu()
        {
            string? userSelection =string.Empty; do
            {
                Console.ResetColor();
                Console.Clear();
                Console.WriteLine("***********");
                Console.WriteLine("*Select an action*");
                Console.WriteLine("***********");

                Console.WriteLine("1: Open order orderview");
                Console.WriteLine("2: Add new order");
                Console.WriteLine("0: Back to main menu");

                Console.WriteLine("Your selection:");

                userSelection = Console.ReadLine();

                switch (userSelection)
                {
                    case "1":
                        ShowOpenOrderOverview();
                        break; 
                    case "2":
                        ShowAddNewOrder();
                        break;
                    default:
                        Console.WriteLine("Invalid selection. please try again. ");
                        break;
                }
            }
            while (userSelection != "0");
            ShowMainMenu();

        }
        private static void ShowCloneExistingProduct()
        {
            string? userSelection =string.Empty;
            string? newId=string.Empty;
            Console.WriteLine( "Enter the Id of product to clone :");
            string? selectedProductId =Console.ReadLine();  

            if(selectedProductId != null) 
            {
                Product? selectProduct=inventory.Where(p=>p.Id==int.Parse(selectedProductId)).FirstOrDefault();
                if(selectProduct != null) 
                {
                    Console.Write("Enter the new Id of the cloned product :");
                    newId = Console.ReadLine();
                  //Product? p=selectProduct.Clone() as Product;
                 
                  //  if(p!= null)
                  //  {
                  //      p.Id = int.Parse(newId);
                  //      inventory.Add(p);
                  //  }
                }
            }
        }
    }
}
