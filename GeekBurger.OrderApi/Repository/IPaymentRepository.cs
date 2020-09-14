using System.Collections.Generic;

namespace GeekBurger.OrderApi.Repository
{
    public interface IPaymentRepository
    {
        List<Model.Payment> GetPaymentsByOrderId(int orderid);

        List<Model.Payment> GetPaymentsByStoreName(string storeName);

        void AddPayment(Model.Payment payment);

        void Save();

    }
}
