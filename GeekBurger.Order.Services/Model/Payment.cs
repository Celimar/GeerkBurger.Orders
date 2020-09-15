using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekBurger.Order.Services.Model
{

    public class Payment
    {
        [Key]
        public Guid PaymentId { get; set; }

        [ForeignKey("OrderId")]
        public Order Order { get; set; }
        public int OrderId { get; set; }


        [ForeignKey("StoreId")]
        public Store Store { get; set; }
        public Guid StoreId { get; set; }

        public PaymentType PayType { get; set; }

        public string CardNumber { get; set; }
        public string CardOwnerName { get; set; }
        public string SecurityCode { get; set; }
        public string ExpirationDate { get; set; }
        public int RequesterId { get; set; }
    }
}
