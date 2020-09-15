using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GeekBurger.Order.Contracts;

namespace GeekBurger.Order.Services.Model
{
    public class OrderChangedEvent
    {
        [Key]
        public Guid EventId { get; set; }

        public OrderState State { get; set; }

        [ForeignKey("OrderId")]
        public Model.Order Order { get; set; }

        public bool MessageSent { get; set; }
    }
}