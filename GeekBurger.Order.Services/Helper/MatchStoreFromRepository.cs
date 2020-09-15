using AutoMapper;
using GeekBurger.Order.Contracts;
using GeekBurger.Order.Services.Repository;

namespace GeekBurger.Order.Services.Helper
{
    public class MatchStoreFromRepository : IMappingAction<NewOrder, Model.Order>
    {
        private IStoreRepository _storeRepository;

        public MatchStoreFromRepository(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
        }

        public void Process(NewOrder source, Model.Order destination)
        {
            var store = _storeRepository.GetStoreByName(source.StoreName);

            if (store != null)
                destination.StoreId = store.StoreId;
        }
    }
}
