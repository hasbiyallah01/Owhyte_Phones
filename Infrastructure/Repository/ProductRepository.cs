using Microsoft.EntityFrameworkCore;
using Owhytee_Phones.Core.Application.Interface.Repository;
using Owhytee_Phones.Core.Domain.Entity;
using System.Linq.Expressions;

namespace Owhytee_Phones.Infrastructure.Repository
{
    public class ProductRepository : IProductRepository
    {
        private OwhyteeContext _context;
        public ProductRepository(OwhyteeContext context)
        {
            _context = context;
        }
        public async Task<Product> AddAsync(Product product)
        {
            await _context.Set<Product>().AddAsync(product);
            return product;
        }

        public async Task<bool> ExistAsync(int id)
        {
            return await _context.Products.AnyAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Set<Product>().ToListAsync();
        }

        public async Task<Product> GetAsync(int id)
        {
            var result = await _context.Set<Product>()
                .Where(p => p.Id == id && p.IsDeleted!)
                .SingleOrDefaultAsync();
            return result;
        }

        public async Task<Product> GetAsync(Expression<Func<Product, bool>> exp)
        {
            var result = await _context.Set<Product>()
                .Where(p => p.IsDeleted!)
                .SingleOrDefaultAsync(exp);
            return result;
        }

        public void Remove(Product product)
        {
            product.IsDeleted = true;
            _context.Set<Product>().Update(product);
        }

        public Product Update(Product product)
        {
            _context.Set<Product>().Update(product); 
            return product;
        }
    }
}
