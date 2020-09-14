using System.Collections.Generic;
using System.Linq;

namespace GeekBurger.OrderApi.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        private OrderDbContext _context;

        public PaymentRepository(OrderDbContext context)
        {
            _context = context;
        }

        public void Save()
        {            
            _context.SaveChanges();
        }

        public void AddPayment(Model.Payment payment)
        {
            _context.Add(payment);
        }

        public List<Model.Payment> GetPaymentsByOrderId(int orderid)
        {
            return _context.Payments
                .Where(payment => payment.OrderId == orderid)
                .ToList();
        }

        public List<Model.Payment> GetPaymentsByStoreName(string storeName)
        {
            return _context.Payments
                .Where(payment => payment.Store.Name.Equals(storeName))
                .ToList();
        }
    }
}
