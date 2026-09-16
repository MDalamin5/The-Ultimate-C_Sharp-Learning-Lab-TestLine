namespace TEcommerceWebApi.DTOs
{
    public class SalesOverviewDto
    {
        public decimal TotalRevenue { get; set; }
        public int TotalOrdersCount { get; set; }
        public int TotalCustomersCount { get; set; }
        public int TotalActiveProducts { get; set; }
        public int LowStockProductsCount { get; set; } // Products with stock < 5
    }
}