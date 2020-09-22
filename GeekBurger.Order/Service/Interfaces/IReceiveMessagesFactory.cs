
namespace GeekBurger.Order.Service.Interfaces
{
    public interface IReceiveMessagesFactory
    {
        ReceiveMessagesNewOrderService CreateNewOrderService(string topic, string subscription, string filterName = null, string filter = null);
    }
}
