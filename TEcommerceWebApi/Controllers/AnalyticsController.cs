using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TEcommerceWebApi.DTOs;
using TEcommerceWebApi.Interfaces;

namespace TEcommerceWebApi.Controllers
{
    [ApiController]
    [Route("/api/v2/analytics")]
    public class AnalyticsController : ControllerBase
    {
        private readonly IAnalyticsService _analyticsService;

        public AnalyticsController(IAnalyticsService analyticsService)
        {
            _analyticsService = analyticsService;
        }

        [HttpGet("category-summary")]
        public async Task<ActionResult<ApiResponse<List<CategorySummaryDto>>>> GetCategorySummaries()
        {
            var summaries = await _analyticsService.GetCategorySummariesAsync();
            return Ok(ApiResponse<List<CategorySummaryDto>>.SuccessResponse(summaries, 200, "Category summaries retrieved."));
        }

        [HttpGet("top-selling-products")]
        public async Task<ActionResult<ApiResponse<List<TopSellingProductDto>>>> GetTopSellingProducts([FromQuery] int count = 5)
        {
            var topProducts = await _analyticsService.GetTopSellingProductsAsync(count);
            return Ok(ApiResponse<List<TopSellingProductDto>>.SuccessResponse(topProducts, 200, "Top selling products retrieved."));
        }
    }
}