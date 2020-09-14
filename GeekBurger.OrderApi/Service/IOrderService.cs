using GeekBurger.Order.Contracts;
using System.Collections.Generic;

namespace GeekBurger.OrderApi.Service
{
    public interface IOrderService
    {

        List<Model.Order> GetList();

        void ReceiveNewOrder(NewOrder newOrder);

        void AddPayment(Payment payment);

        void ChangeOrder(OrderChanged orderChanged);

    }
}
