using Owhytee_Phones.Core.Application.Interface.Repository;
using Owhytee_Phones.Core.Domain.Entity;
using System.Linq.Expressions;

namespace Owhytee_Phones.Infrastructure.Repository
{
    public class ProductRepository : IProductRepository
    {
        public Task<Product> AddAsync(Product product)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Product>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetAsync(Expression<Func<Product, bool>> exp)
        {
            throw new NotImplementedException();
        }

        public void Remove(Product product)
        {
            throw new NotImplementedException();
        }

        public Product Update(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
