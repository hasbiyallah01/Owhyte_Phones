using Owhytee_Phones.Core.Domain.Entity;
using System.Linq.Expressions;

namespace Owhytee_Phones.Core.Application.Interface.Repository
{
    public interface IOrderItemRepository
    {
        Task<OrderItem> AddAsync(OrderItem orderItem);
        Task<OrderItem> GetAsync(int id);
        Task<OrderItem> GetAsync(Expression<Func<OrderItem, bool>> exp);
        Task<ICollection<OrderItem>> GetAllAsync();
        void Remove(OrderItem orderItem);
        OrderItem Update(OrderItem orderItem);
        Task<bool> ExistAsync(int id);
    }
}
