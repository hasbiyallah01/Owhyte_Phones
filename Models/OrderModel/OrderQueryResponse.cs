using Owhytee_Phones.Core.Domain.Enum;

namespace Owhytee_Phones.Models.OrderModel
{
    public class OrderQueryResponse
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 20;
        public int? CooperativeId { get; set; }
        public OrderStatus? Status { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? CustomerName { get; set; }
        public string? SearchTerm { get; set; } 
    }
}
