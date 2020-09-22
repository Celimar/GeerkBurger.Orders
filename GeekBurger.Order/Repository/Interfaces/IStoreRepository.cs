
namespace GeekBurger.Order.Repository.Interfaces
{
    public interface IStoreRepository
    {
        Model.Store GetStoreByName(string storeName);
    }
}
