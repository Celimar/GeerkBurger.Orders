using System;
using System.Collections.Generic;
using GeekBurger.Order.Services.Model;

namespace GeekBurger.Order.Services.Repository
{
    public interface IOrderChangedEventRepository
    {
        OrderChangedEvent Get(Guid eventId);
        bool Add(OrderChangedEvent orderChangedEvent);
        bool Update(OrderChangedEvent orderChangedEvent);
        void Save();
        List<OrderChangedEvent> GetList();
    }
}