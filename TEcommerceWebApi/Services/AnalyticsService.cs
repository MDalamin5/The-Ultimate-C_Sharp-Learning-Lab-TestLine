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
    }
}