using GeekBurger.Products.Contract;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeekBurger.Order.Repository.Interfaces
{
    public interface IProductRepository
    {
        List<Model.Product> Get();
        Model.Product GetById(Guid productId);

        List<Model.Product> GetByIdList(List<Guid> ids);
    }
}
