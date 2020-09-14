using System.Collections.Generic;

namespace GeekBurger.OrderApi.Repository
{
    public interface IOrderRepository
    {

        List<Model.Order> GetList();

        Model.Order GetOrderByOrderId(int orderid);

        void Insert(Model.Order order);

        void Update(Model.Order order);

        void AddPayment(Model.Payment payment);

        void Save();

    }
}
