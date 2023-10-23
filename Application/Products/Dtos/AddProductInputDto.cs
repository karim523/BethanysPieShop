using BethanysPieShop.InventoryManagement.Domain.General;
using BethanysPieShop.InventoryManagement.Domain.ProductManagement;

namespace Application.Products.Dtos
{
    public class AddProductInputDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public int AmountInStock { get; set; }
        public int MaxAmountInStock { get; set; }
        public int MinAmountInStock { get; set; }
        public UnitType UnitType { get; set; }
        public double ItemPrice { get; set; }
        public Currency Currency { get; set; }
    }
}
