using Owhytee_Phones.Models.CooperativeModel;
using Owhytee_Phones.Models.OrderModel;

namespace Owhytee_Phones.Core.Application.Interface.Service;

public interface ICooperativeService
{
    Task<List<CooperativeResponse>> GetCooperativesAsync(bool activeOnly = true);
    Task<CooperativeResponse?> GetCooperativeByIdAsync(int id);
    Task<CooperativeResponse> CreateCooperativeAsync(CooperativeRequest createCooperativeDto);
    Task<CooperativeResponse?> UpdateCooperativeAsync(int id, UpdateCooperativeRequest updateCooperativeDto);
    Task<bool> DeleteCooperativeAsync(int id);
    Task<List<OrderResponse>> GetCooperativeOrdersAsync(int cooperativeId);
    Task<int?> AutoAssignCooperativeAsync();
}