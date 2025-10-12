using Owhytee_Phones.Core.Application.Interface.Repository;

namespace Owhytee_Phones.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly OwhyteeContext _context;

        public UnitOfWork(OwhyteeContext context)
        {
            _context = context;
        }
        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
