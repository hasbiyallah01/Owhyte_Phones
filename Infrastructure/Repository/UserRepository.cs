using Microsoft.EntityFrameworkCore;
using Owhytee_Phones.Core.Application.Interface.Repository;
using Owhytee_Phones.Core.Domain.Entity;
using System.Linq.Expressions;

namespace Owhytee_Phones.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly OwhyteeContext _context;

        public UserRepository(OwhyteeContext context)
        {
            _context = context;
        }

        public async Task<User> AddAsync(User user)
        {
           await _context.Set<User>().AddAsync(user);
            return user;
        }

        public async Task<bool> ExistAsync(string Email)
        {
            return await _context.Users.AnyAsync(x => x.Email == Email);
        }

        public async Task<bool> ExistAsync(string Email, int Id)
        {
            return await (_context.Users.AnyAsync(x => x.Email == Email && x.Id == Id));
        }

        public User Update(User user)
        {
            _context.Users.Update(user);
            return user;
        }

        public async Task<ICollection<User>> GetAllAsync()
        {
            var answer = await _context.Set<User>().ToListAsync();
            return answer;
        }

        public async Task<User> GetAsync(string Email)
        {
            var answer = await _context.Set<User>()
                        .Where(a => !a.IsDeleted && a.Email == Email)
                        .SingleOrDefaultAsync();

            return answer;
        }
        public async Task<User> GetAsync(int Id)
        {
            var answer = await _context.Set<User>()
                        .Where(a => !a.IsDeleted && a.Id == Id)
                        .SingleOrDefaultAsync();

            return answer;
        }

        public async Task<User> GetAsync(Expression<Func<User, bool>> exp)
        {
            var answer = await _context.Set<User>()
                            .Where(a => !a.IsDeleted) 
                            .SingleOrDefaultAsync(exp);
            return answer;
        }

        public void Remove(User user)
        {
            user.IsDeleted = true;
            _context.Set<User>()
                .Update(user);
            _context.SaveChanges();
        }
    }
}
