using BethanysPieShop.InventoryManagement.Domain.OrderManagment;
using BethanysPieShop.InventoryManagement.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class OrderPurchaseRepository : IOrderPurchaseRepository
    {
        private readonly AppDbContext _dbContext;

        public OrderPurchaseRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task CreateOrder(OrderPurchase orderPurchase)
        {
           await _dbContext.AddAsync(orderPurchase) ;
        }

        public async Task<List<OrderPurchase>> GetAll()
        {
            return await _dbContext.OrderPurchases.ToListAsync();
        }

        public async Task<bool> IsSupplierHasPurchaseOrder(int supplierId)
        {
            return await _dbContext.OrderPurchases.AnyAsync(o=>o.SupplierId==supplierId);
        }
    }
}
