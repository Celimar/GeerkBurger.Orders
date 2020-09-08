using GeekBurger.Order.Contracts;
using System;
using System.Collections.Generic;

namespace GeekBurger.OrderApi.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public string StoreName { get; set; }
        public Double Total { get; set; }
        public List<Product> Products { get; set; }
        public List<int> ProductionIds { get; set; }
        public string State { get; set; }
        public Payment Payment { get; set; }
    }

}
