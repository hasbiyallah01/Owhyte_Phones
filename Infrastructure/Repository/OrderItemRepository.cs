using Microsoft.EntityFrameworkCore;
using Owhytee_Phones.Core.Application.Interface.Repository;
using Owhytee_Phones.Core.Domain.Entity;
using System.Linq.Expressions;

namespace Owhytee_Phones.Infrastructure.Repository
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private OwhyteeContext _context;
        public OrderItemRepository(OwhyteeContext context)
        {
            _context = context;
        }
        public async Task<OrderItem> AddAsync(OrderItem orderItem)
        {
            await _context.Set<OrderItem>()
                .AddAsync(orderItem);
            return orderItem;
        }

        public async Task<bool> ExistAsync(int id)
        {
            return await _context.OrderItems.AnyAsync(a => a.Id == id);
        }

        public async Task<ICollection<OrderItem>> GetAllAsync()
        {
            return await _context.Set<OrderItem>().ToListAsync();
        }

        public async Task<OrderItem> GetAsync(int id)
        {
            var answer = await _context.Set<OrderItem>()
                .Where(a => a.Id == id && a.IsDeleted!)
                .SingleOrDefaultAsync();
            return answer;
        }

        public async Task<OrderItem> GetAsync(Expression<Func<OrderItem, bool>> exp)
        {
            var answer = await _context.Set<OrderItem>()
                .Where(a => a.IsDeleted!)
                .SingleOrDefaultAsync(exp);
            return answer;
        }

        public void Remove(OrderItem orderItem)
        {
            orderItem.IsDeleted = true;
            _context.Set<OrderItem>().Update(orderItem);
        }

        public OrderItem Update(OrderItem orderItem)
        {
            _context.Set<OrderItem>().Update(orderItem);
            return orderItem;
        }
    }
}
