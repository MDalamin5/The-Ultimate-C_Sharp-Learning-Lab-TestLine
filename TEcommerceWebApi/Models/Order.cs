using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TEcommerceWebApi.Enums;

namespace TEcommerceWebApi.Models
{
    public class Order
    {
        public Guid OrderId { get; set; }

        // Foreign Key to User
        public Guid UserId { get; set; }
        // Navigation Property back to User
        public User? User { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;

    }
}