using Owhytee_Phones.Core.Application.Interface.Repository;
using Owhytee_Phones.Core.Domain.Entity;
using System.Linq.Expressions;

namespace Owhytee_Phones.Infrastructure.Repository
{
    public class ProductVariantRepository : IProductVariantRepository
    {
        public Task<ProductVariant> AddAsync(ProductVariant variant)
        {
            throw new NotImplementedException();
        }


        public Task<bool> ExistAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<ProductVariant>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ProductVariant> GetAsync(int id)
        {
            throw new NotImplementedException();
        }


        public Task<ProductVariant> GetAsync(Expression<Func<ProductVariant, bool>> exp)
        {
            throw new NotImplementedException();
        }

        public void Remove(ProductVariant varaint)
        {
            throw new NotImplementedException();
        }

        public ProductVariant Update(ProductVariant varaint)
        {
            throw new NotImplementedException();
        }
    }
}
