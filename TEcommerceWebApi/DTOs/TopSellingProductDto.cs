using System;

namespace TEcommerceWebApi.DTOs
{
    public class TopSellingProductDto
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public int TotalUnitsSold { get; set; }
        public decimal TotalRevenue { get; set; }
    }
}