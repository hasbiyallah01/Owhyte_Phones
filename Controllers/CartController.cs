using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Owhytee_Phones.Core.Application.Interface.Service;
using Owhytee_Phones.Models.CartModel;

namespace Owhytee_Phones.Controllers
{
    [Route("api/cart")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet("{sessionId}")]
        public async Task<ActionResult<CartResponse>> GetCart(string sessionId)
        {
            var cart = await _cartService.GetCartAsync(sessionId);
            if (cart == null)
            {
                return Ok(new { message = "Cart is empty" });
            }

            return Ok(cart);
        }


        [HttpPost("{sessionId}/items")]
        public async Task<ActionResult<CartResponse>> AddToCart(string sessionId, CartRequest addToCartDto)
        {
            try
            {
                var cart = await _cartService.AddToCartAsync(sessionId, addToCartDto);
                if(cart == null)
                {
                    return BadRequest(new { message = "Failed to add item to cart" });
                }
                return Ok(cart);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPut("{sessionId}/items/{cartItemId}")]
        public async Task<ActionResult<CartResponse>> UpdateCartItem(string sessionId, int cartItemId, [FromBody] int quantity)
        {
            try
            {
                var cart = await _cartService.UpdateCartItemAsync(sessionId, cartItemId, quantity);
                return Ok(cart);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpDelete("{sessionId}/items/{cartItemId}")]
        public async Task<ActionResult<CartResponse>> RemoveFromCart(string sessionId, int cartItemId)
        {
            try
            {
                var cart = await _cartService.RemoveFromCartAsync(sessionId, cartItemId);
                return Ok(cart);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpDelete("{sessionId}")]
        public async Task<IActionResult> ClearCart(string sessionId)
        {
            var success = await _cartService.ClearCartAsync(sessionId);
            if (!success)
            {
                return NotFound();
            }

            return success ? Ok(new { message = "Cart cleared successfully" }) : StatusCode(500, new { message = "Failed to clear cart" });
        }

        [HttpGet("{sessionId}/total")]
        public async Task<ActionResult<decimal>> GetCartTotal(string sessionId)
        {
            var total = await _cartService.GetCartTotalAsync(sessionId);
            return Ok(new { total });
        }
    }
}
