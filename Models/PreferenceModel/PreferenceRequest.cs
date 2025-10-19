namespace Owhytee_Phones.Models.PreferenceModel
{
    public class PreferenceRequest
    {
        public int Id { get; set; }
        public string SessionId { get; set; } = string.Empty;
        public bool MagicModeEnabled { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

}
