using Owhytee_Phones.Models.AuthModel;

namespace Owhytee_Phones.Core.Application.Interface.Service
{
    public interface IAuthService
    {
        Task<AuthResponse> LoginAsync(LoginRequest loginRequest);
        Task<bool> ChangePasswordAsync(int userId, ChangePasswordRequest changePasswordRequest);
        Task<UserResponse> GetUserIdAsync(int userId);
    }
}
