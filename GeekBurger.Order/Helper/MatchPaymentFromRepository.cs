using AutoMapper;
using GeekBurger.Order.Contracts;
using GeekBurger.Order.Repository;
using GeekBurger.Order.Repository.Interfaces;

namespace GeekBurger.Order.Helper
{
    public class MatchPaymentFromRepository : IMappingAction<Contracts.Payment, Model.Payment>
    {
        private IStoreRepository _storeRepository;
        private IOrderRepository _orderRepository;
        public MatchPaymentFromRepository(IStoreRepository storeRepository, IOrderRepository orderRepository)
        {
            _storeRepository = storeRepository;
            _orderRepository = orderRepository;
        }

        public void Process(Payment source, Model.Payment destination)
        {
            Model.Store store = _storeRepository.GetStoreByName(source.StoreName);
            Model.Order order = _orderRepository.GetOrderByOrderId(source.OrderId);

            if (store != null  && order != null)
            {
                destination.StoreId = store.StoreId;
                destination.OrderId = order.OrderId;
                //TODO:  verificar de map permite inserir item na lista do pai
                //order.Payments.Add(destination);
            }

        }

        public void Process(Payment source, Model.Payment destination, ResolutionContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}
