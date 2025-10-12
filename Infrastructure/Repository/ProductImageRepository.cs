using Owhytee_Phones.Core.Application.Interface.Repository;
using Owhytee_Phones.Core.Domain.Entity;
using System.Linq.Expressions;

namespace Owhytee_Phones.Infrastructure.Repository
{
    public class ProductImageRepository : IProductImageRepository
    {
        public Task<ProductImage> AddAsync(ProductImage productImage)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<ProductImage>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ProductImage> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ProductImage> GetAsync(Expression<Func<ProductImage, bool>> exp)
        {
            throw new NotImplementedException();
        }

        public void Remove(ProductImage productImage)
        {
            throw new NotImplementedException();
        }

        public ProductImage Update(ProductImage productImage)
        {
            throw new NotImplementedException();
        }
    }
}
