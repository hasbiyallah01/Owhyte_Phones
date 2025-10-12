using Microsoft.EntityFrameworkCore;
using Owhytee_Phones.Core.Application.Interface.Repository;
using Owhytee_Phones.Core.Domain.Entity;
using System.Linq.Expressions;

namespace Owhytee_Phones.Infrastructure.Repository
{
    public class CooperativeRepository : ICooperativeRepository
    {
        private OwhyteeContext _context;
        public CooperativeRepository(OwhyteeContext context)
        {
            _context = context;
        }
        public async Task<Cooperative> AddAsync(Cooperative cooperative)
        {
            await _context.Set<Cooperative>().
                AddAsync(cooperative);
            return cooperative;
        }

        

        public async Task<bool> ExistAsync(int id)
        {
            return await _context.Cooperatives.AnyAsync(a  => a.Id == id);
        }

        public async Task<IEnumerable<Cooperative>> GetAllAsync()
        {
            return await _context.Set<Cooperative>().ToListAsync();
        }

        public async Task<Cooperative> GetAsync(int id)
        {
            var cooperative = await _context.Set<Cooperative>()
                .Where(a => a.Id == id)
                .SingleOrDefaultAsync();
            return cooperative;
        }

        public async Task<Cooperative> GetAsync(Expression<Func<Cooperative, bool>> exp)
        {
            var cooperative = await _context.Set<Cooperative>()
                .Where(a => a.IsDeleted)
                .SingleOrDefaultAsync(exp);
            return cooperative;
        }

        public void Remove(Cooperative cooperative)
        {
            cooperative.IsDeleted = true;
            _context.Set<Cooperative>()
                .Update(cooperative);
        }

        public Cooperative Update(Cooperative cooperative)
        {
            _context.Set<Cooperative>()
                .Update(cooperative);
            return cooperative;
        }
    }
}
