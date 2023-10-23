using Application.Suppliers.Dtos;
using BethanysPieShop.InventoryManagement;
using BethanysPieShop.InventoryManagement.Domain.SupplierManagement;
using BethanysPieShop.InventoryManagement.IRepository;

namespace Application.Suppliers
{
    public class SuppliersService : ISuppliersService
    {
        private readonly ISupplierRepository supplierRepository;
        private readonly IOrderPurchaseRepository _orderPurchaseRepository;
        private readonly IUnitOfWork unitOfWork;

        public SuppliersService(ISupplierRepository supplierRepository, IUnitOfWork unitOfWork, IOrderPurchaseRepository orderPurchaseRepository)
        {
            this.supplierRepository = supplierRepository;
            this.unitOfWork = unitOfWork;
            _orderPurchaseRepository = orderPurchaseRepository;
        }

        public async Task<AddSupplierOutputDto> AddSupplier(AddSupplierInputDto input)
        {
            var supplier=new Supplier(input.Name, input.Address, input.Phone);
            
            supplier.SetEmail(input.Email);
           
            //if(supplier.IsInvalid)
            //{
            //    return new AddSupplierOutputDto()
            //    {
            //        Erorrs = supplier.Notifications
            //    };
            //}

            await supplierRepository.Create(supplier);

            await unitOfWork.Commit();

            return new AddSupplierOutputDto()
            {
                Id = supplier.Id
            };
        }

        public async Task<DeleteSupplierOutputDto> DeleteSupplier(DeleteSupplierInputDto inputDto)
        {
            var supplier=await supplierRepository.Get(inputDto.Id);
            
            if (supplier == null)
            {
                return new DeleteSupplierOutputDto()
                {
                    Errors = new List<string>()
                    {
                        "This Supplier is not Exist"
                    }
                };
            }
            var supplierHasPurchaseOrder = await _orderPurchaseRepository.IsSupplierHasPurchaseOrder(inputDto.Id);
          
                if (supplierHasPurchaseOrder)
                {
                    return new DeleteSupplierOutputDto()
                    {
                        Errors = new List<string>()
                        {
                            "Unable to Delete This Supplier Becuse is Used "
                        }
                    };
                }
            supplierRepository.Delete(supplier);

            await unitOfWork.Commit();

            return new DeleteSupplierOutputDto()
            {
                Id = supplier.Id
            };
        }

        public async Task<List<SupplierDetailsDto>> GetAll()
        {
            var suppliers=await supplierRepository.GetAll();

            List<SupplierDetailsDto> supplierDetailsDtos = suppliers.Select(supplier => new SupplierDetailsDto
            {
                Id = supplier.Id,
                Name = supplier.Name,
                Email = supplier.Email,
                Phone = supplier.Phone,
                Adress = supplier.Adress
            }).ToList();

            return supplierDetailsDtos;
        }

        public async Task<UpdateSupplierOutputDto> UpdateSupplier(UpdateSupplierInputDto input)
        {
            var supplier=await supplierRepository.Get(input.Id);
           
            if (supplier==null)
            {
                return new UpdateSupplierOutputDto()
                {
                    Erorrs = new List<string>()
                    {
                        "This Supplier is not Exist"
                    }
                };
            }

            var result = supplier.UpdateSupplier(input.Name, input.Adress, input.Phone, input.Email);

            if (!result)
            {
                return new UpdateSupplierOutputDto()
                {
                    Erorrs = supplier.Notifications
                };
            }
            await unitOfWork.Commit();
           
            return new UpdateSupplierOutputDto()
            {
                Id = supplier.Id
            };
        }
    }
}
