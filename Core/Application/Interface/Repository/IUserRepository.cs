using Owhytee_Phones.Models.AuthModel;

namespace Owhytee_Phones.Core.Application.Interface.Repository
{
    public interface IUserRepository
    {
        Task<AuthResponse> LoginAsync (LoginRequest loginRequest);
        Task<bool> ChangePassword (int userId, ChangePasswordRequest changePasswordRequest);
        Task<UserResponse> GetUserById(int userId);
    }
}
