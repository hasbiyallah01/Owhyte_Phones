using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Owhytee_Phones.Core.Application.Interface.Service;
using Owhytee_Phones.Models.CartModel;

namespace Owhytee_Phones.Controllers
{
    [Route("api/cooperatives")]
    [ApiController]
    public class CooperativesController : ControllerBase
    {
        private readonly ICartService _cartService;
        public CooperativesController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet("{sessionId}")]
        public async Task<IActionResult> GetCart(string sessionId)
        {
            var cart = await _cartService.GetCartAsync(sessionId);
            if (cart == null)
            {
                return NotFound(new { message = "Cart not found" });
            }
            return Ok(cart);
        }

        [HttpPost("{sessionId}/items")]
        public async Task<IActionResult> AddToCart(string sessionId, [FromBody] CartRequest cartRequest)
        {
            var cart = await _cartService.AddToCartAsync(sessionId, cartRequest);
            return Ok(cart);
        }

        [HttpPut("{sessionId}/items/{cartItemId}")]
        public async Task<IActionResult> UpdateCartItem(string sessionId, int cartItemId, [FromBody] int quantity)
        {
            var cart = await _cartService.UpdateCartItemAsync(sessionId, cartItemId, quantity);
            if (cart == null)
            {
                return NotFound(new { message = "Cart or item not found" });
            }
            return Ok(cart);
        }

        [HttpDelete("{sessionId}/items/{cartItemId}")]
        public async Task<IActionResult> RemoveFromCart(string sessionId, int cartItemId)
        {
            var cart = await _cartService.RemoveFromCartAsync(sessionId, cartItemId);
            if (cart == null)
            {
                return NotFound(new { message = "Cart or item not found" });
            }
            return Ok(cart);
        }

        [HttpDelete("{sessionId}")]
        public async Task<IActionResult> ClearCart(string sessionId)
        {
            var result = await _cartService.ClearCartAsync(sessionId);
            if (!result)
            {
                return NotFound(new { message = "Cart not found" });
            }
            return Ok(new { message = "Cart cleared successfully" });
        }

        [HttpGet("{sessionId}/total")]
        public async Task<IActionResult> GetCartTotal(string sessionId)
        {
            var total = await _cartService.GetCartTotalAsync(sessionId);
            return Ok(new { total });
        }
    }
}
