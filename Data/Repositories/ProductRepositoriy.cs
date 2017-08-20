using Data.Infrastructure;
using Model.Models;

namespace Data.Repositories
{
    public interface IProductRepository
    {

    }
    public class ProductRepository : RepositoryBase<Product>,IProductRepository
    {
        public ProductRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}
