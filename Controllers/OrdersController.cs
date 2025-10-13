using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Owhytee_Phones.Core.Application.Interface.Service;
using Owhytee_Phones.Core.Domain.Enum;
using Owhytee_Phones.Models.OrderModel;
using Owhytee_Phones.Models.ProductModel;

namespace Owhytee_Phones.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ILogger<OrdersController> _logger;
        private readonly IOrderService _orderService;
        public OrdersController(ILogger<OrdersController> logger, IOrderService orderService)
        {
            _logger = logger;
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateOrder([FromBody] OrderRequest orderRequest)
        {
            try
            {
                var order = await _orderService.CreateOrderAsync(orderRequest);
                if (order == null)
                {
                    return BadRequest(new { message = "Order creation failed" });
                }
                return Ok(order);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating order for cart");
                return StatusCode(500, new { message = "Internal server error" });
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<PagedResult<OrderResponse>>> GetOrders([FromQuery]OrderQueryResponse query)
        {
            try
            {
                var filter = new OrderFilterResponse
                {
                    Status = query.Status,
                    StartDate = query.StartDate,
                    EndDate = query.EndDate,
                    CooperativeId = query.CooperativeId
                };
                var orders = await _orderService.GetOrdersAsync(filter, query.Page, query.PageSize);
                return Ok(orders);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving orders");
                return StatusCode(500, new { message = "Internal server error" });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderResponse>> GetOrderById(int id)
        {
            try
            {
                var order = await _orderService.GetOrderByIdAsync(id);
                if (order == null)
                {
                    return NotFound(new { message = "Order not found" });
                }
                return Ok(order);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving order with ID {id}");
                return StatusCode(500, new { message = "Internal server error" });
            }
        }

        [HttpPut("{id}/status")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<OrderResponse>> UpdateOrderStatus(int id, [FromBody] OrderStatus status)
        {
            try
            {
                var order = await _orderService.UpdateOrderStatusAsync(id, status);
                if (order == null)
                {
                    return NotFound(new { message = "Order not found or status update failed" });
                }
                return Ok(order);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating status for order with ID {id}");
                return StatusCode(500, new { message = "Internal server error" });
            }
        }

        [HttpPost("{orderId}/assign/{cooperativeId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<OrderResponse>> AssignOrderToCooperative(int orderId, int cooperativeId)
        {
            try
            {
                var order = await _orderService.AssignOrderToCooperativeAsync(orderId, cooperativeId);
                if (order == null)
                {
                    return NotFound(new { message = "Order not found or assignment failed" });
                }
                return Ok(order);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error assigning order with ID {orderId} to cooperative {cooperativeId}");
                return StatusCode(500, new { message = "Internal server error" });
            }
        }

        [HttpGet("{orderId}/whatsapp")]
        public async Task<ActionResult<WhatsAppMessageRequest>> GenerateWhatsAppMessage(int orderId)
        {
            try
            {
                var message = await _orderService.GenerateWhatsAppMessageAsync(orderId);
                if (message == null)
                {
                    return NotFound(new { message = "Order not found or message generation failed" });
                }
                return Ok(message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error generating WhatsApp message for order with ID {orderId}");
                return StatusCode(500, new { message = "Internal server error" });
            }
        }
    }
}
