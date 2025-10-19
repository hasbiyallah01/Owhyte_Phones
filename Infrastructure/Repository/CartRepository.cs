using Microsoft.EntityFrameworkCore;
using Owhytee_Phones.Core.Application.Interface.Repository;
using Owhytee_Phones.Core.Domain.Entity;
using System.Linq.Expressions;

namespace Owhytee_Phones.Infrastructure.Repository
{
    public class CartRepository : ICartRepository
    {
        private OwhyteeContext _context;
        public CartRepository(OwhyteeContext context)
        {
            _context = context;
        }

        public async Task<Cart> AddAsync(Cart cart)
        {
            await _context.Set<Cart>().AddAsync(cart);
            return cart;
        }
        public async Task<Preference> AddAsync(Preference preference)
        {
            await _context.Set<Preference>().AddAsync(preference);
            return preference;
        }
        public async Task<bool> ExistAsync(int id)
        {
            return await _context.Carts.AnyAsync(a => a.Id == id);
        }

        public async Task<ICollection<Cart>> GetAllAsync()
        {
            var cart = await _context.Set<Cart>().ToListAsync();
            return cart;
        }

        public async Task<Cart> GetAsync(int id)
        {
            var cart = await _context.Set<Cart>()
                .Where(a => !a.IsDeleted && a.Id == id)
                .SingleOrDefaultAsync();
            return cart;
        }

        public async Task<Preference> GetAsync(string sessionId)
        {
            var preference = await _context.Set<Preference>()
                    .FirstOrDefaultAsync(p => p.SessionId == sessionId);
            return preference;
        }

        public async Task<Cart> GetAsync(Expression<Func<Cart, bool>> exp)
        {
            var cart = await _context.Set<Cart>()
                    .Where(a => !a.IsDeleted)
                    .SingleOrDefaultAsync(exp);
            return cart;
        }

        public void Remove(Cart cart)
        {
            cart.IsDeleted = true;
            _context.Set<Cart>().Update(cart);
            _context.SaveChanges();
        }

        public Cart Update(Cart cart)
        {
            _context.Carts.Update(cart);
            return cart;
        }
    }
}
