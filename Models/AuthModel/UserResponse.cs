using Owhytee_Phones.Core.Domain.Enum;

namespace Owhytee_Phones.Models.AuthModel
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string Email { get; set; } = default!;
        public Role Role { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastLoginAt { get; set; }
        public bool IsActive { get; set; }
    }
}
