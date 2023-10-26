using BethanysPieShop.InventoryManagement.Domain.OrderManagment;

namespace BethanysPieShop.InventoryManagement.IRepository
{
    public interface IOrderPurchaseRepository
    {
        Task CreateOrder(OrderPurchase orderPurchase);
        Task<List<OrderPurchase>> GetAll();
        Task<bool> IsSupplierHasPurchaseOrder(int supplierId);
        Task<int> CountSupplierHasPurchaseOrder(int supplierId);

    }
}
