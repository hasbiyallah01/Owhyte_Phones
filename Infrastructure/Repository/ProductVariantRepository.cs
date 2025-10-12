using Microsoft.EntityFrameworkCore;
using Owhytee_Phones.Core.Application.Interface.Repository;
using Owhytee_Phones.Core.Domain.Entity;
using System.Linq.Expressions;

namespace Owhytee_Phones.Infrastructure.Repository
{
    public class ProductVariantRepository : IProductVariantRepository
    {
        private OwhyteeContext _context;
        public ProductVariantRepository(OwhyteeContext context)
        {
            _context = context;
        }
        public async Task<ProductVariant> AddAsync(ProductVariant variant)
        {
            await _context.Set<ProductVariant>()
                .AddAsync(variant);
            return variant;
        }


        public async Task<bool> ExistAsync(int id)
        {
            return await _context.ProductVariants.AnyAsync(a => a.Id == id);
        }

        public async Task<ICollection<ProductVariant>> GetAllAsync()
        {
            return await _context.Set<ProductVariant>().ToListAsync();
        }

        public async Task<ProductVariant> GetAsync(int id)
        {
            var answer = await _context.Set<ProductVariant>()
                .Where(a => a.Id == id && a.IsDeleted!)
                .SingleOrDefaultAsync();

            return answer;
        }


        public async Task<ProductVariant> GetAsync(Expression<Func<ProductVariant, bool>> exp)
        {
            var answer = await _context.Set<ProductVariant>()
                .Where(a => a.IsDeleted!)
                .SingleOrDefaultAsync(exp);
            return answer;
        }

        public async Task<ProductVariant> GetByProductIdAsync(int id)
        {
            var answer = await _context.Set<ProductVariant>()
                .Where(a => a.ProductId == id && a.IsDeleted!)
                .SingleOrDefaultAsync();

            return answer;
        }

        public async Task<IEnumerable<ProductVariant>> GetAllByProductIdAsync(int id)
        {
            var answer = await _context.Set<ProductVariant>()
                .Where(a => a.ProductId == id && a.IsDeleted!)
                .ToListAsync();

            return answer;
        }

        public void Remove(ProductVariant varaint)
        {
            varaint.IsDeleted = true;
            _context.Set<ProductVariant>()
                .Update(varaint);
        }

        public ProductVariant Update(ProductVariant varaint)
        {
            _context.Set<ProductVariant>()
                .Update(varaint);
            return varaint;
        }
    }
}
