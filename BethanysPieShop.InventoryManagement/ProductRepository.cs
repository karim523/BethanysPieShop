using BethanysPieShop.InventoryManagement.Contracts;
using BethanysPieShop.InventoryManagement.Domain.General;
using BethanysPieShop.InventoryManagement.Domain.ProductManagement;
using System.Text;

namespace BethanysPieShop.InventoryManagement
{
    internal class ProductRepository
    {
        private string directory = @"D:\data/BethanysPieShop\";
        private string productFileName = "products.txt";

        private void CheckForExistingProductFile()
        {
            string path = $"{directory} {productFileName}";
            bool existingFileFound = File.Exists(path);
            if (!existingFileFound) 
            {
                if(!Directory.Exists(path))                 
                    Directory.CreateDirectory(directory);

                    using FileStream fs=File.Create(path);                
            }            
        }
        public List<Product> LoadProductsFormFile()
        {
            List<Product> products = new List<Product>();
            string path =$"{ directory }{productFileName}";
            try
            {
                CheckForExistingProductFile();
                string[] productsAsString = File.ReadAllLines(path);
                for (int i = 0; i < productsAsString.Length; i++)
                {
                    string[] productsSplits = productsAsString[i].Split(',') ;
                    bool success = int.TryParse(productsSplits[0], out int productId);
                    if (!success)
                    {
                        productId = 0;
                    }
                    string name = productsSplits[1];
                    string description= productsSplits[2];

                    success = int.TryParse(productsSplits[3], out int maxItemInStock);
                    if (!success)
                    {
                        maxItemInStock = 100;
                    }
                    success = int.TryParse(productsSplits[4], out int itemPrice);
                    if (!success)
                    {
                        itemPrice = 0;
                    }
                    success = Enum.TryParse(productsSplits[5], out Currency currency);
                    if (!success)
                    {
                        currency = Currency.Dollar;
                    }
                    success = Enum.TryParse(productsSplits[6], out UnitType unitType);
                    if (!success)
                    {
                        unitType = UnitType.PerItem;
                    }
                    Product product = new Product(productId, name, new Price(itemPrice, currency),
                     unitType);
                    products.Add(product);
                }
            }
            catch(IndexOutOfRangeException iex)
            {
                Console.ForegroundColor =ConsoleColor.Red ;
                Console.WriteLine("Something went wrong parsing the file , please check the data!");
                Console.WriteLine(iex.Message);
            }
            catch (FileNotFoundException fnfex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("The file could not be found!");
                Console.WriteLine(fnfex.Message);
                Console.WriteLine(fnfex.StackTrace);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Something went wrong while loading the file!");
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.ResetColor();
            }
            return products;
        } 
        public void SaveToFile(List<ISaveable> saveables)
        {
            StringBuilder sb= new StringBuilder();
            string path = $"{directory}{productFileName}";

            foreach (var item in saveables)
            {
                sb.Append(item.ConvertToStringForSaving());
                sb.Append(Environment.NewLine);
            }
            File.WriteAllText(path, sb.ToString());

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Saved items successfully");
            Console.ResetColor ();
        }
    }
}
