using BethanysPieShop.InventoryManagement.Domain.ProductManagement;
using BethanysPieShop.InventoryManagement.IRepository;
using Microsoft.EntityFrameworkCore;
using System;

namespace Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _dbContext;

        public ProductRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task CreateProduct(Product product)
        {
            await  _dbContext.AddAsync(product);
        }

        public async Task<Product?> Get(int productId)
        {
            return await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == productId);
        }

        public async Task<List<Product>> GetAll()
        {
            return await _dbContext.Products.Include(p=>p.Price).ToListAsync();
        }
        public async Task<Product?> GetProductIsBelowStockThreshold()
        {
            return await _dbContext.Products.FirstOrDefaultAsync(x => x.IsBelowStockThreshold == true);

        }
    }
}
