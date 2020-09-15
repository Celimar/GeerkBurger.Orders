using AutoMapper;
using GeekBurger.Order.Contracts;
using Microsoft.Azure.Management.ServiceBus.Fluent;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace GeekBurger.Order.Services.Service
{
    public class NewOrderService : INewOrderService
    {
        private IMapper _mapper;
        private readonly Task _lastTask;
        private const string _topicName = "neworder";
        private const string _subscriptionName = "ui";
        private readonly ILogService _logService;
        private readonly IConfiguration _configuration;
        private readonly IServiceBusNamespace _namespace;
        private readonly List<Message> _messages;
        private readonly CancellationTokenSource _cancelMessages;
        private IServiceProvider _serviceProvider { get; }

        static ISubscriptionClient subscriptionClient;


        public NewOrderService(IMapper mapper, IConfiguration configuration, ILogService logService, IServiceProvider serviceProvider)
        {
            _mapper = mapper;
            _configuration = configuration;
            _logService = logService;
            _messages = new List<Message>();
            _namespace = _configuration.GetServiceBusNamespace();
            _cancelMessages = new CancellationTokenSource();
            _serviceProvider = serviceProvider;

            try
            {
                var ServiceBusConfiguration = _configuration.GetSection("serviceBus").Get<ServiceBusConfiguration>();

                subscriptionClient = new SubscriptionClient(ServiceBusConfiguration.ConnectionString, _topicName, _subscriptionName);
            }
            catch (Exception e )
            {
                Console.WriteLine($"NewOrderService constructor exception: '{e}'");

                throw;
            }
        }


        public void EnsureTopicIsCreated()
        {
            if (!_namespace.Topics.List().Any(topic => topic.Name.Equals(_topicName, StringComparison.InvariantCultureIgnoreCase)))
            {
                _namespace.Topics.Define(_topicName)
                    .WithNewSubscription(_subscriptionName)
                    .WithSizeInMB(1024).Create();
            }
        }
        static void RegisterOnMessageHandlerAndReceiveMessages()
        {
            Console.WriteLine("======================== \n NewOrderService RegisterOnMessageHandlerAndReceiveMessages ");
            var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                MaxConcurrentCalls = 1,
                AutoComplete = false
            };

            // Registra a função que vai receber a mensagem
            subscriptionClient.RegisterMessageHandler(ProcessMessagesAsync, messageHandlerOptions);
        }

        static async Task ProcessMessagesAsync(Message message, CancellationToken token)
        {
            Console.WriteLine("======================== \n NewOrderService ProcessMessagesAsync ");
            try
            {
                var newOrder = JsonSerializer.Deserialize<NewOrder>(message.Body);
                Console.WriteLine("newOrder: " + newOrder);

            }
            catch (Exception e)
            {
                Console.WriteLine($"exception: '{e}'");
            }
            finally
            {
                //await subscriptionClient.CompleteAsync(message.SystemProperties.LockToken);
                await Task.CompletedTask;
            }
        }

        static Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            Console.WriteLine("======================== \n NewOrderService ExceptionReceivedHandler ");
            Console.WriteLine($"Message handler encountered an exception {exceptionReceivedEventArgs.Exception}.");
            var context = exceptionReceivedEventArgs.ExceptionReceivedContext;
            Console.WriteLine("Exception context for troubleshooting:");
            Console.WriteLine($"- Endpoint: {context.Endpoint}");
            Console.WriteLine($"- Entity Path: {context.EntityPath}");
            Console.WriteLine($"- Executing Action: {context.Action}");
            return Task.CompletedTask;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("======================== \n NewOrderService StartAsync ");
            EnsureTopicIsCreated();
            RegisterOnMessageHandlerAndReceiveMessages();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("======================== \n NewOrderService StopAsync ");
            _cancelMessages.Cancel();

            return Task.CompletedTask;
        }

        public void ReceiveMessages()
        {

            Console.WriteLine("======================== \n NewOrderService ReceiveMessages ");
            RegisterOnMessageHandlerAndReceiveMessages();
        }
    }
}
