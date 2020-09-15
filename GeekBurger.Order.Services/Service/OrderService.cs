using AutoMapper;
using GeekBurger.Order.Contracts;
using GeekBurger.Order.Services.Repository;
using System.Collections.Generic;

namespace GeekBurger.Order.Services.Service
{
    public class OrderService : IOrderService
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderChangedEventRepository _orderChangedEventRepository;
        private readonly IPaymentRepository _paymentRepository;

        public OrderService(IOrderRepository orderRepository, IPaymentRepository paymentRepository, IOrderChangedEventRepository orderChangedEventRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _paymentRepository = paymentRepository;
            _orderChangedEventRepository = orderChangedEventRepository;
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

        public List<Model.OrderChangedEvent> GetOrderChangedEventList()
        {
            return _orderChangedEventRepository.GetList();
        }

        public List<Model.Order> GetOrderByStoreName(string storeName)
        {
            return _orderRepository.GetOrdersByStoreName(storeName);
        }
    }
}
