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
        public async Task<List<SupplierDetailsDto>> GetAll()
        {
            var suppliers = await supplierRepository.GetAll();

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
        public async Task<AddSupplierOutputDto> AddSupplier(AddSupplierInputDto input)
        {
            var createResult = Supplier.Create(input.Name, input.Address, input.Phone);

            if (createResult.Object == null || !createResult.IsSucces)
            {
                return new AddSupplierOutputDto()
                {
                    Erorrs = createResult.Errors.ToList()
                };
            }

            createResult.Object.SetEmail(input.Email);

            await supplierRepository.Create(createResult.Object);

            await unitOfWork.Commit();

            return new AddSupplierOutputDto()
            {
                Id = createResult.Object.Id
            };
        }
        public async Task<MostSupplierHasPurchaseOrderDto> MaxSupplierHasPurchaseOrder()
        {
            var supplierHasMaxPurchaseOrdersCount = (await _orderPurchaseRepository.GetAll())
                .GroupBy(e => e.SupplierId)
                .Select(group => new SupplierCountDto
                {
                    Id = group.Key,
                    Count = group.Count()
                })
                .OrderBy(dc => dc.Count)
                .FirstOrDefault();

            var maxSupplier = await supplierRepository.Get(supplierHasMaxPurchaseOrdersCount.Id);

            return new MostSupplierHasPurchaseOrderDto()
            {
                Adress = maxSupplier.Adress,
                Phone = maxSupplier.Phone,
                Email = maxSupplier.Email,
                Id = maxSupplier.Id,
                Name = maxSupplier.Name,
                NumberOfTimesUsed = supplierHasMaxPurchaseOrdersCount.Count
            };
            //var suppliers = await supplierRepository.GetAll();

            //var orderPurchase = await _orderPurchaseRepository.GetAll();

            //var supplierList = new List<SupplierCountDto>();

            //foreach (var supplier in suppliers)
            //{
            //    var countSupplierHasPurchaseOrder = orderPurchase.Count(z => z.SupplierId == supplier.Id);

            //    supplierList.Add(new SupplierCountDto
            //    {
            //        Count= countSupplierHasPurchaseOrder,
            //        Id=supplier.Id
            //    });
            //}
            //var max = supplierList.OrderBy(x=>x.Count).FirstOrDefault();

            //var maxSupplier=  suppliers.FirstOrDefault(x=>x.Id == max.Id);

            //return new MostSupplierHasPurchaseOrderDto()
            //{
            //    Adress = maxSupplier.Adress,
            //    Phone = maxSupplier.Phone,
            //    Email = maxSupplier.Email,
            //    Id = maxSupplier.Id,
            //    Name = maxSupplier.Name,
            //    NumberOfTimesUsed = max.Count
            //};
        }
        public async Task<UpdateSupplierOutputDto> UpdateSupplier(UpdateSupplierInputDto input)
        {
            var supplier = await supplierRepository.Get(input.Id);

            if (supplier == null)
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

            if (!result.IsSucces)
            {
                return new UpdateSupplierOutputDto()
                {
                    Erorrs = result.Errors.ToList()
                };
            }
            await unitOfWork.Commit();

            return new UpdateSupplierOutputDto()
            {
                Id = supplier.Id
            };
        }
        public async Task<DeleteSupplierOutputDto> DeleteSupplier(DeleteSupplierInputDto inputDto)
        {
            var supplier = await supplierRepository.Get(inputDto.Id);

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
    }
    public class SupplierCountDto
    {
        public int Id { get; set; }
        public int Count { get; set; }
    }
}