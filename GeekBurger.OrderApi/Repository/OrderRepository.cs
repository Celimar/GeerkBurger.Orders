using GeekBurger.Order.Contracts;
using System.Collections.Generic;
using Models = GeekBurger.OrderApi.Models;

namespace GeekBurger.OrderApi.Repository
{
    public class OrderRepository : IOrderRepository
    {

        private List<Models.Order> _orders;

        public OrderRepository()
        {
            _orders = new List<Models.Order>();
        }

        public List<Models.Order> GetList()
        {
            return _orders;
        }

        public Models.Order FindByOrderId(int orderid)
        {
            return _orders.Find(x => x.OrderId.Equals(orderid));
        }

        public Models.Order Insert(NewOrder newOrder)
        {
            if (FindByOrderId(newOrder.OrderId) == null)
            {
                throw new System.Exception("Já existe ordem cadastrada com esse Id");
            }

            Models.Order Order = new Models.Order()
            {
                OrderId = newOrder.OrderId,
                StoreName = newOrder.StoreName,
                Total = newOrder.Total,
                Products = newOrder.Products,
                ProductionIds = newOrder.ProductionIds,
                State = "New"
            };

            _orders.Add(Order);
            return Order;
        }

        public Models.Order Update(OrderChanged orderChanged)
        {

            Models.Order Order = FindByOrderId(orderChanged.OrderId);
            if (Order == null)
            {
                throw new System.Exception("Ordem não localizada com esse Id");
            }

            //Order.State = orderChanged.State;
            Order.StoreName = orderChanged.StoreName;

            _orders.Add(Order);
            return Order;
        }

        public void AddPayment(Payment payment)
        {
            Models.Order Order = FindByOrderId(payment.OrderId);
            if (Order == null)
            {
                throw new System.Exception("Ordem não localizada com esse Id");
            }
            Order.Payment = payment;
        }
    }
}
