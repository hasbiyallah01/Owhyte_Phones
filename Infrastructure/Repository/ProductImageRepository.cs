using Microsoft.EntityFrameworkCore;
using Owhytee_Phones.Core.Application.Interface.Repository;
using Owhytee_Phones.Core.Domain.Entity;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Owhytee_Phones.Infrastructure.Repository
{
    public class ProductImageRepository : IProductImageRepository
    {
        private OwhyteeContext _context;
        public ProductImageRepository(OwhyteeContext context)
        {
            _context = context;
        }
        public async Task<ProductImage> AddAsync(ProductImage productImage)
        {
            await _context.Set<ProductImage>()
                .AddAsync(productImage);
            return productImage;
        }

        public async Task<bool> ExistAsync(int id)
        {
            return await _context.ProductImages.AnyAsync(x => x.Id == id);
        }

        public async Task<ICollection<ProductImage>> GetAllAsync()
        {
            return await _context.Set<ProductImage>().ToListAsync();
        }

        public async Task<ICollection<ProductImage>> GetAllAsync(int productId, int imageId)
        {
            return await _context.Set<ProductImage>()
                .Where(a => a.ProductId == productId && a.Id == imageId)
                .ToListAsync();
        }

        public async Task<ProductImage> GetAsync(int id)
        {
            return await _context.Set<ProductImage>()
                .Where(x => x.Id == id && x.IsDeleted!)
                .SingleOrDefaultAsync();
        }

        public async Task<ProductImage> GetAsync(Expression<Func<ProductImage, bool>> exp)
        {
            return await _context.Set<ProductImage>()
                .Where(x => x.IsDeleted!)
                .SingleOrDefaultAsync(exp);
        }

        public void Remove(ProductImage productImage)
        {
            productImage.IsDeleted = true;
            _context.Set<ProductImage>()
                .Update(productImage);
        }

        public ProductImage Update(ProductImage productImage)
        {
            _context.Set<ProductImage>()
                .Update(productImage);
            return productImage;
        }
    }
}
