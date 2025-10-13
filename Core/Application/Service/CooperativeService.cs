using Microsoft.EntityFrameworkCore;
using Owhytee_Phones.Core.Application.Interface.Repository;
using Owhytee_Phones.Core.Domain.Entity;
using Owhytee_Phones.Infrastructure.Repository;
using Owhytee_Phones.Models.CooperativeModel;
using Owhytee_Phones.Models.OrderModel;
using Owhytee_Phones.Core.Application.Interface.Service;

namespace Owhytee_Phones.Core.Application.Interface.Service;

public class CooperativeService : ICooperativeService
{
    private readonly ICooperativeRepository _cooperativeRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IOrderRepository _orderRepository;

    public CooperativeService(ICooperativeRepository cooperativeRepository, IUnitOfWork unitOfWork, IOrderRepository orderRepository)
    {
        _cooperativeRepository = cooperativeRepository;
        _unitOfWork = unitOfWork;
        _orderRepository = orderRepository;
    }

    public async Task<List<CooperativeResponse>> GetCooperativesAsync(bool activeOnly = true)
    {
        var query = await _cooperativeRepository.GetAllAsync();
        
        if (activeOnly)
            query = query.Where(c => c.IsActive);

        var cooperatives = query.OrderBy(c => c.Name).ToList();

        return cooperatives.Select(MapToCooperativeDto).ToList();
    }

    public async Task<CooperativeResponse?> GetCooperativeByIdAsync(int id)
    {
        var cooperative = await _cooperativeRepository.GetAsync(id);
        if (cooperative == null)
            return null;

        return cooperative != null ? MapToCooperativeDto(cooperative) : null;
    }

    public async Task<CooperativeResponse> CreateCooperativeAsync(CooperativeRequest createCooperativeDto)
    {
        var cooperative = new Cooperative
        {
            Name = createCooperativeDto.Name,
            WhatsAppNumber = createCooperativeDto.WhatsAppNumber,
            Email = createCooperativeDto.Email,
            Address = createCooperativeDto.Address
        };
        await _cooperativeRepository.AddAsync(cooperative);
        await _unitOfWork.SaveAsync();

        return MapToCooperativeDto(cooperative);
    }

    public async Task<CooperativeResponse?> UpdateCooperativeAsync(int id, UpdateCooperativeRequest updateCooperativeDto)
    {
        var cooperative = await _cooperativeRepository.GetAsync(id);
        if (cooperative == null)
            return null;

        cooperative.Name = updateCooperativeDto.Name;
        cooperative.WhatsAppNumber = updateCooperativeDto.WhatsAppNumber;
        cooperative.Email = updateCooperativeDto.Email;
        cooperative.Address = updateCooperativeDto.Address;
        cooperative.IsActive = updateCooperativeDto.IsActive;
        _cooperativeRepository.Update(cooperative);
        await _unitOfWork.SaveAsync();

        return MapToCooperativeDto(cooperative);
    }

    public async Task<bool> DeleteCooperativeAsync(int id)
    {
        var cooperative = await _cooperativeRepository.GetAsync(id);
        if (cooperative == null)
            return false;

        cooperative.IsActive = false;
        _cooperativeRepository.Remove(cooperative);
        await _unitOfWork.SaveAsync();

        return true;
    }

    public async Task<List<OrderResponse>> GetCooperativeOrdersAsync(int cooperativeId)
    {
        var orders = await _orderRepository.GetAllAsync(cooperativeId);

        return orders.Select(order => new OrderResponse
        {
            Id = order.Id,
            CustomerName = order.CustomerName,
            WhatsAppNumber = order.WhatsAppNumber,
            Email = order.Email,
            DeliveryAddress = order.DeliveryAdress,
            TotalAmount = order.TotalAmount,
            Status = order.Status,
            CreatedAt = order.DateCreated,
            Cooperative = order.Cooperative != null ? MapToCooperativeDto(order.Cooperative) : null,
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
        }).ToList();
    }

    public async Task<int?> AutoAssignCooperativeAsync()
    {
        var cooperatives = await _cooperativeRepository.GetAllAsync();
        var activeCoops = cooperatives.Where(c => c.IsActive).ToList();

        if (!activeCoops.Any())
            return null;

        var orderCounts = await _orderRepository.GetOrderCountsByCooperativeAsync();

        var selectedCoop = activeCoops
            .Select(c => new
            {
                c.Id,
                Count = orderCounts.ContainsKey(c.Id) ? orderCounts[c.Id] : 0
            })
            .OrderBy(c => c.Count)
            .FirstOrDefault();

        return selectedCoop?.Id;
    }

    private CooperativeResponse MapToCooperativeDto(Cooperative cooperative)
    {
        return new CooperativeResponse
        {
            Id = cooperative.Id,
            Name = cooperative.Name,
            WhatsAppNumber = cooperative.WhatsAppNumber,
            Email = cooperative.Email,
            Address = cooperative.Address,
            IsActive = cooperative.IsActive,
            Created = cooperative.DateCreated
        };
    }
}