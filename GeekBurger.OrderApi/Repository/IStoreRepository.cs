
namespace GeekBurger.OrderApi.Repository
{
    public interface IStoreRepository
    {
        Model.Store GetStoreByName(string storeName);
    }
}
