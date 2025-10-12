using Microsoft.EntityFrameworkCore;
using Owhytee_Phones.Core.Application.Interface.Repository;
using Owhytee_Phones.Core.Application.Interface.Service;
using Owhytee_Phones.Core.Domain.Entity;
using Owhytee_Phones.Core.Domain.Enum;
using Owhytee_Phones.Infrastructure.Repository;
using Owhytee_Phones.Models.OrderModel;
using Owhytee_Phones.Models.ProductModel;

namespace Owhytee_Phones.Core.Application.Interface.Service;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IOrderItemRepository _orderItemRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProductRepository _productRepository;
    private readonly IProductVariantRepository _productVariantRepository;
    private readonly ICooperativeRepository _cooperativeRepository;

    public OrderService(IOrderRepository orderRepository, IUnitOfWork unitOfWork, IProductRepository productRepository, 
        IProductVariantRepository productVariantRepository, IOrderItemRepository orderItemRepository, ICooperativeRepository cooperativeRepository)
    {
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
        _productRepository = productRepository;
        _productVariantRepository = productVariantRepository;
        _orderItemRepository = orderItemRepository;
        _cooperativeRepository = cooperativeRepository;
    }

    public async Task<OrderResponse> CreateOrderAsync(OrderRequest createOrderDto)
    {
        
        try
        {
            var order = new Order
            {
                CustomerName = createOrderDto.CustomerName,
                WhatsAppNumber = createOrderDto.WhatsAppNumber,
                Email = createOrderDto.Email,
                DeliveryAdress = createOrderDto.DeliveryAddress,
                CooperativeId = createOrderDto.CooperativeId,
                Status = Owhytee_Phones.Core.Domain.Enum.OrderStatus.AssignedToCooperative
            };
            await _orderRepository.AddAsync(order);
            await _unitOfWork.SaveAsync();

            decimal totalAmount = 0;
            foreach (var itemDto in createOrderDto.OrderItemRequests)
            {
                var product = await _productRepository.GetAsync(itemDto.ProductId);
                if (product == null)
                    throw new KeyNotFoundException($"Product with ID {itemDto.ProductId} not found");

                decimal unitPrice = product.Price;
                
                if (itemDto.ProductVariantId.HasValue)
                {
                    var variant = await _productVariantRepository.GetAsync(itemDto.ProductVariantId.Value);
                    if (variant == null)
                        throw new KeyNotFoundException($"Product variant with ID {itemDto.ProductVariantId} not found");
                    
                    unitPrice += variant.PriceAdjustment;
                }

                var orderItem = new OrderItem
                {
                    OrderId = order.Id,
                    ProductId = itemDto.ProductId,
                    VariantId = itemDto.ProductVariantId,
                    Quantity = itemDto.Quantity,
                    UnitPrice = unitPrice,
                    TotalPrice = unitPrice * itemDto.Quantity
                };

                totalAmount += orderItem.TotalPrice;
                await _orderItemRepository.AddAsync(orderItem);
            }

            order.TotalAmount = totalAmount;
            await _unitOfWork.SaveAsync();

            return await GetOrderByIdAsync(order.Id) ?? throw new InvalidOperationException("Failed to retrieve created order");
        }
        catch
        {
            throw;
        }
    }

    public async Task<OrderResponse?> GetOrderByIdAsync(int id)
    {
        var order = await _orderRepository.GetAsync(id);

        return order != null ? MapToOrderDto(order) : null;
    }

    public async Task<PagedResult<OrderResponse>> GetOrdersAsync(OrderFilterResponse filter, int page = 1, int pageSize = 20)
    {
        var query = await _orderRepository.GetAllAsync();

        if (filter.CooperativeId.HasValue)
            query = query.Where(o => o.CooperativeId == filter.CooperativeId.Value);

        if (filter.Status.HasValue)
            query = query.Where(o => o.Status == filter.Status.Value);

        if (filter.StartDate.HasValue)
            query = query.Where(o => o.DateCreated >= filter.StartDate.Value);

        if (filter.EndDate.HasValue)
            query = query.Where(o => o.DateCreated <= filter.EndDate.Value);

        if (!string.IsNullOrEmpty(filter.CustomerName))
            query = query.Where(o => o.CustomerName.Contains(filter.CustomerName));

        if (!string.IsNullOrEmpty(filter.SearchTerm))
            query = query.Where(o => o.CustomerName.Contains(filter.SearchTerm) || 
                                   o.WhatsAppNumber.Contains(filter.SearchTerm));

        var totalCount = query.Count();
        var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

        var orders = query
            .OrderByDescending(o => o.DateCreated)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var orderDtos = orders.Select(MapToOrderDto).ToList();

        return new PagedResult<OrderResponse>
        {
            Items = orderDtos,
            TotalCount = totalCount,
            Page = page,
            PageSize = pageSize,
            TotalPages = totalPages,
            HasNextPage = page < totalPages,
            HasPreviousPage = page > 1
        };
    }

    public async Task<List<OrderResponse>> GetOrdersByCooperativeAsync(int cooperativeId)
    {
        var order = await _orderRepository.GetAllAsync(cooperativeId);

        return order.Select(MapToOrderDto).ToList();
    }

    public async Task<OrderResponse?> UpdateOrderStatusAsync(int id, OrderStatus status)
    {
        var order = await _orderRepository.GetAsync(id);
        if (order == null)
            return null;

        order.Status = status;
        await _unitOfWork.SaveAsync();

        return await GetOrderByIdAsync(id);
    }

    public async Task<WhatsAppMessageRequest> GenerateWhatsAppMessageAsync(int orderId)
    {
        var order = await _orderRepository.GetAsync(orderId);

        if (order == null || order.Cooperative == null)
            throw new KeyNotFoundException("Order or cooperative not found");

        var message = GenerateWhatsAppMessageText(order);
        
        return new WhatsAppMessageRequest
        {
            CooperativeName = order.Cooperative.Name,
            WhatsAppNumber = order.Cooperative.WhatsAppNumber,
            Message = message
        };
    }

    public async Task<OrderResponse?> AssignOrderToCooperativeAsync(int orderId, int cooperativeId)
    {
        var order = await _orderRepository.GetAsync(orderId);
        if (order == null)
            return null;

        var cooperative = await _cooperativeRepository.GetAsync(cooperativeId);
        if (cooperative == null || !cooperative.IsActive)
            return null;

        order.CooperativeId = cooperativeId;
        order.Status = OrderStatus.AssignedToCooperative;
        _cooperativeRepository.Update(cooperative);
        await _unitOfWork.SaveAsync();

        return await GetOrderByIdAsync(orderId);
    }

    public async Task<List<OrderResponse>> GetOrdersByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        var orders = await _orderRepository.GetAllAsync();

        return orders.Select(MapToOrderDto).ToList();
    }

    private OrderResponse MapToOrderDto(Order order)
    {
        return new OrderResponse
        {
            Id = order.Id,
            CustomerName = order.CustomerName,
            WhatsAppNumber = order.WhatsAppNumber,
            Email = order.Email,
            DeliveryAddress = order.DeliveryAdress,
            TotalAmount = order.TotalAmount,
            Status = order.Status,
            CreatedAt = order.DateCreated,
            Cooperative = order.Cooperative != null ? new Owhytee_Phones.Models.CooperativeModel.CooperativeResponse
            {
                Id = order.Cooperative.Id,
                Name = order.Cooperative.Name,
                WhatsAppNumber = order.Cooperative.WhatsAppNumber,
                Email = order.Cooperative.Email,
                Address = order.Cooperative.Address,
                IsActive = order.Cooperative.IsActive,
                Created = order.Cooperative.DateCreated
            } : null,
            OrderItems = order.OrderItems.Select(oi => new OrderItemResponse
            {
                Id = oi.Id,
                ProductId = oi.ProductId,
                ProductName = oi.Product.Name,
                ProductVariantId = oi.VariantId,
                VariantDetails = oi.Variant != null ? 
                    $"{oi.Variant.Color} - {oi.Variant.Storage}" : null,
                Quantity = oi.Quantity,
                UnitPrice = oi.UnitPrice,
                TotalPrice = oi.TotalPrice
            }).ToList()
        };
    }

    private string GenerateWhatsAppMessageText(Order order)
    {
        var items = string.Join("\n", order.OrderItems.Select(oi => 
        {
            var variant = oi.Variant != null ? 
                $" ({oi.Variant.Storage}, {oi.Variant.Color})" : "";
            return $"- {oi.Product.Name}{variant} x{oi.Quantity} - ₦{oi.TotalPrice:N0}";
        }));

        return $@"Hello {order.Cooperative!.Name},
                A customer has placed an order via the store.
                Customer Name: {order.CustomerName}
                WhatsApp: {order.WhatsAppNumber}
                {(string.IsNullOrEmpty(order.Email) ? "" : $"Email: {order.Email}\n")}{(string.IsNullOrEmpty(order.DeliveryAdress) ? "" : $"Delivery Address: {order.DeliveryAdress}\n")}
                Order Items:
                {items}
                Total Amount: ₦{order.TotalAmount:N0}

                Please confirm availability and payment instructions.";
    }
}