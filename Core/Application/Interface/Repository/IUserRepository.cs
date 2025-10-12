using Owhytee_Phones.Core.Domain.Entity;
using Owhytee_Phones.Models.AuthModel;
using System.Linq.Expressions;

namespace Owhytee_Phones.Core.Application.Interface.Repository
{
    public interface IUserRepository
    {
        Task<User> AddAsync(User user);
        Task<User> GetAsync(int id);
        Task<User> GetAsync(string Email);
        Task<User> GetAsync(Expression<Func<User, bool>> exp);
        Task<ICollection<User>> GetAllAsync();
        void Remove(User user);
        User Update(User user);
        Task<bool> ExistAsync(string Email, int Id);
        Task<bool> ExistAsync(string email);
    }
}
