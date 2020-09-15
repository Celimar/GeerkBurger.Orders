using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekBurger.Order.Services.Model
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public Double Total { get; set; }
        public OrderState State { get; set; }
        public DateTime OrderTime { get; set; }

        [ForeignKey("StoreId")]
        public Store Store { get; set; }
        public Guid StoreId { get; set; }

        public List<Product> Product { get; set; }
        
        public List<Payment> Payments { get; set; }
    }


}
