namespace Owhytee_Phones.Models.CooperativeModel
{
    public class CooperativeResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string WhatsAppNumber { get; set; } = default!;
        public string? Email { get; set; }
        public string? Address { get; set; }
        public bool IsActive { get; set; }
        public DateTime? Created { get; set; }
    }
}
