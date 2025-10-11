namespace Owhytee_Phones.Models.AuthModel
{
    public class AuthResponse
    {
        public string Tokem { get; set; } = default!;
        public DateTime ExpiresAt { get; set; }
        public UserResponse UserResponse { get; set; } = default!;
    }
}
