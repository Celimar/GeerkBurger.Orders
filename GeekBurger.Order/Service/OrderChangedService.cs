using GeekBurger.Order.Configuration;
using GeekBurger.Order.Contracts;
using GeekBurger.Order.Model;
using GeekBurger.Order.Service.Interfaces;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GeekBurger.Order.Service
{
    public class OrderChangedService : IOrderChangedService
    {
        IConfiguration Configuration;

        public OrderChangedService(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public async Task SendMessagesAsync(OrderChanged orderChanged)
        {
            try
            {
                var config = Configuration
                    .GetSection("serviceBus")
                    .Get<ServiceBusConfiguration>();

                var json = JsonSerializer.Serialize(orderChanged);
                var message = new Message(Encoding.UTF8.GetBytes(json));

                var topicClient = new TopicClient(config.ConnectionString, config.TopicName);
                await topicClient.SendAsync(message);

                var closeTask = topicClient.CloseAsync();
                await closeTask;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
