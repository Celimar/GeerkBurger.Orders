
namespace GeekBurger.Order.Services.Repository
{
    public interface IStoreRepository
    {
        Model.Store GetStoreByName(string storeName);
    }
}
