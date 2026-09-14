using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TEcommerceWebApi.DTOs;
using TEcommerceWebApi.Enums;
using TEcommerceWebApi.Interfaces;

namespace TEcommerceWebApi.Controllers
{
    [ApiController]
    [Route("/api/v2/orders")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("user/{userId:guid}")]
        public async Task<ActionResult<ApiResponse<List<OrderReadDto>>>> GetUserOrders(
            Guid userId, 
            [FromQuery] OrderStatus? status)
        {
            var orders = await _orderService.GetOrdersByUserIdAsync(userId, status);

            if (orders == null)
            {
                return NotFound(ApiResponse<object>.ErrorResponse(
                    new List<string> { $"User with ID '{userId}' does not exist." },
                    404,
                    "User Not Found."
                ));
            }

            return Ok(ApiResponse<List<OrderReadDto>>.SuccessResponse(
                orders, 
                200, 
                "User orders retrieved successfully."
            ));
        }
    }
}