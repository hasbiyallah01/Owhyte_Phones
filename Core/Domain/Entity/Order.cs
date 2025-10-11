using Owhytee_Phones.Core.Domain.Enum;

namespace Owhytee_Phones.Core.Domain.Entity
{
    public class Order : Auditables
    {
        public string CustomerName { get; set; } = default!;
        public string WhatsAppNumber { get; set; } = default!;
        public string? Email { get; set; } = default!;
        public string? DeliveryAdress { get; set; } = default!;
        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        public int? CooperativeId { get; set; }
        public Cooperative? Cooperative { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
