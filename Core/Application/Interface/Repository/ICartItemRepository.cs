using Owhytee_Phones.Core.Domain.Entity;
using System.Linq.Expressions;

namespace Owhytee_Phones.Core.Application.Interface.Repository
{
    public interface ICartItemRepository
    {
        Task<CartItem> AddAsync(CartItem cartItem);
        Task<CartItem> GetAsync(int id);
        Task<CartItem> GetAsync(Expression<Func<CartItem, bool>> exp);
        Task<ICollection<CartItem>> GetAllAsync();
        void Remove(CartItem cartItem);
        CartItem Update(CartItem cartItem);
        Task<bool> ExistAsync(int id);
    }
}
