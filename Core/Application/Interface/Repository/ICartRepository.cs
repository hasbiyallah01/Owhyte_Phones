using Owhytee_Phones.Core.Domain.Entity;
using System.Linq.Expressions;

namespace Owhytee_Phones.Core.Application.Interface.Repository
{
    public interface ICartRepository
    {
        Task<Cart> AddAsync(Cart cart);
        Task<Cart> GetAsync(int id);
        Task<Cart> GetAsync(Expression<Func<Cart, bool>> exp);
        Task<ICollection<Cart>> GetAllAsync();
        void Remove(Cart cart);
        Cart Update(Cart cart);
        Task<bool> ExistAsync(int id);
        Task<Preference> GetAsync(string sessionId);
        Task<Preference> AddAsync(Preference preference);
    }
}
