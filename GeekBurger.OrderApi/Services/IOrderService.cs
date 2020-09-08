using GeekBurger.Order.Contracts;
using System.Collections.Generic;

namespace GeekBurger.OrderApi.Services
{
    public interface IOrderService
    {

        List<Models.Order> GetList();

        void ReceiveNewOrder(NewOrder newOrder);

        void ReceivePayment(Payment payment);

        void ChangeOrder(OrderChanged orderChanged);

    }
}
