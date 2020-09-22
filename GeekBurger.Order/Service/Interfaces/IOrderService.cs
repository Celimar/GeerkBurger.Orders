using GeekBurger.Order.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeekBurger.Order.Service.Interfaces
{
    public interface IOrderService
    {

        //Task<List<NewOrder>> Get();

        //Task Create(NewOrder order);

        //Task Create(List<NewOrder> orders);

        //Task<NewOrder> GetById(int orderId);


        Task<List<Model.Order>> GetList();

        Task<List<Model.OrderChangedEvent>> GetOrderChangedEventList();

        Task<List<Model.Order>> GetOrderByStoreName(string storeName);
       

        Task Insert(NewOrder newOrder);

        Task AddPayment(Payment payment);

        Task ChangeOrder(OrderChanged orderChanged);

    }
}
