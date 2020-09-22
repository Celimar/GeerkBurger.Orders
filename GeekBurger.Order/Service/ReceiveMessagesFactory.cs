using GeekBurger.Order.Service.Interfaces;

namespace GeekBurger.Order.Service
{
    public class ReceiveMessagesFactory : IReceiveMessagesFactory
    {
        private readonly IOrderService _salesService;

        public ReceiveMessagesFactory(IOrderService salesService)
        {
            _salesService = salesService;            

            CreateNewOrderService("neworder", "UI");
        }

        public ReceiveMessagesNewOrderService CreateNewOrderService(string topic, string subscription, string filterName = null, string filter = null)
        {
            return new ReceiveMessagesNewOrderService(_salesService, topic, subscription, filterName, filter);
        }
    }
}
