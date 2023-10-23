using Application.OrderPurchases;
using Application.OrderPurchases.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BethanysPieShopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderPurchasesController : ControllerBase
    {
        private readonly IOrderPurchasesService _orderPurchasesService;

        public OrderPurchasesController(IOrderPurchasesService orderPurchasesService)
        {
            _orderPurchasesService = orderPurchasesService;
        }

        [HttpPost]
        public async Task <IActionResult> AddOrder(AddOrderPurchaseInputDto inputDto)
        {
            var output=await _orderPurchasesService.AddOrder(inputDto);
            return Ok(output);
        }
    }
}