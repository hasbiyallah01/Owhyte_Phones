namespace Owhytee_Phones.Core.Domain.Entity
{
    public class Cooperative : Auditables
    {
        public string Name { get; set; }
        public string WhatsAppNumber { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public bool IsActive { get; set; }

        public ICollection<OrderItem> Items { get; set; }  = new List<OrderItem>();
    }
}
