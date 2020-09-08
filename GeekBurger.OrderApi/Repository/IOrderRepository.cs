using GeekBurger.Order.Contracts;
using Models = GeekBurger.OrderApi.Models;
using System.Collections.Generic;

namespace GeekBurger.OrderApi.Repository
{
    public interface IOrderRepository
    {

        List<Models.Order> GetList();

        Models.Order FindByOrderId(int orderid);

        Models.Order Insert(NewOrder newOrder);

        Models.Order Update(OrderChanged orderChanged);

        void AddPayment(Payment payment);
    }
}
