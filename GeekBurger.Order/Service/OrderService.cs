using AutoMapper;
using GeekBurger.Order.Contracts;
using GeekBurger.Order.Repository;
using GeekBurger.Order.Repository.Interfaces;
using GeekBurger.Order.Service.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeekBurger.Order.Service
{
    public class OrderService : IOrderService
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;
        //private readonly IOrderChangedEventRepository _orderChangedEventRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IOrderChangedService _orderChangedService;

        public OrderService(
            IOrderRepository orderRepository, 
            IPaymentRepository paymentRepository, 
            IOrderChangedService orderChangedService,
            IMapper mapper)
        {
            _orderRepository = orderRepository;
            _paymentRepository = paymentRepository;
            _orderChangedService = orderChangedService;
            _mapper = mapper;
        }

        public async Task ChangeOrder(OrderChanged orderChanged)
        {
            var order = _mapper.Map<Model.Order>(orderChanged);

            await Task.Run(() =>
            {
                _orderRepository.Update(order);
                _orderRepository.Save();
            });

        }

        public async Task<List<Model.Order>> GetList()
        {
            return await Task.Run(() => _orderRepository.GetList() );
        }

        public async Task AddPayment(Payment payment)
        {
            var newPayment = _mapper.Map<Model.Payment>(payment);

            await Task.Run(() =>
            {
                _paymentRepository.AddPayment(newPayment);
                _paymentRepository.Save();

                var order = _orderRepository.GetOrderByOrderId(payment.OrderId);
                order.Payments.Add(newPayment);
                _orderRepository.AddPayment(newPayment);
                _orderRepository.Save();

                OrderChanged orderChanged = new OrderChanged()
                {
                    OrderId = order.OrderId,
                    StoreName = order.Store.Name,
                    State = "Paid"
                };

                //notificar publish Order Changed
                _orderChangedService.SendMessagesAsync(orderChanged);
            });



        }

        public async Task<List<Model.OrderChangedEvent>> GetOrderChangedEventList()
        {
            //return _orderChangedEventRepository.GetList();
            return await Task.Run(() => new List<Model.OrderChangedEvent>() );
        }


        public async Task<List<Model.Order>> GetOrderByStoreName(string storeName)
        {
            return await Task.Run(() => _orderRepository.GetOrdersByStoreName(storeName));
        }

        public async Task Insert(NewOrder newOrder)
        {
            var order = _mapper.Map<Model.Order>(newOrder);
            await Insert(order);
        }

        public async Task Insert(Model.Order order)
        {
            await Task.Run(() =>
            {
                _orderRepository.Insert(order);
                _orderRepository.Save();
            });
        }


    }
}
