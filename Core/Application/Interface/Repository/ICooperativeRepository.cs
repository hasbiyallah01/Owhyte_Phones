using Owhytee_Phones.Core.Domain.Entity;
using System.Linq.Expressions;

namespace Owhytee_Phones.Core.Application.Interface.Repository
{
    public interface ICooperativeRepository
    {
        Task<Cooperative> AddAsync(Cooperative cooperative);
        Task<Cooperative> GetAsync(int id);
        Task<Cooperative> GetAsync(Expression<Func<Cooperative, bool>> exp);
        Task<ICollection<Cooperative>> GetAllAsync();
        void Remove(Cooperative cooperative);
        Cooperative Update(Cooperative cooperative);
        Task<bool> ExistAsync(int id);
    }
}
