using GeekBurger.Order.Contracts;
using System.Collections.Generic;

namespace GeekBurger.Order.Services.Service
{
    public interface IOrderService
    {

        List<Model.Order> GetList();
        List<Model.OrderChangedEvent> GetOrderChangedEventList();
        List<Model.Order> GetOrderByStoreName(string storeName);

        void ReceiveNewOrder(NewOrder newOrder);

        void AddPayment(Payment payment);

        void ChangeOrder(OrderChanged orderChanged);

    }
}
