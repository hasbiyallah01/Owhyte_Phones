using Owhytee_Phones.Core.Application.Interface.Repository;
using Owhytee_Phones.Core.Domain.Entity;
using System.Linq.Expressions;

namespace Owhytee_Phones.Infrastructure.Repository
{
    public class OrderRepository : IOrderRepository
    {
        public Task<Order> AddAsync(Order order)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Order>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Order> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Order> GetAsync(Expression<Func<Order, bool>> exp)
        {
            throw new NotImplementedException();
        }

        public void Remove(Order order)
        {
            throw new NotImplementedException();
        }

        public Order Update(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
