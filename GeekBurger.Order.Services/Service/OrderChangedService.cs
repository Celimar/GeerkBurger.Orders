using AutoMapper;
using GeekBurger.Order.Contracts;
using GeekBurger.Order.Services.Model;
using GeekBurger.Order.Services.Repository;
using Microsoft.Azure.Management.ServiceBus.Fluent;
using Microsoft.Azure.ServiceBus;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace GeekBurger.Order.Services.Service
{
    public class OrderChangedService : IOrderChangedService
    {
        private IMapper _mapper;
        private Task _lastTask;
        private const string Topic = "OrderChangedTopic";
        private readonly ILogService _logService;
        private readonly IConfiguration _configuration;
        private readonly IServiceBusNamespace _namespace;
        private readonly List<Message> _messages;
        private CancellationTokenSource _cancelMessages;
        private IServiceProvider _serviceProvider { get; }


        public OrderChangedService(IMapper mapper,IConfiguration configuration, ILogService logService, IServiceProvider serviceProvider)
        {
            _mapper = mapper;
            _configuration = configuration;
            _logService = logService;
            _messages = new List<Message>();
            _namespace = _configuration.GetServiceBusNamespace();
            _cancelMessages = new CancellationTokenSource();
            _serviceProvider = serviceProvider;
        }


        public void EnsureTopicIsCreated()
        {
            if (!_namespace.Topics.List().Any(topic => topic.Name.Equals(Topic, StringComparison.InvariantCultureIgnoreCase)))
                _namespace.Topics.Define(Topic)
                    .WithSizeInMB(1024).Create();

        }

        public void AddToMessageList(IEnumerable<EntityEntry<Model.Order>> changes)
        {
            _messages.AddRange(changes
                .Where(entity => entity.State != EntityState.Detached && entity.State != EntityState.Unchanged)
                .Select(GetMessage).ToList()
                );
        }


        private void AddOrUpdateEvent(OrderChangedEvent orderChangedEvent)
        {
            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var scopedProcessingService =
                        scope.ServiceProvider
                            .GetRequiredService<IOrderChangedEventRepository>();

                    OrderChangedEvent evt;
                    if (orderChangedEvent.EventId == Guid.Empty
                        || (evt = scopedProcessingService.Get(orderChangedEvent.EventId)) == null)
                        scopedProcessingService.Add(orderChangedEvent);
                    else
                    {
                        evt.MessageSent = true;
                        scopedProcessingService.Update(evt);
                    }

                    scopedProcessingService.Save();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        public Message GetMessage(EntityEntry<Model.Order> entity)
        {
            var orderChanged = Mapper.Map<OrderChangedMessage>(entity);
            var orderChangedSerialized = JsonConvert.SerializeObject(orderChanged);
            var orderChangedByteArray = Encoding.UTF8.GetBytes(orderChangedSerialized);

            var orderChangedEvent = Mapper.Map<OrderChangedEvent>(entity);
            AddOrUpdateEvent(orderChangedEvent);

            return new Message
            {
                Body      = orderChangedByteArray,
                MessageId = orderChangedEvent.EventId.ToString(),
                Label     = orderChanged.OrderId.ToString()
            };
        }
        

        public async void SendMessagesAsync()
        {
            if (_lastTask != null && !_lastTask.IsCompleted)
                return;

            var config = _configuration.GetSection("serviceBus").Get<ServiceBusConfiguration>();
            var topicClient = new TopicClient(config.ConnectionString, Topic);

            _logService.SendMessagesAsync("Order was changed");

            _lastTask = SendAsync(topicClient, _cancelMessages.Token);

            await _lastTask;

            var closeTask = topicClient.CloseAsync();
            await closeTask;
            HandleException(closeTask);
        }


        public async Task SendAsync(TopicClient topicClient, CancellationToken cancellationToken)
        {
            var tries = 0;
            while (!cancellationToken.IsCancellationRequested)
            {
                if (_messages.Count <= 0)
                    break;

                Message message;
                lock (_messages)
                {
                    message = _messages.FirstOrDefault();
                }

                var sendTask = topicClient.SendAsync(message);
                await sendTask;
                var success = HandleException(sendTask);

                if (!success)
                {
                    var cancelled = cancellationToken.WaitHandle.WaitOne(10000 * (tries < 60 ? tries++ : tries));
                    if (cancelled) break;
                }
                else
                {
                    if (message == null) continue;
                    AddOrUpdateEvent(new OrderChangedEvent() { EventId = new Guid(message.MessageId) });
                    _messages.Remove(message);
                }
            }
        }

        public bool HandleException(Task task)
        {
            if (task.Exception == null || task.Exception.InnerExceptions.Count == 0) return true;

            task.Exception.InnerExceptions.ToList().ForEach(innerException =>
            {
                Console.WriteLine($"Error in SendAsync task: {innerException.Message}. Details:{innerException.StackTrace} ");

                if (innerException is ServiceBusCommunicationException)
                    Console.WriteLine("Connection Problem with Host. Internet Connection can be down");
            });

            return false;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            EnsureTopicIsCreated();

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _cancelMessages.Cancel();

            return Task.CompletedTask;
        }
    }
}
