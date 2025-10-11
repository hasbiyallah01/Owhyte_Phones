using Owhytee_Phones.Core.Domain.Enum;
using Owhytee_Phones.Models.CooperativeModel;

namespace Owhytee_Phones.Models.OrderModel
{
    public class OrderResponse
    {
        public int Id { get; set; }
        public string CustomerName { get; set; } = default!;
        public string WhatsAppNumber { get; set; } = default!;
        public string? Email { get; set; }
        public string? DeliveryAddress { get; set; }
        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public CooperativeResponse? CooperativeResponse { get; set; }
        public List<OrderItemResponse> Items { get; set; } = new List<OrderItemResponse>();
    }

    public class UpdateOrderStatusDto
    {
        public OrderStatus Status { get; set; }
    }

    public class AssignOrder
    {
        public int CooperativeId { get; set; }
    }
}
