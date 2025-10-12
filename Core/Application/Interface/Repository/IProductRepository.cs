using Owhytee_Phones.Core.Domain.Entity;
using System.Linq.Expressions;

namespace Owhytee_Phones.Core.Application.Interface.Repository
{
    public interface IProductRepository
    {
        Task<Product> AddAsync(Product product);
        Task<Product> GetAsync(int id);
        Task<Product> GetAsync(Expression<Func<Product, bool>> exp);
        Task<ICollection<Product>> GetAllAsync();
        void Remove(Product product);
        Product Update(Product product);
        Task<bool> ExistAsync(int id);
    }
}
