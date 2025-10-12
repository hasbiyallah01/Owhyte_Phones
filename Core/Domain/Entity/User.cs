using Owhytee_Phones.Core.Domain.Enum;

namespace Owhytee_Phones.Core.Domain.Entity
{
    public class User : Auditables
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool IsAdmin { get; set; }
        public Role Role { get; set; } = Role.User;
    }

    public class CloudinarySettings
    {
        public string CloudName { get; set; } = string.Empty;
        public string ApiKey { get; set; } = string.Empty;
        public string ApiSecret { get; set; } = string.Empty;
    }
}
