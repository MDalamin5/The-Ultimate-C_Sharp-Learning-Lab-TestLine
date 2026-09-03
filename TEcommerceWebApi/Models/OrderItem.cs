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

/*
💡 Senior Developer Note — Why is UnitPrice stored inside OrderItem?
A product’s price in the Products table might change from $100 to $150 next month. If you only read the price from the Products table, all your past receipts and accounting history would change! OrderItem.UnitPrice takes a snapshot of what the customer actually paid at the moment they bought it.
*/