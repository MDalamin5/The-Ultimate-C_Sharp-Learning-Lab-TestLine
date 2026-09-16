using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TEcommerceWebApi.data;
using TEcommerceWebApi.DTOs;
using TEcommerceWebApi.Enums;
using TEcommerceWebApi.Helpers;
using TEcommerceWebApi.Interfaces;
using TEcommerceWebApi.Models;
using TEcommerceWebApi.Controllers;

namespace TEcommerceWebApi.Services
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _appDbContext;

        public OrderService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<OrderReadDto> CheckoutAsync(OrderCheckoutDto checkoutData)
        {
            // 1. Verify User Exists
            var user = await _appDbContext.Users.FindAsync(checkoutData.UserId);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID '{checkoutData.UserId}' does not exist.");
            }

            // 2. Fetch all requested products from DB in ONE single query (efficient batch lookup)
            var productIds = checkoutData.Items.Select(i => i.ProductId).Distinct().ToList();
            var products = await _appDbContext.Products
                .Include(p => p.Category)
                .Where(p => productIds.Contains(p.ProductId))
                .ToListAsync();

            // 3. START ATOMIC DATABASE TRANSACTION ⚡
            using var transaction = await _appDbContext.Database.BeginTransactionAsync();

            try
            {
                var orderItems = new List<OrderItem>();
                decimal totalAmount = 0;
                var newOrderId = Guid.NewGuid();

                foreach (var itemDto in checkoutData.Items)
                {
                    var product = products.FirstOrDefault(p => p.ProductId == itemDto.ProductId);
                    if (product == null)
                    {
                        throw new KeyNotFoundException($"Product with ID '{itemDto.ProductId}' was not found.");
                    }

                    // Check Stock Availability
                    if (product.StockQuantity < itemDto.Quantity)
                    {
                        throw new InvalidOperationException(
                            $"Insufficient stock for '{product.Name}'. Available: {product.StockQuantity}, Requested: {itemDto.Quantity}."
                        );
                    }

                    // Deduct Stock
                    product.StockQuantity -= itemDto.Quantity;

                    // Calculate Subtotal with verified DB Price
                    var itemTotal = product.Price * itemDto.Quantity;
                    totalAmount += itemTotal;

                    // Create Line Item
                    orderItems.Add(new OrderItem
                    {
                        OrderItemId = Guid.NewGuid(),
                        OrderId = newOrderId,
                        ProductId = product.ProductId,
                        Quantity = itemDto.Quantity,
                        UnitPrice = product.Price // Snapshot of current price
                    });
                }

                // 4. Create Parent Order Entity
                var order = new Order
                {
                    OrderId = newOrderId,
                    UserId = user.UserId,
                    OrderDate = DateTime.UtcNow,
                    TotalAmount = totalAmount,
                    Status = OrderStatus.Pending,
                    OrderItems = orderItems
                };

                // 5. Add Order to context (EF Core change tracker will save Order, OrderItems, and update Product stocks!)
                await _appDbContext.Orders.AddAsync(order);
                await _appDbContext.SaveChangesAsync();

                // 6. COMMIT TRANSACTION TO POSTGRESQL 🚀
                await transaction.CommitAsync();

                // 7. Return complete OrderReadDto
                return await GetOrderByIdAsync(newOrderId) 
                    ?? throw new Exception("Error loading created order.");
            }
            catch
            {
                // If anything fails above, ROLLBACK all changes!
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<OrderReadDto?> GetOrderByIdAsync(Guid orderId)
        {
            return await _appDbContext.Orders
                .AsNoTracking()
                .Where(o => o.OrderId == orderId)
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
                        CategoryName = oi.Product != null && oi.Product.Category != null ? oi.Product.Category.Name : string.Empty,
                        Quantity = oi.Quantity,
                        UnitPrice = oi.UnitPrice
                    }).ToList()
                })
                .FirstOrDefaultAsync();
        }

        public async Task<List<OrderReadDto>?> GetOrdersByUserIdAsync(Guid userId, OrderStatus? status = null)
        {
            var userExists = await _appDbContext.Users.AnyAsync(u => u.UserId == userId);
            if (!userExists) return null;

            var query = _appDbContext.Orders
                .AsNoTracking()
                .Where(o => o.UserId == userId);

            if (status.HasValue)
            {
                query = query.Where(o => o.Status == status.Value);
            }

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
                        CategoryName = oi.Product != null && oi.Product.Category != null ? oi.Product.Category.Name : string.Empty,
                        Quantity = oi.Quantity,
                        UnitPrice = oi.UnitPrice
                    }).ToList()
                })
                .ToListAsync();
        }

        public async Task<PaginatedResult<OrderReadDto>> GetAllOrdersAsync(QueryParameters queryParameters, OrderStatus? status = null)
        {
            var query = _appDbContext.Orders
                .AsNoTracking()
                .AsQueryable();

            if (status.HasValue)
            {
                query = query.Where(o => o.Status == status.Value);
            }

            query = query.OrderByDescending(o => o.OrderDate);

            var totalCount = await query.CountAsync();

            var orders = await query
                .Skip((queryParameters.PageNumber - 1) * queryParameters.PageSize)
                .Take(queryParameters.PageSize)
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
                        CategoryName = oi.Product != null && oi.Product.Category != null ? oi.Product.Category.Name : string.Empty,
                        Quantity = oi.Quantity,
                        UnitPrice = oi.UnitPrice
                    }).ToList()
                })
                .ToListAsync();

            return new PaginatedResult<OrderReadDto>
            {
                Items = orders,
                TotalCount = totalCount,
                PageNumber = queryParameters.PageNumber,
                PageSize = queryParameters.PageSize
            };
        }

        public async Task<OrderReadDto?> UpdateOrderStatusAsync(Guid orderId, OrderStatus newStatus)
        {
            var order = await _appDbContext.Orders.FindAsync(orderId);
            if (order == null) return null;

            order.Status = newStatus;
            await _appDbContext.SaveChangesAsync();

            return await GetOrderByIdAsync(orderId);
        }
    }
}