using System;
using System.Collections.Generic;

namespace GeekBurger.Order.Contracts
{
    public class OrderChangedMessage
    {
        public int OrderId { get; set; }
        public string StoreName { get; set; }
        public string State { get; set; }
        public Double Total { get; set; }
        public DateTime OrderTime { get; set; }

    }
}
