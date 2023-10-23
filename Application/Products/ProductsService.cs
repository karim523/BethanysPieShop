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

        public async Task<AddProductOutputDto> CreateProduct(AddProductInputDto input)
        {
            var priceDto = new Price(input.ItemPrice, input.Currency);
            if (priceDto.IsInvalid) 
            {
                return new AddProductOutputDto()
                {
                    Erorrs = priceDto.Notifications
                };
            }

            var product = new Product
                (input.Name, priceDto, input.UnitType, input.MaxAmountInStock, input.MinAmountInStock, input.AmountInStock, input.Description);
            
            if (product.IsInvalid )
            {
                return new AddProductOutputDto()
                {
                    Erorrs = product.Notifications
                };
            }

            await _productRepository.CreateProduct(product);

            await _unitOfWork.Commit();

            return new AddProductOutputDto
            {
                ProductId = product.Id
            };
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