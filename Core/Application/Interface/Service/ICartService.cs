using Owhytee_Phones.Models.CartModel;

namespace Owhytee_Phones.Core.Application.Interface.Service;

public interface ICartService
{
    Task<CartResponse> GetCartAsync(string sessionId);
    Task<CartResponse> AddToCartAsync(string sessionId, CartRequest addToCartDto);
    Task<CartResponse> UpdateCartItemAsync(string sessionId, int cartItemId, int quantity);
    Task<CartResponse> RemoveFromCartAsync(string sessionId, int cartItemId);
    Task<bool> ClearCartAsync(string sessionId);
    Task<decimal> GetCartTotalAsync(string sessionId);
}