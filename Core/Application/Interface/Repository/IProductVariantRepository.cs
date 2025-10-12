using Owhytee_Phones.Core.Domain.Entity;
using System.Linq.Expressions;

namespace Owhytee_Phones.Core.Application.Interface.Repository
{
    public interface IProductVariantRepository
    {
        Task<ProductVariant> AddAsync(ProductVariant variant);
        Task<ProductVariant> GetAsync(int id);
        Task<ProductVariant> GetAsync(Expression<Func<ProductVariant, bool>> exp);
        Task<ICollection<ProductVariant>> GetAllAsync();
        void Remove(ProductVariant varaint);
        ProductVariant Update(ProductVariant varaint);
        Task<bool> ExistAsync(int id);
    }
}
