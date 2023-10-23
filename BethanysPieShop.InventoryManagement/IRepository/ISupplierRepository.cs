
using BethanysPieShop.InventoryManagement.Domain.SupplierManagement;

namespace BethanysPieShop.InventoryManagement.IRepository
{
    public interface ISupplierRepository
    {
        Task Create(Supplier supplier);
        Task<List<Supplier>> GetAll();
        Task<Supplier?> Get(int supplierId);
        void Delete(Supplier supplier);
    }
}
