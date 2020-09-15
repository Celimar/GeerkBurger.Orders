using GeekBurger.Order.Services.Model;
using System;
using System.Linq;

namespace GeekBurger.Order.Services.Repository
{
    public class StoreRepository : IStoreRepository
    {
        private OrderDbContext _context { get; set; }

        public StoreRepository(OrderDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Store GetStoreByName(string storeName)
        {
            return _context.Stores.FirstOrDefault(store => store.Name.Equals(storeName, StringComparison.InvariantCultureIgnoreCase));   
        }
    }
}
