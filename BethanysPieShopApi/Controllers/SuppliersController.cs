using Application.Suppliers;
using Application.Suppliers.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BethanysPieShopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuppliersController : ControllerBase
    {
        private readonly ISuppliersService _suppliersService;

        public SuppliersController(ISuppliersService suppliersService)
        {
            _suppliersService = suppliersService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateSupplier(AddSupplierInputDto inputDto)
        {
            var output= await _suppliersService.AddSupplier(inputDto);
            return Ok(output);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var output=await _suppliersService.GetAll();
            return Ok(output);
        }
        [HttpPut]
        public async Task <IActionResult> UpdateSupplier(UpdateSupplierInputDto inputDto)
        {
            var output = await _suppliersService.UpdateSupplier(inputDto);
            return Ok(output);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteSupplier(DeleteSupplierInputDto inputDto)
        {
            var output = await _suppliersService.DeleteSupplier(inputDto);
            return Ok(output);
        }
    }
}
