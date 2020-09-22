using GeekBurger.Order.Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace GeekBurger.Order.Repository
{
    public class OrderRepository : IOrderRepository
    {

        private OrderDbContext _context;
        public OrderRepository(OrderDbContext context)
        {
            _context = context;
        }

        public Model.Order GetOrderByOrderId(int orderid)
        {
            var orders = _context.Orders
                    .Where(order => order.OrderId.Equals(orderid))
                    .FirstOrDefault();
            return orders;
        }

        public void AddPayment(Model.Payment payment)
        {

            Model.Order Order = GetOrderByOrderId(payment.OrderId);
            if (Order == null)
            {
                throw new System.Exception("Ordem não localizada com esse Id");
            }
            Order.Payments.Add(payment);

        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public List<Model.Order> GetList()
        {
            return _context.Orders.ToList();
        }

        public void Insert(Model.Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        public void Update(Model.Order order)
        {
            _context.Orders.Update(order);
            _context.SaveChanges();
        }

        List<Model.Order> IOrderRepository.GetOrdersByStoreName(string storeName)
        {
            var orders = _context.Orders
                    .Where(order => order.Store.Name.Equals(storeName))
                    .ToList();
            return orders;
        }
    }
}
