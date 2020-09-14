using System;
using System.Collections.Generic;
using System.Text;

namespace GeekBurger.Order.Contracts
{
    public enum OrderState
    {
        New = 1,
        Paid = 2,
        Canceled = 3,
        Finished = 4
    }
}
