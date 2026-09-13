using System;

namespace TEcommerceWebApi.DTOs
{
    public class CustomerSpendingDto
    {
        public Guid UserId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int TotalOrdersPlaced { get; set; }
        public decimal TotalAmountSpent { get; set; }
        public DateTime? LastOrderDate { get; set; }
    }
}