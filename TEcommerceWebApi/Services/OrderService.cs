using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TEcommerceWebApi.data;
using TEcommerceWebApi.DTOs;
using TEcommerceWebApi.Enums;
using TEcommerceWebApi.Interfaces;

namespace TEcommerceWebApi.Services
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _appDbContext;

        public OrderService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<OrderReadDto>?> GetOrdersByUserIdAsync(Guid userId, OrderStatus? status = null)
        {
            // 1. Verify User Exists
            var userExists = await _appDbContext.Users.AnyAsync(u => u.UserId == userId);
            if (!userExists)
            {
                return null; // Return null so Controller knows to respond with 404
            }

            // 2. Build Query starting from Orders
            var query = _appDbContext.Orders
                .AsNoTracking()
                .Where(o => o.UserId == userId);

            // 3. Optional Status Filter
            if (status.HasValue)
            {
                query = query.Where(o => o.Status == status.Value);
            }

            // 4. Sort and Project to DTO (with nested collection)
            return await query
                .OrderByDescending(o => o.OrderDate)
                .Select(o => new OrderReadDto
                {
                    OrderId = o.OrderId,
                    OrderDate = o.OrderDate,
                    TotalAmount = o.TotalAmount,
                    Status = o.Status.ToString(),
                    CustomerName = o.User != null ? o.User.FullName : string.Empty,
                    CustomerEmail = o.User != null ? o.User.Email : string.Empty,
                    Items = o.OrderItems.Select(oi => new OrderItemReadDto
                    {
                        OrderItemId = oi.OrderItemId,
                        ProductId = oi.ProductId,
                        ProductName = oi.Product != null ? oi.Product.Name : string.Empty,
                        CategoryName = oi.Product != null && oi.Product.Category != null 
                                       ? oi.Product.Category.Name 
                                       : string.Empty,
                        Quantity = oi.Quantity,
                        UnitPrice = oi.UnitPrice
                    }).ToList()
                })
                .ToListAsync();
        }
    }
}