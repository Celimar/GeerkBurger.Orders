using System.Collections.Generic;

namespace GeekBurger.Order.Services.Repository
{
    public interface IOrderRepository
    {

        List<Model.Order> GetList();

        Model.Order GetOrderByOrderId(int orderid);

        List<Model.Order> GetOrdersByStoreName(string storeName);

        void Insert(Model.Order order);

        void Update(Model.Order order);

        void AddPayment(Model.Payment payment);

        void Save();

    }
}
