using Microsoft.EntityFrameworkCore;
using Owhytee_Phones.Core.Application.Interface.Repository;
using Owhytee_Phones.Core.Domain.Entity;
using System.Linq.Expressions;

namespace Owhytee_Phones.Infrastructure.Repository
{
    public class CartItemRepository : ICartItemRepository
    {
        private OwhyteeContext _context;
        public CartItemRepository(OwhyteeContext context)
        {
            _context = context;
        }
        public async Task<CartItem> AddAsync(CartItem cartItem)
        {
            await _context.Set<CartItem>().AddAsync(cartItem);
            return cartItem;
        }

        public async Task<bool> ExistAsync(int id)
        {
            return await _context.CartItems.AnyAsync(a => a.Id == id);
        }

        public async Task<ICollection<CartItem>> GetAllAsync()
        {
            return await _context.Set<CartItem>().ToListAsync();
        }

        public async Task<CartItem> GetAsync(int id)
        {
            return await _context.Set<CartItem>()
                .Where(a => !a.IsDeleted && a.Id == id)
                .SingleOrDefaultAsync();
        }

        public async Task<CartItem> GetAsync(Expression<Func<CartItem, bool>> exp)
        {
            return await _context.Set<CartItem>()
                .Where(a => !a.IsDeleted)
                .SingleOrDefaultAsync(exp);
        }

        public void Remove(CartItem cartItem)
        {
            cartItem.IsDeleted = true;
            _context.Set<CartItem>()
                .Update(cartItem);
        }

        public CartItem Update(CartItem cartItem)
        {
            var result = _context.CartItems.Update(cartItem);
            return cartItem;
        }
    }
}
