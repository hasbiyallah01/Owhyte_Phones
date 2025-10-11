namespace Owhytee_Phones.Models.CooperativeModel
{
    public class CooperativeRequest
    {
        public string Name { get; set; } = default!;
        public string WhatsAppNumber { get; set; } = default!;
        public string? Email { get; set; } = default!;
        public string? Address { get; set; } = default!;
    }
}
