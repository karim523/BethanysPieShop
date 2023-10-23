using Application.Products.Dtos;

namespace Application.Products
{
    public interface IProductsService
    {
        Task<AddProductOutputDto> CreateProduct(AddProductInputDto input);
        Task<List<ProductDetailsDto>> GetAll();
        Task<GetProductIsBelowStockThresholdOutputDto> GetBelowStockThresholdProduct();
    }
}
