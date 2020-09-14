using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GeekBurger.OrderApi.Model
{    public class Store
    {
        [Key]
        public Guid StoreId { get; set; }
        public string Name { get; set; }
    }

}
