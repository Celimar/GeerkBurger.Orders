using AutoMapper;
using GeekBurger.Order.Contracts;
using GeekBurger.Order.Repository;
using GeekBurger.Order.Repository.Interfaces;
using System.Collections.Generic;

namespace GeekBurger.Order.Helper
{
    public class MatchOrderFromRepository : IMappingAction<NewOrder, Model.Order>
    {
        private IStoreRepository _storeRepository;
        //private IProductRepository _productRepository;

        public MatchOrderFromRepository(IStoreRepository storeRepository
            //, IProductRepository productRepository
            )
        {
            _storeRepository = storeRepository;
            //_productRepository = productRepository;
        }

        public void Process(NewOrder source, Model.Order destination)
        {
            var store = _storeRepository.GetStoreByName(source.StoreName);
            //var prods = _productRepository.GetByIdList(source.ProductionIds);

            if (store != null)
            {
                destination.Store    = store;
                destination.StoreId  = store.StoreId;
                destination.State    = Model.OrderState.New;
            };
            destination.Products = null;
            destination.Payments = null;

            destination.Products = new List<Model.Product>();
        }
    }
}
