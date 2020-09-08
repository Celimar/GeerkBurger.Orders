using GeekBurger.Order.Contracts;
using GeekBurger.OrderApi.Repository;
using System.Collections.Generic;

namespace GeekBurger.OrderApi.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;


        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public void ChangeOrder(OrderChanged orderChanged)
        {
            _orderRepository.Update(orderChanged);
        }

        public List<Models.Order> GetList()
        {
            return _orderRepository.GetList();
        }

        public void ReceiveNewOrder(NewOrder newOrder)
        {
            Models.Order order =_orderRepository.Insert(newOrder);
        }

        public void ReceivePayment(Payment payment)
        {

            _orderRepository.AddPayment(payment);
            //TODO -notificar publish Order Changed
        }
    }
}
