using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;

namespace GeekBurger.Order.Services.Service
{
    public interface INewOrderService : IHostedService
    {
        void ReceiveMessages();
    }
}
