using Owhytee_Phones.Core.Application.Interface.Service;
using Owhytee_Phones.Models.AuthModel;

namespace Owhytee_Phones.Core.Application.Service
{
    public class AuthService : IAuthService
    {
        public Task<bool> ChangePasswordAsync(int userId, ChangePasswordRequest changePasswordRequest)
        {
            throw new NotImplementedException();
        }

        public Task<UserResponse> GetUserIdAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<AuthResponse> LoginAsync(LoginRequest loginRequest)
        {
            throw new NotImplementedException();
        }
    }
}
