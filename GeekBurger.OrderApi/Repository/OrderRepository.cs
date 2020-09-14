using System.Collections.Generic;
using System.Linq;

namespace GeekBurger.OrderApi.Repository
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
                //    .Include(order => order)
                    .FirstOrDefault()
                    ;
            return orders;
        }

//        public Model.Order Insert(NewOrder newOrder)
//        {
//            if (GetOrderByOrderId(newOrder.OrderId) == null)
//            {
//                throw new System.Exception("Já existe ordem cadastrada com esse Id");
//            }

//            Model.Order Order = new Model.Order()
//            {
//                OrderId = newOrder.OrderId,
////                StoreName = newOrder.StoreName,
//                Total = newOrder.Total,
////                Products = newOrder.Products,
////                ProductionIds = newOrder.ProductionIds,
//                State = OrderState.New
//            };

//            _orders.Add(Order);
//            return Order;
//        }

        //public Model.Order Update(OrderChanged orderChanged)
        //{

        //    Model.Order Order = GetOrderByOrderId(orderChanged.OrderId);
        //    if (Order == null)
        //    {
        //        throw new System.Exception("Ordem não localizada com esse Id");
        //    }

        //    //Order.State = orderChanged.State;
        //    //Order.StoreName = orderChanged.StoreName;

        //    _orders.Add(Order);
        //    return Order;
        //}

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
    }
}
