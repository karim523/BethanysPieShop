using Application.OrderPurchases.Dtos;
using BethanysPieShop.InventoryManagement;
using BethanysPieShop.InventoryManagement.Domain.OrderManagment;
using BethanysPieShop.InventoryManagement.IRepository;

namespace Application.OrderPurchases
{
    public class OrderPurchasesService : IOrderPurchasesService
    {
        private readonly IOrderPurchaseRepository _orderPurchaseRepository;
        private readonly ISupplierRepository _supplierRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public OrderPurchasesService(IOrderPurchaseRepository orderPurchaseRepository, IUnitOfWork unitOfWork, IProductRepository productRepository, ISupplierRepository supplierRepository)
        {
            _orderPurchaseRepository = orderPurchaseRepository;
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
            _supplierRepository = supplierRepository;
        }
        public async Task<AddOrderPurchaseOutputDto> AddOrder(AddOrderPurchaseInputDto input)
        {
            var supplier = await _supplierRepository.Get(input.SupplierId);

            if (supplier == null)
            {
                return new AddOrderPurchaseOutputDto()
                {
                    Errors = new List<string>()
                    {
                        "Supplier is Not Exist"
                    }
                };
            }
            List<OrderItem> orderItems = new List<OrderItem>();
            foreach (var item in input.OrderItems)
            {
                var product = await _productRepository.Get(item.ProductId);
               
                if (product == null)
                {
                    return new AddOrderPurchaseOutputDto()
                    {
                        Errors = new List<string>()
                        {
                            "Product is Not Exist"
                        }
                    };
                }
                var orderItem =new OrderItem(item.Amount,product);
                orderItems.Add(orderItem);
            }
            
            var orderPurchase = new OrderPurchase(input.SupplierId, input.OrderDate, orderItems);            
        
            await _orderPurchaseRepository.CreateOrder(orderPurchase);

            await _unitOfWork.Commit();

            return new AddOrderPurchaseOutputDto
            {
                Id = orderPurchase.Id
            };
        }
    }
}
