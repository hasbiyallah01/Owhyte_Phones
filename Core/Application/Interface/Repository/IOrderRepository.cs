using Owhytee_Phones.Core.Domain.Entity;
using System.Linq.Expressions;

namespace Owhytee_Phones.Core.Application.Interface.Repository
{
    public interface IOrderRepository
    {
        Task<Order> AddAsync(Order order);
        Task<Order> GetAsync(int id);
        Task<Order> GetAsync(Expression<Func<Order, bool>> exp);
        Task<ICollection<Order>> GetAllAsync();
        void Remove(Order order);
        Order Update(Order order);
        Task<bool> ExistAsync(int id);
    }
}
