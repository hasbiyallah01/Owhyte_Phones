using Microsoft.EntityFrameworkCore;
using Owhytee_Phones.Core.Application.Interface.Repository;
using Owhytee_Phones.Core.Domain.Entity;
using System.Linq.Expressions;

namespace Owhytee_Phones.Infrastructure.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private OwhyteeContext _context;
        public OrderRepository(OwhyteeContext context)
        {
            _context = context;
        }
        public async Task<Order> AddAsync(Order order)
        {
            await _context.Set<Order>().AddAsync(order);
            return order;
        }

        public async Task<bool> ExistAsync(int id)
        {
            return await _context.Orders.AnyAsync(o => o.Id == id);
        }
        public async Task<Dictionary<int, int>> GetOrderCountsByCooperativeAsync()
        {
            return await _context.Orders
                .Where(o => o.CooperativeId != null)
                .GroupBy(o => o.CooperativeId)
                .Select(g => new { CooperativeId = g.Key!.Value, Count = g.Count() })
                .ToDictionaryAsync(x => x.CooperativeId, x => x.Count);
        }
        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _context.Set<Order>().ToListAsync();
        }
        public async Task<ICollection<Order>> GetAllAsync(int id)
        {
            return await _context.Set<Order>()
            .Include(o => o.Cooperative)
            .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
            .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Variant)
            .Where(o => o.CooperativeId == id)
            .OrderByDescending(o => o.DateCreated)
            .ToListAsync();
        }
        public async Task<Order> GetAsync(int id)
        {
            var answer = await _context.Set<Order>()
                .Where(a => a.Id == id && a.IsDeleted!)
                .SingleOrDefaultAsync();
            return answer;
        }

        public async Task<Order> GetAsync(Expression<Func<Order, bool>> exp)
        {
            var answer = await _context.Set<Order>()
                .Where(a =>  a.IsDeleted!)
                .SingleOrDefaultAsync(exp);
            return answer;
        }

        public void Remove(Order order)
        {
            order.IsDeleted = true;
            _context.Set<Order>().Update(order);
        }

        public Order Update(Order order)
        {
            _context.Set<Order>().Update(order);
            return order;
        }
    }
}
