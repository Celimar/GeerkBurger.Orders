using Microsoft.Azure.Management.ServiceBus.Fluent;
using System.Threading.Tasks;

namespace GeekBurger.Order.Service.Interfaces
{
    public interface IServiceBusService
    {
        Task<IServiceBusNamespace> ListarTopicos();
        Task CriarTopico();
    }
}
