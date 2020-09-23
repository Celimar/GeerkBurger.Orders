using GeekBurger.Order.Contracts;
using GeekBurger.Order.Model;
using System.Threading.Tasks;

namespace GeekBurger.Order.Service.Interfaces
{
    public interface IOrderChangedService
    {
        Task SendMessagesAsync(OrderChanged orderChanged);
    }
}
