using Application.OrderPurchases.Dtos;

namespace Application.OrderPurchases
{
    public interface IOrderPurchasesService
    {
        Task<AddOrderPurchaseOutputDto> AddOrder(AddOrderPurchaseInputDto dto);
    }
}
