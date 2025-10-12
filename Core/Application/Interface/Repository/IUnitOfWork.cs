namespace Owhytee_Phones.Core.Application.Interface.Repository
{
    public interface IUnitOfWork
    {
        Task<int> SaveAsync();
    }
}