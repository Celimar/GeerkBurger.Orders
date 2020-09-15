using GeekBurger.Order.Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GeekBurger.Order.Services.Repository
{
    public class OrderChangedEventRepository : IOrderChangedEventRepository
    {
        private readonly OrderDbContext _dbContext;

        public OrderChangedEventRepository(OrderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool Add(OrderChangedEvent orderChangedEvent)
        {
            orderChangedEvent.Order =_dbContext.Orders
                .FirstOrDefault(_ => _.OrderId == orderChangedEvent.Order.OrderId);

            orderChangedEvent.EventId = Guid.NewGuid();

            _dbContext.OrderChangedEvents.Add(orderChangedEvent);

            return true;
        }

        public OrderChangedEvent Get(Guid eventId)
        {
            return _dbContext.OrderChangedEvents
                .FirstOrDefault(order => order.EventId == eventId);
        }

        public List<OrderChangedEvent> GetList()
        {
            return _dbContext.OrderChangedEvents.ToList();
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public bool Update(OrderChangedEvent orderChangedEvent)
        {
            return true;
        }
    }
}
