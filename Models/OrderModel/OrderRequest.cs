using Owhytee_Phones.Core.Domain.Enum;
using Owhytee_Phones.Models.CooperativeModel;

namespace Owhytee_Phones.Models.OrderModel
{
    public class OrderRequest
    {
        public string CustomerName { get; set; } = default!;
        public string WhatsAppNumber { get; set; } = default!;
        public string? Email { get; set; }
        public string? DeliveryAddress { get; set; }
        public int CooperativeId { get; set; }
        public List<OrderItemRequest> OrderItemRequests { get; set; } = new List<OrderItemRequest>(); 

    }
}
