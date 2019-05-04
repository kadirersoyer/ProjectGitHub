using OrderManagment.Entities.Entities;
using System.Collections.Generic;

namespace OrderManagment.Core.Repositories
{
    public interface IProductViewRepository
    {
        IEnumerable<ProductDetailList> GetProductDetailLists(string URL);
    }
}
