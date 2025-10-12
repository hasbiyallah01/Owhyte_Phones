using Owhytee_Phones.Core.Application.Interface.Repository;
using Owhytee_Phones.Core.Domain.Entity;
using System.Linq.Expressions;

namespace Owhytee_Phones.Infrastructure.Repository
{
    public class OrderItemRepository : IOrderItemRepository
    {
        public Task<OrderItem> AddAsync(OrderItem orderItem)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<OrderItem>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<OrderItem> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<OrderItem> GetAsync(Expression<Func<OrderItem, bool>> exp)
        {
            throw new NotImplementedException();
        }

        public void Remove(OrderItem orderItem)
        {
            throw new NotImplementedException();
        }

        public OrderItem Update(OrderItem orderItem)
        {
            throw new NotImplementedException();
        }
    }
}
