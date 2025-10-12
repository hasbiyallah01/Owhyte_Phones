using Owhytee_Phones.Core.Domain.Enum;
using Owhytee_Phones.Models.OrderModel;
using Owhytee_Phones.Models.ProductModel;

namespace Owhytee_Phones.Core.Application.Interface.Service;

public interface IOrderService
{
    Task<OrderResponse> CreateOrderAsync(OrderRequest createOrderDto);
    Task<OrderResponse?> GetOrderByIdAsync(int id);
    Task<PagedResult<OrderResponse>> GetOrdersAsync(OrderFilterResponse filter, int page = 1, int pageSize = 20);
    Task<List<OrderResponse>> GetOrdersByCooperativeAsync(int cooperativeId);
    Task<OrderResponse?> UpdateOrderStatusAsync(int id, OrderStatus status);
    Task<WhatsAppMessageRequest> GenerateWhatsAppMessageAsync(int orderId);
    Task<OrderResponse?> AssignOrderToCooperativeAsync(int orderId, int cooperativeId);
    Task<List<OrderResponse>> GetOrdersByDateRangeAsync(DateTime startDate, DateTime endDate);
}