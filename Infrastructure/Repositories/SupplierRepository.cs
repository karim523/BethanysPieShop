using BethanysPieShop.InventoryManagement.Domain.SupplierManagement;
using BethanysPieShop.InventoryManagement.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly AppDbContext _context;
        public SupplierRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task Create(Supplier supplier)
        {
           await _context.AddAsync(supplier);
        }

        public void Delete(Supplier supplier)
        {
            _context.Remove(supplier);
        }

        public async Task<Supplier?> Get(int supplierId)
        {
            return await _context.Suppliers.FirstOrDefaultAsync(s => s.Id == supplierId);
        }

        public async Task<List<Supplier>> GetAll()
        {
           return await _context.Suppliers.ToListAsync();
        }
      
    }
}
