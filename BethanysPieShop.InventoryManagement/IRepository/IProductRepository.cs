using BethanysPieShop.InventoryManagement.Domain.ProductManagement;

namespace BethanysPieShop.InventoryManagement.IRepository
{
    public interface IProductRepository
    {
        Task CreateProduct(Product product);
        Task<Product?> Get(int productId);
        Task<List<Product>> GetAll();
        Task<Product?> GetProductIsBelowStockThreshold();
    }
}
