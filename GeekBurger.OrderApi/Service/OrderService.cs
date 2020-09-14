using AutoMapper;
using GeekBurger.Order.Contracts;
using GeekBurger.OrderApi.Repository;
using System.Collections.Generic;

namespace GeekBurger.OrderApi.Service
{
    public class OrderService : IOrderService
    {
        private IMapper _mapper;
        private IOrderRepository _orderRepository;
        private IPaymentRepository _paymentRepository;

        public OrderService(IOrderRepository orderRepository, IPaymentRepository paymentRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _paymentRepository = paymentRepository;
            _mapper = mapper;
        }

        public void ChangeOrder(OrderChanged orderChanged)
        {
            var order = _mapper.Map<Model.Order>(orderChanged);

            _orderRepository.Update(order);
            _orderRepository.Save();
        }

        public List<Model.Order> GetList()
        {
            return _orderRepository.GetList();
        }

        public void AddPayment(Payment payment)
        {
            var newPayment = _mapper.Map<Model.Payment>(payment);

            _paymentRepository.AddPayment(newPayment);
            _paymentRepository.Save();

            var order = _orderRepository.GetOrderByOrderId(payment.OrderId);
            order.Payments.Add(newPayment);
            _orderRepository.AddPayment(newPayment);
            _orderRepository.Save();

            //TODO -notificar publish Order Changed
        }

        public void ReceiveNewOrder(NewOrder newOrder)
        {
            var order = _mapper.Map<Model.Order>(newOrder);

            _orderRepository.Insert(order);
            _orderRepository.Save();
        }
    }
}
