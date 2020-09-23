using GeekBurger.Order.Repository.Interfaces;
using GeekBurger.Products.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekBurger.Order.Repository
{
    public class ProductRepository : IProductRepository
    {
        private OrderDbContext _context;

        public ProductRepository(OrderDbContext context)
        {
            _context = context;
        }

        public List<Model.Product> Get()
        {
            return _context.Products.ToList();
        }

        public Model.Product GetById(Guid productId)
        {
            return _context.Products
                .Where(p => p.ProductId.Equals(productId))
                .FirstOrDefault();
        }

        public List<Model.Product> GetByIdList(List<Guid> ids)
        {
            return _context.Products
                .Where(p => ids.Contains( p.ProductId) )
                .ToList();
        }


    }

}
