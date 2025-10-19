using Owhytee_Phones.Core.Domain.Enum;

namespace Owhytee_Phones.Core.Domain.Entity
{
    public class User : Auditables
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool IsAdmin { get; set; }
        public Role Role { get; set; } = Role.User;
        public Preference? UserPreferences { get; set; }
    }

    public class Preference
    {
        public int Id { get; set; }
        public string SessionId { get; set; } = string.Empty;
        public bool MagicModeEnabled { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
    public class CloudinarySettings
    {
        public string CloudName { get; set; } = string.Empty;
        public string ApiKey { get; set; } = string.Empty;
        public string ApiSecret { get; set; } = string.Empty;
    }
}
