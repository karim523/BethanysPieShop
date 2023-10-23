using Application.Products;
using Application.Products.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace BethanysPieShopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService _productsService;

        public ProductsController(IProductsService productsService)
        {
            _productsService = productsService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(AddProductInputDto input)
        {
            var output= await  _productsService.CreateProduct(input);
            return Ok(output);
        }
        [HttpGet("GetAllProducts")]
        public async Task<IActionResult> GetAll()
        {
            var output = await _productsService.GetAll();
            return Ok(output);
        }
        [HttpGet("GetBelowStockThresholdProduct")]
        public async Task<IActionResult> GetBelowStockThresholdProduct()
        {
            var output = await _productsService.GetBelowStockThresholdProduct();
            return Ok(output);
        }
    }
}