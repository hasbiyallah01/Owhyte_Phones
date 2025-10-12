using Owhytee_Phones.Core.Application.Interface.Repository;
using Owhytee_Phones.Core.Domain.Entity;
using System.Linq.Expressions;

namespace Owhytee_Phones.Infrastructure.Repository
{
    public class CooperativeRepository : ICooperativeRepository
    {
        public Task<Cooperative> AddAsync(Cooperative cooperative)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Cooperative>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Cooperative> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Cooperative> GetAsync(Expression<Func<Cooperative, bool>> exp)
        {
            throw new NotImplementedException();
        }

        public void Remove(Cooperative cooperative)
        {
            throw new NotImplementedException();
        }

        public Cooperative Update(Cooperative cooperative)
        {
            throw new NotImplementedException();
        }
    }
}
