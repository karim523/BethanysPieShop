namespace BethanysPieShop.InventoryManagement
{
    public interface IUnitOfWork
    {
        Task<int> Commit();

    }
}
