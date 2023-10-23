using BethanysPieShop.InventoryManagement.Domain.General;
using BethanysPieShop.InventoryManagement.Domain.ProductManagement;

namespace Application.OrderPurchases.Dtos
{
    public class AddOrderPurchaseInputDto
    {
        public int SupplierId { get; set; }
        public List<OrderItemDto>? OrderItems { get; set; }
        public DateTime OrderDate { get; set; }

    }
    public class OrderItemDto
    {
        public int Amount { get; set; }
        public int ProductId { get; set; }
    }
}
