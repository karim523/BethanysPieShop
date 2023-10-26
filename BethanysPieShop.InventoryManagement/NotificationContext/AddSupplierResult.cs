using BethanysPieShop.InventoryManagement.Domain.SupplierManagement;

namespace BethanysPieShop.InventoryManagement.NotificationContext
{
    public class AddSupplierResult
    {
        public List<string> Errors { get; set; }
        public Supplier Supplier { get; set; }
    }
}