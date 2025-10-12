using Owhytee_Phones.Core.Application.Interface.Repository;
using Owhytee_Phones.Core.Domain.Entity;
using System.Linq.Expressions;

namespace Owhytee_Phones.Infrastructure.Repository
{
    public class CartRepository : ICartRepository
    {
        public Task<Cart> AddAsync(Cart cart)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Cart>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Cart> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Cart> GetAsync(Expression<Func<Cart, bool>> exp)
        {
            throw new NotImplementedException();
        }

        public void Remove(Cart cart)
        {
            throw new NotImplementedException();
        }

        public Cart Update(Cart cart)
        {
            throw new NotImplementedException();
        }
    }
}
