using Application.Products.Dtos;
using Application.Suppliers.Dtos;
using BethanysPieShop.InventoryManagement;
using BethanysPieShop.InventoryManagement.Domain.General;
using BethanysPieShop.InventoryManagement.Domain.ProductManagement;
using BethanysPieShop.InventoryManagement.IRepository;

namespace Application.Products
{
    public class ProductsService : IProductsService
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        public ProductsService(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<List<ProductDetailsDto>> GetAll()
        {
            var products = await _productRepository.GetAll();

            List<ProductDetailsDto> ProductsDto = products.Select(product => new ProductDetailsDto
            {
                Id = product.Id,
                Name = product.Name,
                UnitType = product.UnitType,
                Currency = product.Price.Currency,
                Description = product.Description,
                ItemPrice = product.Price.ItemPrice,
                AmountInStock = product.AmountInStock,
                MaxAmountInStock = product.MaxAmountInStock,
                MinAmountInStock = product.MinAmountInStock,
                IsBelowStockThreshold = product.IsBelowStockThreshold
            }).ToList();
            return ProductsDto;
        }
        public async Task<AddProductOutputDto> CreateProduct(AddProductInputDto input)
        {
            var priceDto =  Price.Create(input.ItemPrice, input.Currency);
            if (priceDto.Object==null) 
            {
                return new AddProductOutputDto()
                {
                    Erorrs = priceDto.Errors.ToList()
                };
            }

            var createResult =  Product.Create
                (input.Name, priceDto.Object, input.UnitType, input.MaxAmountInStock, input.MinAmountInStock, input.AmountInStock);
           
            createResult.Object.SetDescription(input.Description);
          
            if (createResult.Object==null )
            {
                return new AddProductOutputDto()
                {
                    Erorrs = createResult.Errors.ToList()
                };
            }
            createResult.Object.UpdateLowStock();
          await _productRepository.CreateProduct(createResult.Object);

            await _unitOfWork.Commit();

            return new AddProductOutputDto
            {
                ProductId = createResult.Object.Id
            };
        }
        public async Task<GetProductIsBelowStockThresholdOutputDto> GetBelowStockThresholdProduct()
        {
           var product=await _productRepository.GetProductIsBelowStockThreshold();
            if (product == null)
            {
                return new GetProductIsBelowStockThresholdOutputDto()
                {
                    Errors = new List<string>()
                    {
                        "This Product is not Exist"
                    }
                };
            }
            return new GetProductIsBelowStockThresholdOutputDto
            {
                productDto = new ProductDetailDto
                {
                    Name = product.Name,
                    Id = product.Id,
                    IsBelowStockThreshold = product.IsBelowStockThreshold,
                    AmountInStock = product.AmountInStock,
                    MaxAmountInStock = product.MaxAmountInStock,
                    MinAmountInStock = product.MinAmountInStock
                }
            };


        }
    }
}