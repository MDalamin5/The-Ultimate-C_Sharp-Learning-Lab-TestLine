using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TEcommerceWebApi.Models
{
    public class OrderItem
    {
        public Guid OrderItemId { get; set; }

        // 1. Foreign Key & Navigation to Order
        public Guid OrderId { get; set; }
        public Order? Order { get; set; }

        // 2. Foreign Key & Navigation to Product
        public Guid ProductId { get; set; }
        public Product? Product { get; set; }

        // 3. Payload Columns
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}