using Microsoft.EntityFrameworkCore;
using Owhytee_Phones.Core.Application.Interface.Repository;
using Owhytee_Phones.Core.Application.Interface.Service;
using Owhytee_Phones.Core.Domain.Entity;
using Owhytee_Phones.Infrastructure;
using Owhytee_Phones.Models.CartModel;

namespace Owhytee_Phones.Core.Application.Interface.Service;

public class CartService : ICartService
{
    private readonly OwhyteeContext _context;
    private readonly ICartRepository _cartRepository;
    private readonly ICartItemRepository _cartItemRepository;

    public CartService(OwhyteeContext context, ICartRepository cartRepository, ICartItemRepository cartItemRepository)
    {
        _context = context;
        _cartRepository = cartRepository;
        _cartItemRepository = cartItemRepository;
    }

    public async Task<CartResponse> GetCartAsync(string sessionId)
    {
        var cart = await _cartRepository.GetAsync(c => c.SessionId == sessionId);
        return await MapToCartDtoAsync(cart);
    }

    public async Task<CartResponse> AddToCartAsync(string sessionId, CartRequest addToCartDto)
    {
        var cart = await _cartRepository.GetAsync(c => c.SessionId == sessionId);
        if(cart == null)
        {
            cart = new Cart
            {
                SessionId = sessionId,
                IsDeleted = false
            };
            _context.Carts.Add(cart);
            await _context.SaveChangesAsync();
        }

        var existingItem = await _cartItemRepository.GetAsync(ci => ci.CartId== cart.Id &&
                                     ci.ProductId == addToCartDto.ProductId &&
                                     ci.VariantId == addToCartDto.ProductVariantId);

        if (existingItem != null)
        {
            existingItem.Quantity += addToCartDto.Quantity;
        }
        else
        {
            var cartItem = new CartItem
            {
                CartId = cart.Id,
                ProductId = addToCartDto.ProductId,
                VariantId = addToCartDto.ProductVariantId,
                Quantity = addToCartDto.Quantity
            };

            _context.CartItems.Add(cartItem);
        }

        cart.DateModified = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        return await MapToCartDtoAsync(cart);
    }

    public async Task<CartResponse> UpdateCartItemAsync(string sessionId, int cartItemId, int quantity)
    {
        var cart = await _cartRepository.GetAsync(c => c.SessionId == sessionId);
        if (cart == null)
        {
            var Item = new CartItem
            {
                CartId = cart.Id,
                Quantity = quantity,
                IsDeleted = false, 
            };
            _context.CartItems.Add(Item);
            await _context.SaveChangesAsync();
        }
        var cartItem = await _context.CartItems
            .FirstOrDefaultAsync(ci => ci.Id == cartItemId && ci.CartId == cart.Id);

        if (cartItem == null)
            throw new KeyNotFoundException("Cart item not found");

        if (quantity <= 0)
        {
            _context.CartItems.Remove(cartItem);
        }
        else
        {
            cartItem.Quantity = quantity;
        }

        cart.DateModified = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        return await MapToCartDtoAsync(cart);
    }

    public async Task<CartResponse> RemoveFromCartAsync(string sessionId, int cartItemId)
    {
        var cart = await _cartRepository.GetAsync(x => x.SessionId == sessionId);
        var cartItem = await _context.CartItems
            .FirstOrDefaultAsync(ci => ci.Id == cartItemId && ci.CartId == cart.Id);

        if (cartItem == null)
            throw new KeyNotFoundException("Cart item not found");

        _context.CartItems.Remove(cartItem);
        cart.DateModified = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        return await MapToCartDtoAsync(cart);
    }

    public async Task<bool> ClearCartAsync(string sessionId)
    {

        var cart = await _cartRepository.GetAsync(x => x.SessionId == sessionId);

        if (cart == null)
            return false;

        _context.CartItems.RemoveRange(cart.CartItems);
        cart.DateModified = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<decimal> GetCartTotalAsync(string sessionId)
    {
        var cart = await _cartRepository.GetAsync(x => x.SessionId == sessionId);
        var cartItems = await _context.CartItems
            .Include(ci => ci.Product)
            .Include(ci => ci.Variant)
            .Where(ci => ci.CartId == cart.Id)
            .ToListAsync();

        return cartItems.Sum(ci =>
        {
            var basePrice = ci.Product.Price;
            var variantAdjustment = ci.Variant?.PriceAdjustment ?? 0;
            return (basePrice + variantAdjustment) * ci.Quantity;
        });
    }


    private async Task<CartResponse> MapToCartDtoAsync(Cart cart)
    {
        var cartItems = await _context.CartItems
            .Include(ci => ci.Product)
                .ThenInclude(p => p.ProductImages)
            .Include(ci => ci.Variant)
            .Where(ci => ci.CartId == cart.Id)
            .ToListAsync();

        var cartItemDtos = cartItems.Select(ci =>
        {
            var basePrice = ci.Product.Price;
            var variantAdjustment = ci.Variant?.PriceAdjustment ?? 0;
            var unitPrice = basePrice + variantAdjustment;

            return new CartItemResponse
            {
                Id = ci.Id,
                ProductId = ci.ProductId,
                ProductName = ci.Product.Name,
                ProductBrand = ci.Product.Brand,
                ProductImageUrl = ci.Product.ProductImages.FirstOrDefault(i => i.IsPrimary)?.IamgeUrl,
                ProductVariantId = ci.VariantId,
                VariantDetails = ci.Variant != null ? 
                    $"{ci.Variant.Color} - {ci.Variant.Storage}" : null,
                Quantity = ci.Quantity,
                UnitPrice = unitPrice,
                TotalPrice = unitPrice * ci.Quantity,
                CreatedAt = ci.DateCreated,
            };
        }).ToList();

        return new CartResponse
        {
            Id = cart.Id,
            SessionId = cart.SessionId,
            Items = cartItemDtos,
            Total = cartItemDtos.Sum(ci => ci.TotalPrice),
            ItemCount = cartItemDtos.Sum(ci => ci.Quantity),
            UpadtedAt = cart.DateModified,
        };
    }

    
}