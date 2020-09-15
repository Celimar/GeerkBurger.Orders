using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;

namespace GeekBurger.Order.Services.Service
{
   public interface IOrderChangedService : IHostedService
    {
        void SendMessagesAsync();
        void AddToMessageList(IEnumerable<EntityEntry<Model.Order>> changes);
    }
}
