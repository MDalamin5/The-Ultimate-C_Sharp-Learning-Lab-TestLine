using System.Collections.Generic;
using System.Threading.Tasks;
using TEcommerceWebApi.DTOs;

namespace TEcommerceWebApi.Interfaces
{
    public interface IAnalyticsService
    {
        Task<List<CategorySummaryDto>> GetCategorySummariesAsync();
        Task<List<TopSellingProductDto>> GetTopSellingProductsAsync(int topCount = 5);
    }
}