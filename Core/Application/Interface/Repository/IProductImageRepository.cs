using Owhytee_Phones.Core.Domain.Entity;
using System.Linq.Expressions;

namespace Owhytee_Phones.Core.Application.Interface.Repository
{
    public interface IProductImageRepository
    {
        Task<ProductImage> AddAsync(ProductImage productImage);
        Task<ProductImage> GetAsync(int id);
        Task<ProductImage> GetAsync(Expression<Func<ProductImage, bool>> exp);
        Task<ICollection<ProductImage>> GetAllAsync();
        void Remove(ProductImage productImage);
        ProductImage Update(ProductImage productImage);
        Task<bool> ExistAsync(int id);
    }
}
