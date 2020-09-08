using System;
using System.Collections.Generic;
using System.Text;

namespace GeekBurger.Order.Contracts
{
    public class OrderChanged
    {
        public int OrderId { get; set; }
        public string StoreName { get; set; }
        public string State { get; set; }
    }
}
