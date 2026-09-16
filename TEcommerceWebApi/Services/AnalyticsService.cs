using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TEcommerceWebApi.data;
using TEcommerceWebApi.DTOs;
using TEcommerceWebApi.Interfaces;
namespace TEcommerceWebApi.Services
{
    public class AnalyticsService : IAnalyticsService
    {
        private readonly AppDbContext _appDbContext;

        public AnalyticsService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<CategorySummaryDto>> GetCategorySummariesAsync()
        {
            return await _appDbContext.Categories
                .AsNoTracking()
                .Select(c => new CategorySummaryDto
                {
                    CategoryId = c.CategoryId,
                    CategoryName = c.Name,
                    TotalProducts = c.Products.Count(),
                    AveragePrice = c.Products.Any() ? c.Products.Average(p => p.Price) : 0,
                    MaxPrice = c.Products.Any() ? c.Products.Max(p => p.Price) : 0,
                    MinPrice = c.Products.Any() ? c.Products.Min(p => p.Price) : 0
                })
                .ToListAsync();
        }

        public async Task<List<TopSellingProductDto>> GetTopSellingProductsAsync(int topCount = 5)
        {
            return await _appDbContext.OrderItems
                .AsNoTracking()
                .GroupBy(oi => new 
                { 
                    oi.ProductId, 
                    oi.Product.Name, 
                    CategoryName = oi.Product.Category != null ? oi.Product.Category.Name : "Uncategorized" 
                })
                .Select(g => new TopSellingProductDto
                {
                    ProductId = g.Key.ProductId,
                    ProductName = g.Key.Name,
                    CategoryName = g.Key.CategoryName,
                    TotalUnitsSold = g.Sum(oi => oi.Quantity),
                    TotalRevenue = g.Sum(oi => oi.Quantity * oi.UnitPrice)
                })
                .OrderByDescending(p => p.TotalUnitsSold)
                .Take(topCount)
                .ToListAsync();
        }

        public async Task<List<CustomerSpendingDto>> GetCustomerSpendingSummaryAsync(decimal minSpent = 0)
        {
            return await _appDbContext.Users
                .AsNoTracking()
                .Select(u => new CustomerSpendingDto
                {
                    UserId = u.UserId,
                    CustomerName = u.FullName,
                    Email = u.Email,
                    TotalOrdersPlaced = u.Orders.Count(),
                    TotalAmountSpent = u.Orders.Any() ? u.Orders.Sum(o => o.TotalAmount) : 0,
                    LastOrderDate = u.Orders.Max(o => (DateTime?)o.OrderDate)
                })
                .Where(x => x.TotalAmountSpent >= minSpent)
                .OrderByDescending(x => x.TotalAmountSpent)
                .ToListAsync();
        }

        public async Task<SalesOverviewDto> GetSalesOverviewAsync()
        {
            var totalRevenue = await _appDbContext.Orders
                .AsNoTracking()
                .Where(o => o.Status != Enums.OrderStatus.Cancelled)
                .SumAsync(o => o.TotalAmount);

            var totalOrders = await _appDbContext.Orders.CountAsync();
            var totalCustomers = await _appDbContext.Users.CountAsync();
            var totalProducts = await _appDbContext.Products.CountAsync(); // Filtered automatically by !IsDeleted!
            
            var lowStockCount = await _appDbContext.Products
                .AsNoTracking()
                .CountAsync(p => p.StockQuantity < 5);

            return new SalesOverviewDto
            {
                TotalRevenue = totalRevenue,
                TotalOrdersCount = totalOrders,
                TotalCustomersCount = totalCustomers,
                TotalActiveProducts = totalProducts,
                LowStockProductsCount = lowStockCount
            };
        }
    }
}