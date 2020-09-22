using Microsoft.Azure.Management.ServiceBus.Fluent;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using GeekBurger.Order.Service.Interfaces;

namespace GeekBurger.Order.Service
{
    public class ServiceBusService : IServiceBusService
    {
        IConfiguration Configuration;


        public ServiceBusService(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public async Task<IServiceBusNamespace> ListarTopicos()
        {
            return ServiceBusNamespaceExtension.GetServiceBusNamespace(Configuration);
        }

        public async Task CriarTopico()
        {
            ServiceBusNamespaceExtension.CreateTopic(Configuration);
        }
    }
}
