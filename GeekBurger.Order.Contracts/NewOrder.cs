using System;
using System.Collections.Generic;
using System.Text;

namespace GeekBurger.Order.Contracts
{
    public class NewOrder
    {
        public int OrderId { get; set; }
        public string StoreName { get; set; }
        public Double Total { get; set; }
        public List<Product> Products { get; set; }
        public List<int> ProductionIds { get; set; }
    }

}
