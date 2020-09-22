using GeekBurger.Order.Contracts;
using System;
using System.Collections.Generic;

namespace GeekBurger.Order.Contracts
{
    public class NewOrderMessage
    {
        public int OrderId { get; set; }
        public string StoreName { get; set; }
        public Double Total { get; set; }
        public DateTime OrderTime { get; set; }
        public List<Product> Products { get; set; }
        public List<int> ProductionIds { get; set; }
    }
}