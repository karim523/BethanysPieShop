using Application.Suppliers.Dtos;

namespace Application.Suppliers
{
    public interface ISuppliersService
    {
        Task<AddSupplierOutputDto> AddSupplier(AddSupplierInputDto input);
        Task<List<SupplierDetailsDto>> GetAll();
        Task<UpdateSupplierOutputDto> UpdateSupplier(UpdateSupplierInputDto input);
        Task<DeleteSupplierOutputDto> DeleteSupplier(DeleteSupplierInputDto inputDto );
        Task<MostSupplierHasPurchaseOrderDto> MaxSupplierHasPurchaseOrder();
    }
}
