using Owhytee_Phones.Core.Application.Interface.Repository;
using Owhytee_Phones.Core.Domain.Entity;
using System.Linq.Expressions;

namespace Owhytee_Phones.Infrastructure.Repository
{
    public class CartItemRepository : ICartItemRepository
    {
        public Task<CartItem> AddAsync(CartItem cartItem)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<CartItem>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<CartItem> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<CartItem> GetAsync(Expression<Func<CartItem, bool>> exp)
        {
            throw new NotImplementedException();
        }

        public void Remove(CartItem cartItem)
        {
            throw new NotImplementedException();
        }

        public CartItem Update(CartItem cartItem)
        {
            throw new NotImplementedException();
        }
    }
}
